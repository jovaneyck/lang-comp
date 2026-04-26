using System.Collections.Generic;

namespace GameOfLife;

public enum Cell { Dead, Alive, Zombie }

public record Position(int Row, int Col);

public record Grid(int Width, int Height, Dictionary<Position, Cell> Cells)
{
    public Cell? CellAt(Position position)
    {
        return Cells.TryGetValue(position, out var cell) ? cell : null;
    }

    public int CountLiveNeighbors(Position position)
    {
        (int dr, int dc)[] offsets =
        [
            (-1, -1), (-1, 0), (-1, 1),
            (0, -1),           (0, 1),
            (1, -1),  (1, 0),  (1, 1)
        ];

        var count = 0;
        for (var i = 0; i < offsets.Length; i++)
        {
            var neighbor = new Position(position.Row + offsets[i].dr, position.Col + offsets[i].dc);
            var c = CellAt(neighbor);
            if (c == Cell.Alive || c == Cell.Zombie)
                count++;
        }
        return count;
    }

    public static Cell EvolveCell(Cell cell, int liveNeighbors) => (cell, liveNeighbors) switch
    {
        (Cell.Zombie, _) => Cell.Zombie,
        (Cell.Alive, 2) => Cell.Alive,
        (Cell.Alive, 3) => Cell.Alive,
        (Cell.Dead, 3) => Cell.Alive,
        _ => Cell.Dead
    };

    public Grid Tick()
    {
        var newCells = new Dictionary<Position, Cell>();
        for (var row = 0; row < Height; row++)
        {
            for (var col = 0; col < Width; col++)
            {
                var pos = new Position(row, col);
                var current = CellAt(pos) ?? Cell.Dead;
                var neighbors = CountLiveNeighbors(pos);
                newCells[pos] = EvolveCell(current, neighbors);
            }
        }
        return this with { Cells = newCells };
    }
}
