import { describe, expect, test } from '@jest/globals';
import { GameOfLife, Matrix } from './game-of-life';

describe('Game of Life', () => {
    test('empty matrix', () => {
        const matrix: Matrix = [];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected: Matrix = [];
        expect(game.state()).toEqual(expected);
    });

    test('live cells with zero live neighbors die', () => {
        const matrix: Matrix = [[0, 0, 0], [0, 1, 0], [0, 0, 0]];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected: Matrix = [[0, 0, 0], [0, 0, 0], [0, 0, 0]];
        expect(game.state()).toEqual(expected);
    });

    test('live cells with only one live neighbor die', () => {
        const matrix: Matrix = [[0, 0, 0], [0, 1, 0], [0, 1, 0]];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected: Matrix = [[0, 0, 0], [0, 0, 0], [0, 0, 0]];
        expect(game.state()).toEqual(expected);
    });

    test('live cells with two live neighbors stay alive', () => {
        const matrix: Matrix = [[1, 0, 1], [1, 0, 1], [1, 0, 1]];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected: Matrix = [[0, 0, 0], [1, 0, 1], [0, 0, 0]];
        expect(game.state()).toEqual(expected);
    });

    test('live cells with three live neighbors stay alive', () => {
        const matrix: Matrix = [[0, 1, 0], [1, 0, 0], [1, 1, 0]];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected: Matrix = [[0, 0, 0], [1, 0, 0], [1, 1, 0]];
        expect(game.state()).toEqual(expected);
    });

    test('dead cells with three live neighbors become alive', () => {
        const matrix: Matrix = [[1, 1, 0], [0, 0, 0], [1, 0, 0]];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected: Matrix = [[0, 0, 0], [1, 1, 0], [0, 0, 0]];
        expect(game.state()).toEqual(expected);
    });

    test('live cells with four or more neighbors die', () => {
        const matrix: Matrix = [[1, 1, 1], [1, 1, 1], [1, 1, 1]];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected: Matrix = [[1, 0, 1], [0, 0, 0], [1, 0, 1]];
        expect(game.state()).toEqual(expected);
    });

    test('bigger matrix', () => {
        const matrix: Matrix = [
            [1, 1, 0, 1, 1, 0, 0, 0],
            [1, 0, 1, 1, 0, 0, 0, 0],
            [1, 1, 1, 0, 0, 1, 1, 1],
            [0, 0, 0, 0, 0, 1, 1, 0],
            [1, 0, 0, 0, 1, 1, 0, 0],
            [1, 1, 0, 0, 0, 1, 1, 1],
            [0, 0, 1, 0, 1, 0, 0, 1],
            [1, 0, 0, 0, 0, 0, 1, 1],
        ];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected: Matrix = [
            [1, 1, 0, 1, 1, 0, 0, 0],
            [0, 0, 0, 0, 0, 1, 1, 0],
            [1, 0, 1, 1, 1, 1, 0, 1],
            [1, 0, 0, 0, 0, 0, 0, 1],
            [1, 1, 0, 0, 1, 0, 0, 1],
            [1, 1, 0, 1, 0, 0, 0, 1],
            [1, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 1, 1],
        ];
        expect(game.state()).toEqual(expected);
    });
});
