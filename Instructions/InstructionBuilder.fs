namespace Instructions

[<AutoOpen>]
module InstructionBuilder =

    open Instruction

    type InstructionBuilder() =

        member this.Bind(x, fn) = bind fn x

        member this.Return x = ret x

        member this.Zero () = ret ()

        member this.ReturnFrom x = x

    let instr = InstructionBuilder()
