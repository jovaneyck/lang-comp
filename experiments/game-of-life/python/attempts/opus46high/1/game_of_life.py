def tick(matrix):
    if not matrix:
        return []

    rows = len(matrix)
    cols = len(matrix[0])

    def count_live_neighbors(r, c):
        count = 0
        for dr, dc in [
            (-1, -1), (-1, 0), (-1, 1),
            (0, -1),           (0, 1),
            (1, -1),  (1, 0),  (1, 1),
        ]:
            nr, nc = r + dr, c + dc
            if 0 <= nr < rows and 0 <= nc < cols:
                if matrix[nr][nc] == 1 or matrix[nr][nc] == "Z":
                    count += 1
        return count

    new_matrix = []
    for r in range(rows):
        new_row = []
        for c in range(cols):
            current = matrix[r][c]
            if current == "Z":
                new_row.append("Z")
            else:
                neighbors = count_live_neighbors(r, c)
                state = 0
                if current == 1 and (neighbors == 2 or neighbors == 3):
                    state = 1
                elif current == 0 and neighbors == 3:
                    state = 1
                new_row.append(state)
        new_matrix.append(new_row)

    return new_matrix