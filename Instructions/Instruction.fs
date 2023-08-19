namespace Instructions

type Interpreter = {
    Delay: int -> unit Async
    GenerateNumber: int -> int
    Log: string -> unit
}

type 'a Instruction =
    | GenerateNumber of int * (int -> 'a Instruction)
    | Delay of int * (unit Async -> 'a Instruction Async)
    | Log of string * (unit -> 'a Instruction)
    | Done of 'a

module Instruction =

    let ret = Done

    let retAsync x = Async.ret x |> ret

    let rec bind fn = function
        | Delay (ms, next) -> Delay(ms, next >> Async.map (bind fn))
        | GenerateNumber (max, next) -> GenerateNumber (max, next >> bind fn)
        | Log (str, next) -> Log (str, next >> bind fn)
        | Done x -> fn x

    let map fn = bind (fn >> ret)

    let rec run interpreter = function
        | Delay (ms, next) ->
            interpreter.Delay ms
            |> next
            |> Async.bind (run interpreter)
        | GenerateNumber (max, next) ->
            interpreter.GenerateNumber max
            |> next
            |> run interpreter
        | Log (str, next) ->
            interpreter.Log str
            |> next
            |> run interpreter
        | Done x -> Async.ret x

    let delay ms = Delay (ms, Async.map ret)

    let generateNumber max = GenerateNumber (max, ret)

    let log str = Log (str, ret)
