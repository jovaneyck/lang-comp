export type Cell = 0 | 1;
export type Matrix = Cell[][];

export class GameOfLife {
    #matrix: Matrix;

    constructor(matrix: Matrix) {
        this.#matrix = matrix;
    }

    tick(): void {
        if (this.#matrix.length === 0) {
            return;
        }
        const rows: number = this.#matrix.length;
        const cols: number = this.#matrix[0].length;

        const newMatrix: Matrix = JSON.parse(JSON.stringify(this.#matrix));

        for (let row: number = 0; row < rows; row++) {
            for (let col: number = 0; col < cols; col++) {
                const liveNeighbors: number = this.#countLiveNeighbors(row, col, rows, cols);

                let cell: Cell = this.#matrix[row][col];
                if (cell === 1) {
                    if (liveNeighbors < 2 || liveNeighbors > 3) {
                        cell = 0;
                    }
                } else {
                    if (liveNeighbors === 3) {
                        cell = 1;
                    }
                }

                newMatrix[row][col] = cell;
            }
        }

        this.#matrix = newMatrix;
    }

    #countLiveNeighbors(row: number, col: number, rows: number, cols: number): number {
        let count: number = 0;
        for (let newRow: number = row - 1; newRow <= row + 1; newRow++) {
            for (let newCol: number = col - 1; newCol <= col + 1; newCol++) {
                if (newRow === row && newCol === col) {
                    continue;
                }
                if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols) {
                    count += this.#matrix[newRow][newCol];
                }
            }
        }
        return count;
    }

    state(): Matrix {
        return this.#matrix;
    }
}
