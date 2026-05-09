module GameOfLifeTests

open FsUnit.Xunit
open Xunit

open GameOfLife

let O: Cell = Dead
let X: Cell = Alive
let Z: Cell = Zombie

let grid (rows: Cell list list) : Grid =
    let height: int = List.length rows

    let width: int =
        if height = 0 then
            0
        else
            List.length rows.[0]

    let cells: Map<Position, Cell> =
        [ for row in 0 .. height - 1 do
              for col in 0 .. width - 1 do
                  { Row = row; Col = col }, rows.[row].[col] ]
        |> Map.ofList

    { Width = width
      Height = height
      Cells = cells }

[<Fact>]
let ``Empty matrix`` () =
    tickGrid (grid []) |> should equal (grid [])

[<Fact>]
let ``Live cells with zero live neighbors die`` () =
    let input =
        grid [ [ O; O; O ]
               [ O; X; O ]
               [ O; O; O ] ]

    let expected =
        grid [ [ O; O; O ]
               [ O; O; O ]
               [ O; O; O ] ]

    tickGrid input |> should equal expected

[<Fact>]
let ``Live cells with only one live neighbor die`` () =
    let input =
        grid [ [ O; O; O ]
               [ O; X; O ]
               [ O; X; O ] ]

    let expected =
        grid [ [ O; O; O ]
               [ O; O; O ]
               [ O; O; O ] ]

    tickGrid input |> should equal expected

[<Fact>]
let ``Live cells with two live neighbors stay alive`` () =
    let input =
        grid [ [ X; O; X ]
               [ X; O; X ]
               [ X; O; X ] ]

    let expected =
        grid [ [ O; O; O ]
               [ X; O; X ]
               [ O; O; O ] ]

    tickGrid input |> should equal expected

[<Fact>]
let ``Live cells with three live neighbors stay alive`` () =
    let input =
        grid [ [ O; X; O ]
               [ X; O; O ]
               [ X; X; O ] ]

    let expected =
        grid [ [ O; O; O ]
               [ X; O; O ]
               [ X; X; O ] ]

    tickGrid input |> should equal expected

[<Fact>]
let ``Dead cells with three live neighbors become alive`` () =
    let input =
        grid [ [ X; X; O ]
               [ O; O; O ]
               [ X; O; O ] ]

    let expected =
        grid [ [ O; O; O ]
               [ X; X; O ]
               [ O; O; O ] ]

    tickGrid input |> should equal expected

[<Fact>]
let ``Live cells with four or more neighbors die`` () =
    let input =
        grid [ [ X; X; X ]
               [ X; X; X ]
               [ X; X; X ] ]

    let expected =
        grid [ [ X; O; X ]
               [ O; O; O ]
               [ X; O; X ] ]

    tickGrid input |> should equal expected

[<Fact>]
let ``Bigger matrix`` () =
    let input =
        grid [ [ X; X; O; X; X; O; O; O ]
               [ X; O; X; X; O; O; O; O ]
               [ X; X; X; O; O; X; X; X ]
               [ O; O; O; O; O; X; X; O ]
               [ X; O; O; O; X; X; O; O ]
               [ X; X; O; O; O; X; X; X ]
               [ O; O; X; O; X; O; O; X ]
               [ X; O; O; O; O; O; X; X ] ]

    let expected =
        grid [ [ X; X; O; X; X; O; O; O ]
               [ O; O; O; O; O; X; X; O ]
               [ X; O; X; X; X; X; O; X ]
               [ X; O; O; O; O; O; O; X ]
               [ X; X; O; O; X; O; O; X ]
               [ X; X; O; X; O; O; O; X ]
               [ X; O; O; O; O; O; O; O ]
               [ O; O; O; O; O; O; X; X ] ]

    tickGrid input |> should equal expected

[<Fact>]
let ``Zombie cells stay zombie regardless of neighbor count`` () =
    let withoutNeighbors =
        grid [ [ O; O; O ]
               [ O; Z; O ]
               [ O; O; O ] ]

    let withThreeNeighbors =
        grid [ [ X; X; O ]
               [ O; Z; O ]
               [ O; X; O ] ]

    let overcrowded =
        grid [ [ Z; Z; Z ]
               [ Z; Z; Z ]
               [ O; O; O ] ]

    tickGrid withoutNeighbors |> cellAt <| { Row = 1; Col = 1 } |> should equal (Some Z)
    tickGrid withThreeNeighbors |> cellAt <| { Row = 1; Col = 1 } |> should equal (Some Z)
    tickGrid overcrowded |> cellAt <| { Row = 1; Col = 1 } |> should equal (Some Z)

[<Fact>]
let ``Dead cell with three zombie neighbors becomes alive`` () =
    let input =
        grid [ [ Z; Z; O ]
               [ O; O; Z ]
               [ O; O; O ] ]

    tickGrid input |> cellAt <| { Row = 1; Col = 1 } |> should equal (Some X)

[<Fact>]
let ``Live cell with two zombie neighbors stays alive`` () =
    let input =
        grid [ [ Z; O; Z ]
               [ O; X; O ]
               [ O; O; O ] ]

    tickGrid input |> cellAt <| { Row = 1; Col = 1 } |> should equal (Some X)

[<Fact>]
let ``Live cell with four zombie neighbors dies`` () =
    let input =
        grid [ [ Z; Z; O ]
               [ Z; X; Z ]
               [ O; O; O ] ]

    tickGrid input |> cellAt <| { Row = 1; Col = 1 } |> should equal (Some O)

[<Fact>]
let ``Mixed grid evolves with zombies`` () =
    let input =
        grid [ [ O; O; X; O; O; O; Z; O; O ]
               [ O; O; O; X; O; O; O; O; O ]
               [ O; X; X; X; O; O; O; O; O ]
               [ O; O; O; O; O; O; O; O; O ]
               [ Z; O; O; O; O; O; O; O; Z ]
               [ O; O; O; O; O; O; O; O; O ]
               [ O; O; O; O; O; X; X; X; O ]
               [ O; O; O; O; O; X; O; O; O ]
               [ O; O; Z; O; O; X; O; Z; O ] ]

    let expected =
        grid [ [ O; O; O; O; O; O; Z; O; O ]
               [ O; X; O; X; O; O; O; O; O ]
               [ O; O; X; X; O; O; O; O; O ]
               [ O; X; X; O; O; O; O; O; O ]
               [ Z; O; O; O; O; O; O; O; Z ]
               [ O; O; O; O; O; O; X; X; O ]
               [ O; O; O; O; O; X; X; O; O ]
               [ O; O; O; O; X; X; O; X; O ]
               [ O; O; Z; O; O; O; X; Z; O ] ]

    tickGrid input |> should equal expected
