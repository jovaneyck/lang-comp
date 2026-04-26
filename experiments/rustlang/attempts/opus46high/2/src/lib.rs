use std::collections::HashMap;

#[derive(Debug, Clone, Copy, PartialEq, Eq)]
pub enum Cell {
    Alive,
    Dead,
    Zombie,
}

#[derive(Debug, Clone, Copy, PartialEq, Eq, Hash)]
pub struct Position {
    pub row: i32,
    pub col: i32,
}

#[derive(Debug, Clone, PartialEq, Eq)]
pub struct Grid {
    pub width: i32,
    pub height: i32,
    pub cells: HashMap<Position, Cell>,
}

pub fn cell_at(grid: &Grid, position: &Position) -> Option<Cell> {
    grid.cells.get(position).copied()
}

pub fn count_live_neighbors(grid: &Grid, position: &Position) -> i32 {
    let offsets = [
        (-1, -1), (-1, 0), (-1, 1),
        ( 0, -1),          ( 0, 1),
        ( 1, -1), ( 1, 0), ( 1, 1),
    ];

    offsets
        .iter()
        .filter(|(dr, dc)| {
            let neighbor = Position {
                row: position.row + dr,
                col: position.col + dc,
            };
            matches!(cell_at(grid, &neighbor), Some(Cell::Alive) | Some(Cell::Zombie))
        })
        .count() as i32
}

pub fn evolve_cell(cell: Cell, live_neighbors: i32) -> Cell {
    match (cell, live_neighbors) {
        (Cell::Zombie, _) => Cell::Zombie,
        (Cell::Alive, 2) | (Cell::Alive, 3) => Cell::Alive,
        (Cell::Dead, 3) => Cell::Alive,
        _ => Cell::Dead,
    }
}

pub fn tick_grid(grid: &Grid) -> Grid {
    let mut cells = HashMap::new();

    for row in 0..grid.height {
        for col in 0..grid.width {
            let pos = Position { row, col };
            let current = cell_at(grid, &pos).unwrap_or(Cell::Dead);
            let neighbors = count_live_neighbors(grid, &pos);
            cells.insert(pos, evolve_cell(current, neighbors));
        }
    }

    Grid {
        width: grid.width,
        height: grid.height,
        cells,
    }
}
