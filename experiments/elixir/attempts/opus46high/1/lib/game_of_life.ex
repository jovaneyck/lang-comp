defmodule GameOfLife do
  @doc """
  Apply the rules of Conway's Game of Life to a grid of cells
  """

  @spec tick(matrix :: list(list(0 | 1 | :z))) :: list(list(0 | 1 | :z))
  def tick([]), do: []

  def tick(matrix) do
    neighbors = count_neighbors(matrix)

    Enum.zip_with(matrix, neighbors, fn row_matrix, row_neighbor ->
      Enum.zip_with(row_matrix, row_neighbor, &rule/2)
    end)
  end

  defp count_neighbors(matrix) do
    numeric = Enum.map(matrix, fn row ->
      Enum.map(row, fn
        :z -> 1
        cell -> cell
      end)
    end)

    shift_left = shift_left(numeric)
    shift_right = shift_right(numeric)

    shifts = [
      shift_left,
      shift_right,
      shift_up(numeric),
      shift_down(numeric),
      shift_left |> shift_up(),
      shift_left |> shift_down(),
      shift_right |> shift_up(),
      shift_right |> shift_down()
    ]

    Enum.zip_with(shifts, fn rows -> Enum.zip_with(rows, &Enum.sum/1) end)
  end

  defp shift_right(matrix), do: Enum.map(matrix, fn row -> [0 | row] end)
  defp shift_left(matrix), do: Enum.map(matrix, fn row -> Enum.drop(row, 1) ++ [0] end)
  defp shift_up([first | rest]), do: rest ++ [Enum.map(first, fn _ -> 0 end)]
  defp shift_down([first | _] = matrix), do: [Enum.map(first, fn _ -> 0 end) | matrix]

  defp rule(:z, _live_neighbors), do: :z
  defp rule(1, 2), do: 1
  defp rule(1, 3), do: 1
  defp rule(0, 3), do: 1
  defp rule(_cell, _live_neighbors), do: 0
end