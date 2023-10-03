namespace Instructions

type 'a Instruction =
    | Roll of int * (int -> 'a Instruction)
    | Delay of int * (unit Async -> 'a Instruction Async)
    | Log of string * (unit -> 'a Instruction)
    | Done of 'a

module Instruction =

    let ret = Done

    let retAsync x = Async.ret x |> ret

    let rec bind fn = function
        | Roll (max, next) -> Roll (max, next >> bind fn)
        | Delay (ms, next) -> Delay (ms, next >> Async.map (bind fn))
        | Log (str, next) -> Log (str, next >> bind fn)
        | Done x -> fn x

    let map fn = bind (fn >> ret)

    let rec interpret interpreter = function
        | Roll (max, next) ->
            interpreter.Roll max
            |> next
            |> interpret interpreter
        | Delay (ms, next) ->
            interpreter.Delay ms
            |> next
            |> Async.bind (interpret interpreter)
        | Log (str, next) ->
            interpreter.Log str
            |> next
            |> interpret interpreter
        | Done x -> Async.ret x

    let roll max = Roll (max, ret)

    let delay ms = Delay (ms, Async.map ret)

    let log str = Log (str, ret)
