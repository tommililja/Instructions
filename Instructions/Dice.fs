namespace Instructions

type Dice = {
    Size: int
    Delay: int option
}

module Dice =

    open Instruction

    let roll dice = instr {

        let! number =
            dice.Size
            |> generateNumber

        do!
            dice.Delay
            |> Option.map delay
            |> Option.defaultWith ret

        do! log $"Dice rolled: %i{number}."

        return number
    }
