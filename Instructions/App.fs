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
    }

    let result =
        dice
        |> Dice.roll
        |> Instruction.run interpreter
        |> Async.RunSynchronously

    Console.WriteLine($"Dice: {result}.")
