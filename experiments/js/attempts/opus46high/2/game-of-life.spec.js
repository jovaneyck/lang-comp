import { describe, expect, test } from '@jest/globals';
import { GameOfLife } from './game-of-life';

describe('Game of Life', () => {
    // Empty matrix
    test('empty matrix', () => {
        const matrix = [];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected = [];
        expect(game.state()).toEqual(expected);
    });

    // Live cells with zero live neighbors die
    test('live cells with zero live neighbors die', () => {
        const matrix = [
            [0, 0, 0],
            [0, 1, 0],
            [0, 0, 0],
        ];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected = [
            [0, 0, 0],
            [0, 0, 0],
            [0, 0, 0],
        ];
        expect(game.state()).toEqual(expected);
    });

    // Live cells with only one live neighbor die
    test('live cells with only one live neighbor die', () => {
        const matrix = [
            [0, 0, 0],
            [0, 1, 0],
            [0, 1, 0],
        ];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected = [
            [0, 0, 0],
            [0, 0, 0],
            [0, 0, 0],
        ];
        expect(game.state()).toEqual(expected);
    });

    // Live cells with two live neighbors stay alive
    test('live cells with two live neighbors stay alive', () => {
        const matrix = [
            [1, 0, 1],
            [1, 0, 1],
            [1, 0, 1],
        ];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected = [
            [0, 0, 0],
            [1, 0, 1],
            [0, 0, 0],
        ];
        expect(game.state()).toEqual(expected);
    });

    // Live cells with three live neighbors stay alive
    test('live cells with three live neighbors stay alive', () => {
        const matrix = [
            [0, 1, 0],
            [1, 0, 0],
            [1, 1, 0],
        ];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected = [
            [0, 0, 0],
            [1, 0, 0],
            [1, 1, 0],
        ];
        expect(game.state()).toEqual(expected);
    });

    // Dead cells with three live neighbors become alive
    test('dead cells with three live neighbors become alive', () => {
        const matrix = [
            [1, 1, 0],
            [0, 0, 0],
            [1, 0, 0],
        ];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected = [
            [0, 0, 0],
            [1, 1, 0],
            [0, 0, 0],
        ];
        expect(game.state()).toEqual(expected);
    });

    // Live cells with four or more neighbors die
    test('live cells with four or more neighbors die', () => {
        const matrix = [
            [1, 1, 1],
            [1, 1, 1],
            [1, 1, 1],
        ];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected = [
            [1, 0, 1],
            [0, 0, 0],
            [1, 0, 1],
        ];
        expect(game.state()).toEqual(expected);
    });

    // Zombie immutability - zombie with 0 live neighbors stays zombie
    test('zombie with zero live neighbors stays zombie', () => {
        const matrix = [
            [0, 0, 0],
            [0, 'Z', 0],
            [0, 0, 0],
        ];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected = [
            [0, 0, 0],
            [0, 'Z', 0],
            [0, 0, 0],
        ];
        expect(game.state()).toEqual(expected);
    });

    // Zombie immutability - zombie with 3 live neighbors stays zombie
    test('zombie with three live neighbors stays zombie', () => {
        const matrix = [
            [0, 1, 0],
            [1, 'Z', 0],
            [0, 1, 0],
        ];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected = [
            [1, 1, 0],
            [1, 'Z', 1],
            [1, 1, 0],
        ];
        expect(game.state()).toEqual(expected);
    });

    // Zombie immutability - zombie with 4+ live neighbors stays zombie
    test('zombie with four or more live neighbors stays zombie', () => {
        const matrix = [
            [1, 1, 0],
            [1, 'Z', 1],
            [0, 0, 0],
        ];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected = [
            [1, 0, 1],
            [1, 'Z', 1],
            [0, 1, 0],
        ];
        expect(game.state()).toEqual(expected);
    });

    // Zombie counts as live for neighbor birth rule
    test('dead cell with exactly three zombie neighbors becomes alive', () => {
        const matrix = [
            ['Z', 'Z', 0],
            [0, 0, 0],
            ['Z', 0, 0],
        ];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected = [
            ['Z', 'Z', 0],
            [1, 1, 0],
            ['Z', 0, 0],
        ];
        expect(game.state()).toEqual(expected);
    });

    // Zombie counts as live for neighbor survival
    test('live cell with exactly two zombie neighbors stays alive', () => {
        const matrix = [
            ['Z', 0, 0],
            [0, 1, 0],
            [0, 0, 'Z'],
        ];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected = [
            ['Z', 0, 0],
            [0, 1, 0],
            [0, 0, 'Z'],
        ];
        expect(game.state()).toEqual(expected);
    });

    // Zombie counts as live for overcrowding
    test('live cell surrounded by zombies dies from overcrowding', () => {
        const matrix = [
            ['Z', 'Z', 'Z'],
            ['Z', 1, 'Z'],
            [0, 0, 0],
        ];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected = [
            ['Z', 'Z', 'Z'],
            ['Z', 0, 'Z'],
            [0, 1, 0],
        ];
        expect(game.state()).toEqual(expected);
    });

    // Mixed grid scenario
    test('mixed grid with zombies, alive, and dead cells', () => {
        const matrix = [
            [0, 0, 1, 0, 0, 0, 'Z', 0, 0],
            [0, 0, 0, 1, 0, 0, 0, 0, 0],
            [0, 1, 1, 1, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 0],
            ['Z', 0, 0, 0, 0, 0, 0, 0, 'Z'],
            [0, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 1, 1, 1, 0],
            [0, 0, 0, 0, 0, 1, 0, 0, 0],
            [0, 0, 'Z', 0, 0, 1, 0, 'Z', 0],
        ];
        const game = new GameOfLife(matrix);
        game.tick();
        const expected = [
            [0, 0, 0, 0, 0, 0, 'Z', 0, 0],
            [0, 1, 0, 1, 0, 0, 0, 0, 0],
            [0, 0, 1, 1, 0, 0, 0, 0, 0],
            [0, 1, 1, 0, 0, 0, 0, 0, 0],
            ['Z', 0, 0, 0, 0, 0, 0, 0, 'Z'],
            [0, 0, 0, 0, 0, 0, 1, 1, 0],
            [0, 0, 0, 0, 0, 1, 1, 0, 0],
            [0, 0, 0, 0, 1, 1, 0, 1, 0],
            [0, 0, 'Z', 0, 0, 0, 1, 'Z', 0],
        ];
        expect(game.state()).toEqual(expected);
    });

    // Bigger matrix
    test('bigger matrix', () => {
        const matrix = [
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
        const expected = [
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