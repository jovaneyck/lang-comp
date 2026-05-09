module GameOfLife

type Cell =
    | Alive
    | Dead
    | Zombie

type Position = { Row: int; Col: int }

type Grid =
    { Width: int
      Height: int
      Cells: Map<Position, Cell> }

let cellAt (grid: Grid) (position: Position) : Cell option = Map.tryFind position grid.Cells

let isLiveNeighbor cell =
    match cell with
    | Alive
    | Zombie -> true
    | Dead -> false

let countLiveNeighbors (grid: Grid) (position: Position) : int =
    [ (-1, -1)
      (-1, 0)
      (-1, 1)
      (0, -1)
      (0, 1)
      (1, -1)
      (1, 0)
      (1, 1) ]
    |> List.map (fun (dr, dc) ->
        cellAt
            grid
            { Row = position.Row + dr
              Col = position.Col + dc })
    |> List.filter (Option.exists isLiveNeighbor)
    |> List.length

let evolveCell (cell: Cell) (liveNeighbors: int) : Cell =
    match cell, liveNeighbors with
    | Zombie, _ -> Zombie
    | Alive, 2
    | Alive, 3 -> Alive
    | Dead, 3 -> Alive
    | _ -> Dead

let tickGrid (grid: Grid) : Grid =
    let cells: Map<Position, Cell> =
        [ for row in 0 .. grid.Height - 1 do
              for col in 0 .. grid.Width - 1 do
                  let pos = { Row = row; Col = col }
                  let current = cellAt grid pos |> Option.defaultValue Dead
                  let neighbors = countLiveNeighbors grid pos
                  pos, evolveCell current neighbors ]
        |> Map.ofList

    { grid with Cells = cells }
