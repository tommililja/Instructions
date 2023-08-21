namespace Instructions

open System
open System.Threading.Tasks

module App =

    let dice = {
        Size = 6
        Delay = Some 3000
    }

    let random = Random()

    let interpreter = {
        Delay = fun ms -> Task.Delay(ms) |> Async.AwaitTask
        GenerateNumber = fun max -> random.Next(1, max + 1)
        Log = Console.WriteLine
    }

    dice
    |> Dice.roll
    |> Instruction.interpret interpreter
    |> Async.RunSynchronously
    |> ignore
