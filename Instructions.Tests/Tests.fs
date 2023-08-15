namespace Dice.Tests

open Instructions
open Xunit

module DiceTests =

    let private interpreter = {
        Delay = ignore >> Async.ret
        GenerateNumber = id
    }

    [<Fact>]
    let ``Assert dice roll is equal to max`` () = async {

        let dice = {
            Size = 6
            Delay = None
        }

        let! roll =
            dice
            |> Dice.roll
            |> Instruction.run interpreter

        Assert.Equal(roll, dice.Size)
    }
