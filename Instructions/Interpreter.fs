namespace Instructions

open System
open System.Threading.Tasks

type Interpreter = {
    Roll: int -> int
    Delay: int -> unit Async
    Log: string -> unit
}

module Interpreter =

    let private random = Random()

    let private delay (ms:int) =
        Task.Delay(ms)
        |> Async.AwaitTask

    let private roll max =
        random.Next(1, max + 1)

    let create () = {
        Delay = delay
        Roll = roll
        Log = Console.WriteLine
    }
