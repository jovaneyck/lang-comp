#[cfg(test)]
mod tests {
    use game_of_life::*;
    use std::collections::HashMap;

    const O: Cell = Cell::Dead;
    const X: Cell = Cell::Alive;
    const Z: Cell = Cell::Zombie;

    fn grid(rows: &[&[Cell]]) -> Grid {
        let height = rows.len() as i32;
        let width = if height == 0 { 0 } else { rows[0].len() as i32 };

        let mut cells = HashMap::new();
        for (r, row) in rows.iter().enumerate() {
            for (c, &cell) in row.iter().enumerate() {
                cells.insert(
                    Position {
                        row: r as i32,
                        col: c as i32,
                    },
                    cell,
                );
            }
        }

        Grid {
            width,
            height,
            cells,
        }
    }

    #[test]
    fn empty_matrix() {
        assert_eq!(tick_grid(&grid(&[])), grid(&[]));
    }

    #[test]
    fn live_cells_with_zero_live_neighbors_die() {
        let input = grid(&[
            &[O, O, O],
            &[O, X, O],
            &[O, O, O],
        ]);
        let expected = grid(&[
            &[O, O, O],
            &[O, O, O],
            &[O, O, O],
        ]);
        assert_eq!(tick_grid(&input), expected);
    }

    #[test]
    fn live_cells_with_only_one_live_neighbor_die() {
        let input = grid(&[
            &[O, O, O],
            &[O, X, O],
            &[O, X, O],
        ]);
        let expected = grid(&[
            &[O, O, O],
            &[O, O, O],
            &[O, O, O],
        ]);
        assert_eq!(tick_grid(&input), expected);
    }

    #[test]
    fn live_cells_with_two_live_neighbors_stay_alive() {
        let input = grid(&[
            &[X, O, X],
            &[X, O, X],
            &[X, O, X],
        ]);
        let expected = grid(&[
            &[O, O, O],
            &[X, O, X],
            &[O, O, O],
        ]);
        assert_eq!(tick_grid(&input), expected);
    }

    #[test]
    fn live_cells_with_three_live_neighbors_stay_alive() {
        let input = grid(&[
            &[O, X, O],
            &[X, O, O],
            &[X, X, O],
        ]);
        let expected = grid(&[
            &[O, O, O],
            &[X, O, O],
            &[X, X, O],
        ]);
        assert_eq!(tick_grid(&input), expected);
    }

    #[test]
    fn dead_cells_with_three_live_neighbors_become_alive() {
        let input = grid(&[
            &[X, X, O],
            &[O, O, O],
            &[X, O, O],
        ]);
        let expected = grid(&[
            &[O, O, O],
            &[X, X, O],
            &[O, O, O],
        ]);
        assert_eq!(tick_grid(&input), expected);
    }

    #[test]
    fn live_cells_with_four_or_more_neighbors_die() {
        let input = grid(&[
            &[X, X, X],
            &[X, X, X],
            &[X, X, X],
        ]);
        let expected = grid(&[
            &[X, O, X],
            &[O, O, O],
            &[X, O, X],
        ]);
        assert_eq!(tick_grid(&input), expected);
    }

    #[test]
    fn bigger_matrix() {
        let input = grid(&[
            &[X, X, O, X, X, O, O, O],
            &[X, O, X, X, O, O, O, O],
            &[X, X, X, O, O, X, X, X],
            &[O, O, O, O, O, X, X, O],
            &[X, O, O, O, X, X, O, O],
            &[X, X, O, O, O, X, X, X],
            &[O, O, X, O, X, O, O, X],
            &[X, O, O, O, O, O, X, X],
        ]);
        let expected = grid(&[
            &[X, X, O, X, X, O, O, O],
            &[O, O, O, O, O, X, X, O],
            &[X, O, X, X, X, X, O, X],
            &[X, O, O, O, O, O, O, X],
            &[X, X, O, O, X, O, O, X],
            &[X, X, O, X, O, O, O, X],
            &[X, O, O, O, O, O, O, O],
            &[O, O, O, O, O, O, X, X],
        ]);
        assert_eq!(tick_grid(&input), expected);
    }

    #[test]
    fn zombie_with_zero_live_neighbors_stays_zombie() {
        let input = grid(&[
            &[O, O, O],
            &[O, Z, O],
            &[O, O, O],
        ]);
        let expected = grid(&[
            &[O, O, O],
            &[O, Z, O],
            &[O, O, O],
        ]);
        assert_eq!(tick_grid(&input), expected);
    }

    #[test]
    fn zombie_with_three_live_neighbors_stays_zombie() {
        let input = grid(&[
            &[X, X, O],
            &[O, Z, O],
            &[X, O, O],
        ]);
        let expected = grid(&[
            &[X, X, O],
            &[O, Z, O],
            &[O, O, O],
        ]);
        assert_eq!(tick_grid(&input), expected);
    }

    #[test]
    fn zombie_with_four_plus_live_neighbors_stays_zombie() {
        let input = grid(&[
            &[X, X, X],
            &[X, Z, X],
            &[X, X, X],
        ]);
        let expected = grid(&[
            &[X, O, X],
            &[O, Z, O],
            &[X, O, X],
        ]);
        assert_eq!(tick_grid(&input), expected);
    }

    #[test]
    fn dead_cell_with_three_zombie_neighbors_becomes_alive() {
        let input = grid(&[
            &[Z, Z, O],
            &[O, O, O],
            &[Z, O, O],
        ]);
        let expected = grid(&[
            &[Z, Z, O],
            &[X, X, O],
            &[Z, O, O],
        ]);
        assert_eq!(tick_grid(&input), expected);
    }

    #[test]
    fn live_cell_with_two_zombie_neighbors_stays_alive() {
        let input = grid(&[
            &[Z, O, O],
            &[O, X, O],
            &[O, O, Z],
        ]);
        let expected = grid(&[
            &[Z, O, O],
            &[O, X, O],
            &[O, O, Z],
        ]);
        assert_eq!(tick_grid(&input), expected);
    }

    #[test]
    fn live_cell_with_four_plus_zombie_neighbors_dies() {
        let input = grid(&[
            &[Z, Z, O],
            &[Z, X, Z],
            &[O, O, O],
        ]);
        let expected = grid(&[
            &[Z, Z, X],
            &[Z, O, Z],
            &[O, X, O],
        ]);
        assert_eq!(tick_grid(&input), expected);
    }

    #[test]
    fn mixed_grid_integration() {
        let input = grid(&[
            &[O, O, X, O, O, O, Z, O, O],
            &[O, O, O, X, O, O, O, O, O],
            &[O, X, X, X, O, O, O, O, O],
            &[O, O, O, O, O, O, O, O, O],
            &[Z, O, O, O, O, O, O, O, Z],
            &[O, O, O, O, O, O, O, O, O],
            &[O, O, O, O, O, X, X, X, O],
            &[O, O, O, O, O, X, O, O, O],
            &[O, O, Z, O, O, X, O, Z, O],
        ]);
        let expected = grid(&[
            &[O, O, O, O, O, O, Z, O, O],
            &[O, X, O, X, O, O, O, O, O],
            &[O, O, X, X, O, O, O, O, O],
            &[O, X, X, O, O, O, O, O, O],
            &[Z, O, O, O, O, O, O, O, Z],
            &[O, O, O, O, O, O, X, X, O],
            &[O, O, O, O, O, X, X, O, O],
            &[O, O, O, O, X, X, O, X, O],
            &[O, O, Z, O, O, O, X, Z, O],
        ]);
        assert_eq!(tick_grid(&input), expected);
    }
}
