using System.Collections.Generic;
using Xunit;

namespace GameOfLife;

public class GameOfLifeTests
{
    private const Cell O = Cell.Dead;
    private const Cell X = Cell.Alive;
    private const Cell Z = Cell.Zombie;

    private static Grid MakeGrid(Cell[][] rows)
    {
        var height = rows.Length;
        var width = height == 0 ? 0 : rows[0].Length;
        var cells = new Dictionary<Position, Cell>();
        for (var row = 0; row < height; row++)
            for (var col = 0; col < width; col++)
                cells[new Position(row, col)] = rows[row][col];
        return new Grid(width, height, cells);
    }

    private static void AssertGridsEqual(Grid expected, Grid actual)
    {
        Assert.Equal(expected.Width, actual.Width);
        Assert.Equal(expected.Height, actual.Height);
        for (var row = 0; row < expected.Height; row++)
            for (var col = 0; col < expected.Width; col++)
            {
                var pos = new Position(row, col);
                Assert.Equal(expected.Cells[pos], actual.Cells[pos]);
            }
    }

    [Fact]
    public void Empty_matrix()
    {
        AssertGridsEqual(MakeGrid([]), MakeGrid([]).Tick());
    }

    [Fact]
    public void Live_cells_with_zero_live_neighbors_die()
    {
        var input = MakeGrid([[O, O, O], [O, X, O], [O, O, O]]);
        var expected = MakeGrid([[O, O, O], [O, O, O], [O, O, O]]);
        AssertGridsEqual(expected, input.Tick());
    }

    [Fact]
    public void Live_cells_with_only_one_live_neighbor_die()
    {
        var input = MakeGrid([[O, O, O], [O, X, O], [O, X, O]]);
        var expected = MakeGrid([[O, O, O], [O, O, O], [O, O, O]]);
        AssertGridsEqual(expected, input.Tick());
    }

    [Fact]
    public void Live_cells_with_two_live_neighbors_stay_alive()
    {
        var input = MakeGrid([[X, O, X], [X, O, X], [X, O, X]]);
        var expected = MakeGrid([[O, O, O], [X, O, X], [O, O, O]]);
        AssertGridsEqual(expected, input.Tick());
    }

    [Fact]
    public void Live_cells_with_three_live_neighbors_stay_alive()
    {
        var input = MakeGrid([[O, X, O], [X, O, O], [X, X, O]]);
        var expected = MakeGrid([[O, O, O], [X, O, O], [X, X, O]]);
        AssertGridsEqual(expected, input.Tick());
    }

    [Fact]
    public void Dead_cells_with_three_live_neighbors_become_alive()
    {
        var input = MakeGrid([[X, X, O], [O, O, O], [X, O, O]]);
        var expected = MakeGrid([[O, O, O], [X, X, O], [O, O, O]]);
        AssertGridsEqual(expected, input.Tick());
    }

    [Fact]
    public void Live_cells_with_four_or_more_neighbors_die()
    {
        var input = MakeGrid([[X, X, X], [X, X, X], [X, X, X]]);
        var expected = MakeGrid([[X, O, X], [O, O, O], [X, O, X]]);
        AssertGridsEqual(expected, input.Tick());
    }

    [Fact]
    public void Bigger_matrix()
    {
        var input = MakeGrid([
            [X, X, O, X, X, O, O, O],
            [X, O, X, X, O, O, O, O],
            [X, X, X, O, O, X, X, X],
            [O, O, O, O, O, X, X, O],
            [X, O, O, O, X, X, O, O],
            [X, X, O, O, O, X, X, X],
            [O, O, X, O, X, O, O, X],
            [X, O, O, O, O, O, X, X]
        ]);
        var expected = MakeGrid([
            [X, X, O, X, X, O, O, O],
            [O, O, O, O, O, X, X, O],
            [X, O, X, X, X, X, O, X],
            [X, O, O, O, O, O, O, X],
            [X, X, O, O, X, O, O, X],
            [X, X, O, X, O, O, O, X],
            [X, O, O, O, O, O, O, O],
            [O, O, O, O, O, O, X, X]
        ]);
        AssertGridsEqual(expected, input.Tick());
    }

    [Fact]
    public void Zombie_with_zero_live_neighbors_stays_zombie()
    {
        var input = MakeGrid([[O, O, O], [O, Z, O], [O, O, O]]);
        var expected = MakeGrid([[O, O, O], [O, Z, O], [O, O, O]]);
        AssertGridsEqual(expected, input.Tick());
    }

    [Fact]
    public void Zombie_with_three_live_neighbors_stays_zombie()
    {
        var input = MakeGrid([[X, X, O], [O, Z, O], [X, O, O]]);
        var expected = MakeGrid([[X, X, O], [O, Z, O], [O, O, O]]);
        AssertGridsEqual(expected, input.Tick());
    }

    [Fact]
    public void Zombie_with_four_plus_live_neighbors_stays_zombie()
    {
        var input = MakeGrid([[X, X, X], [X, Z, X], [X, X, X]]);
        var expected = MakeGrid([[X, O, X], [O, Z, O], [X, O, X]]);
        AssertGridsEqual(expected, input.Tick());
    }

    [Fact]
    public void Dead_cell_with_three_zombie_neighbors_becomes_alive()
    {
        var input = MakeGrid([[Z, Z, O], [O, O, O], [Z, O, O]]);
        var expected = MakeGrid([[Z, Z, O], [X, X, O], [Z, O, O]]);
        AssertGridsEqual(expected, input.Tick());
    }

    [Fact]
    public void Live_cell_with_two_zombie_neighbors_stays_alive()
    {
        var input = MakeGrid([[Z, O, O], [O, X, O], [O, O, Z]]);
        var expected = MakeGrid([[Z, O, O], [O, X, O], [O, O, Z]]);
        AssertGridsEqual(expected, input.Tick());
    }

    [Fact]
    public void Live_cell_with_four_plus_zombie_neighbors_dies()
    {
        var input = MakeGrid([[Z, Z, O], [Z, X, O], [Z, O, O]]);
        var expected = MakeGrid([[Z, Z, O], [Z, O, O], [Z, X, O]]);
        AssertGridsEqual(expected, input.Tick());
    }

    [Fact]
    public void Mixed_grid_scenario()
    {
        var input = MakeGrid([
            [O, O, X, O, O, O, Z, O, O],
            [O, O, O, X, O, O, O, O, O],
            [O, X, X, X, O, O, O, O, O],
            [O, O, O, O, O, O, O, O, O],
            [Z, O, O, O, O, O, O, O, Z],
            [O, O, O, O, O, O, O, O, O],
            [O, O, O, O, O, X, X, X, O],
            [O, O, O, O, O, X, O, O, O],
            [O, O, Z, O, O, X, O, Z, O]
        ]);
        var expected = MakeGrid([
            [O, O, O, O, O, O, Z, O, O],
            [O, X, O, X, O, O, O, O, O],
            [O, O, X, X, O, O, O, O, O],
            [O, X, X, O, O, O, O, O, O],
            [Z, O, O, O, O, O, O, O, Z],
            [O, O, O, O, O, O, X, X, O],
            [O, O, O, O, O, X, X, O, O],
            [O, O, O, O, X, X, O, X, O],
            [O, O, Z, O, O, O, X, Z, O]
        ]);
        AssertGridsEqual(expected, input.Tick());
    }
}
