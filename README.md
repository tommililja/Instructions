Example:

```fsharp
module Dice =

    let roll dice = instr {

        let! number =
            dice.Size
            |> Instruction.roll

        do!
            dice.Delay
            |> Option.map Instruction.delay
            |> Option.defaultWith Instruction.ret

        do! Instruction.log $"Dice rolled: %i{number}."

        return number
    }
```
