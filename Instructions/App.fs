namespace Instructions

module App =

    let dice = {
        Size = 6
        Delay = Some 3000
    }

    let interpreter = Interpreter.create ()

    dice
    |> Dice.roll
    |> Instruction.interpret interpreter
    |> Async.RunSynchronously
    |> ignore
