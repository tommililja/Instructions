namespace Dice.Tests

open Instructions
open Xunit

module DiceTests =

    [<Fact>]
    let ``Dice roll is equal to max`` () = async {

        let dice = { Size = 6; Delay = None }

        let interpreter = {
            Delay = ignore >> Async.ret
            Roll = id
            Log = ignore
        }

        let! roll =
            dice
            |> Dice.roll
            |> Instruction.interpret interpreter

        Assert.Equal(roll, dice.Size)
    }
