namespace Instructions

module Async =

    let ret x = async {
        return x
    }

    let bind fn x = async {
        let! a = x
        return! fn a
    }

    let map fn = bind (fn >> ret)
