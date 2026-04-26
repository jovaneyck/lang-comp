import { describe, expect, test } from '@jest/globals';
import { Cell, GameOfLife, Matrix } from './game-of-life';

const O: Cell = Cell.Dead;
const X: Cell = Cell.Alive;

describe('Game of Life', () => {
    test('empty matrix', () => {
        const matrix: Matrix = [];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected: Matrix = [];
        expect(game.state()).toEqual(expected);
    });

    test('live cells with zero live neighbors die', () => {
        const matrix: Matrix = [[O, O, O], [O, X, O], [O, O, O]];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected: Matrix = [[O, O, O], [O, O, O], [O, O, O]];
        expect(game.state()).toEqual(expected);
    });

    test('live cells with only one live neighbor die', () => {
        const matrix: Matrix = [[O, O, O], [O, X, O], [O, X, O]];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected: Matrix = [[O, O, O], [O, O, O], [O, O, O]];
        expect(game.state()).toEqual(expected);
    });

    test('live cells with two live neighbors stay alive', () => {
        const matrix: Matrix = [[X, O, X], [X, O, X], [X, O, X]];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected: Matrix = [[O, O, O], [X, O, X], [O, O, O]];
        expect(game.state()).toEqual(expected);
    });

    test('live cells with three live neighbors stay alive', () => {
        const matrix: Matrix = [[O, X, O], [X, O, O], [X, X, O]];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected: Matrix = [[O, O, O], [X, O, O], [X, X, O]];
        expect(game.state()).toEqual(expected);
    });

    test('dead cells with three live neighbors become alive', () => {
        const matrix: Matrix = [[X, X, O], [O, O, O], [X, O, O]];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected: Matrix = [[O, O, O], [X, X, O], [O, O, O]];
        expect(game.state()).toEqual(expected);
    });

    test('live cells with four or more neighbors die', () => {
        const matrix: Matrix = [[X, X, X], [X, X, X], [X, X, X]];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected: Matrix = [[X, O, X], [O, O, O], [X, O, X]];
        expect(game.state()).toEqual(expected);
    });

    test('bigger matrix', () => {
        const matrix: Matrix = [
            [X, X, O, X, X, O, O, O],
            [X, O, X, X, O, O, O, O],
            [X, X, X, O, O, X, X, X],
            [O, O, O, O, O, X, X, O],
            [X, O, O, O, X, X, O, O],
            [X, X, O, O, O, X, X, X],
            [O, O, X, O, X, O, O, X],
            [X, O, O, O, O, O, X, X],
        ];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected: Matrix = [
            [X, X, O, X, X, O, O, O],
            [O, O, O, O, O, X, X, O],
            [X, O, X, X, X, X, O, X],
            [X, O, O, O, O, O, O, X],
            [X, X, O, O, X, O, O, X],
            [X, X, O, X, O, O, O, X],
            [X, O, O, O, O, O, O, O],
            [O, O, O, O, O, O, X, X],
        ];
        expect(game.state()).toEqual(expected);
    });
});
