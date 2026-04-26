export class GameOfLife {
    #matrix;

    constructor(matrix) {
        this.#matrix = matrix;
    }

    tick() {
        if (this.#matrix.length === 0) {
            return;
        }
        const rows = this.#matrix.length;
        const cols = this.#matrix[0].length;

        const newMatrix = JSON.parse(JSON.stringify(this.#matrix));

        for (let row = 0; row < rows; row++) {
            for (let col = 0; col < cols; col++) {
                var cell = this.#matrix[row][col];

                if (cell === 'Z') {
                    newMatrix[row][col] = 'Z';
                    continue;
                }

                let liveNeighbors = 0;
                for (let newRow = row - 1; newRow <= row + 1; newRow++) {
                    for (let newCol = col - 1; newCol <= col + 1; newCol++) {
                        if (newRow === row && newCol === col) {
                            continue;
                        }
                        if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols) {
                            const neighbor = this.#matrix[newRow][newCol];
                            if (neighbor === 1 || neighbor === 'Z') {
                                liveNeighbors++;
                            }
                        }
                    }
                }

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

    state() {
        return this.#matrix;
    }
}