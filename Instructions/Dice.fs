namespace Instructions

type Dice = {
    Size: int
    Delay: int option
}

module Dice =

    let roll dice = instr {

        let! number =
            dice.Size
            |> Instruction.generateNumber

        do!
            dice.Delay
            |> Option.map Instruction.delay
            |> Option.defaultWith Instruction.ret

        do! Instruction.log $"Dice rolled: %i{number}."

        return number
    }
