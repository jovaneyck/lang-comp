defmodule GameOfLifeTest do
  use ExUnit.Case

  # @tag :pending
  test "empty matrix" do
    assert GameOfLife.tick([]) == []
  end


  test "live cells with zero live neighbors die" do
    matrix = [[0, 0, 0], [0, 1, 0], [0, 0, 0]]
    output = [[0, 0, 0], [0, 0, 0], [0, 0, 0]]

    assert GameOfLife.tick(matrix) == output
  end


  test "live cells with only one live neighbor die" do
    matrix = [[0, 0, 0], [0, 1, 0], [0, 1, 0]]
    output = [[0, 0, 0], [0, 0, 0], [0, 0, 0]]

    assert GameOfLife.tick(matrix) == output
  end


  test "live cells with two live neighbors stay alive" do
    matrix = [[1, 0, 1], [1, 0, 1], [1, 0, 1]]
    output = [[0, 0, 0], [1, 0, 1], [0, 0, 0]]

    assert GameOfLife.tick(matrix) == output
  end


  test "live cells with three live neighbors stay alive" do
    matrix = [[0, 1, 0], [1, 0, 0], [1, 1, 0]]
    output = [[0, 0, 0], [1, 0, 0], [1, 1, 0]]

    assert GameOfLife.tick(matrix) == output
  end


  test "dead cells with three live neighbors become alive" do
    matrix = [[1, 1, 0], [0, 0, 0], [1, 0, 0]]
    output = [[0, 0, 0], [1, 1, 0], [0, 0, 0]]

    assert GameOfLife.tick(matrix) == output
  end


  test "live cells with four or more neighbors die" do
    matrix = [[1, 1, 1], [1, 1, 1], [1, 1, 1]]
    output = [[1, 0, 1], [0, 0, 0], [1, 0, 1]]

    assert GameOfLife.tick(matrix) == output
  end


  test "bigger matrix" do
    matrix = [
      [1, 1, 0, 1, 1, 0, 0, 0],
      [1, 0, 1, 1, 0, 0, 0, 0],
      [1, 1, 1, 0, 0, 1, 1, 1],
      [0, 0, 0, 0, 0, 1, 1, 0],
      [1, 0, 0, 0, 1, 1, 0, 0],
      [1, 1, 0, 0, 0, 1, 1, 1],
      [0, 0, 1, 0, 1, 0, 0, 1],
      [1, 0, 0, 0, 0, 0, 1, 1]
    ]

    output = [
      [1, 1, 0, 1, 1, 0, 0, 0],
      [0, 0, 0, 0, 0, 1, 1, 0],
      [1, 0, 1, 1, 1, 1, 0, 1],
      [1, 0, 0, 0, 0, 0, 0, 1],
      [1, 1, 0, 0, 1, 0, 0, 1],
      [1, 1, 0, 1, 0, 0, 0, 1],
      [1, 0, 0, 0, 0, 0, 0, 0],
      [0, 0, 0, 0, 0, 0, 1, 1]
    ]

    assert GameOfLife.tick(matrix) == output
  end

  test "zombie with 0 live neighbors stays zombie" do
    matrix = [[0, 0, 0], [0, :z, 0], [0, 0, 0]]
    output = [[0, 0, 0], [0, :z, 0], [0, 0, 0]]

    assert GameOfLife.tick(matrix) == output
  end

  test "zombie with 3 live neighbors stays zombie" do
    matrix = [[1, 1, 0], [0, :z, 0], [1, 0, 0]]
    output = [[1, 1, 0], [0, :z, 0], [0, 0, 0]]

    assert GameOfLife.tick(matrix) == output
  end

  test "zombie with 4+ live neighbors stays zombie" do
    matrix = [[1, 1, 1], [1, :z, 0], [0, 0, 0]]
    output = [[1, 0, 1], [1, :z, 1], [0, 0, 0]]

    assert GameOfLife.tick(matrix) == output
  end

  test "dead cell with exactly 3 zombie neighbors becomes alive" do
    matrix = [[:z, :z, 0], [0, 0, 0], [:z, 0, 0]]
    output = [[:z, :z, 0], [1, 1, 0], [:z, 0, 0]]

    assert GameOfLife.tick(matrix) == output
  end

  test "live cell with exactly 2 zombie neighbors stays alive" do
    matrix = [[:z, 0, 0], [0, 1, 0], [0, 0, :z]]
    output = [[:z, 0, 0], [0, 1, 0], [0, 0, :z]]

    assert GameOfLife.tick(matrix) == output
  end

  test "live cell surrounded by zombies (4+) dies" do
    matrix = [[:z, :z, :z], [:z, 1, 0], [0, 0, 0]]
    output = [[:z, :z, :z], [:z, 0, 1], [0, 0, 0]]

    assert GameOfLife.tick(matrix) == output
  end

  test "mixed grid with zombies" do
    matrix = [
      [0, 0, 1, 0, 0, 0, :z, 0, 0],
      [0, 0, 0, 1, 0, 0, 0, 0, 0],
      [0, 1, 1, 1, 0, 0, 0, 0, 0],
      [0, 0, 0, 0, 0, 0, 0, 0, 0],
      [:z, 0, 0, 0, 0, 0, 0, 0, :z],
      [0, 0, 0, 0, 0, 0, 0, 0, 0],
      [0, 0, 0, 0, 0, 1, 1, 1, 0],
      [0, 0, 0, 0, 0, 1, 0, 0, 0],
      [0, 0, :z, 0, 0, 1, 0, :z, 0]
    ]

    output = [
      [0, 0, 0, 0, 0, 0, :z, 0, 0],
      [0, 1, 0, 1, 0, 0, 0, 0, 0],
      [0, 0, 1, 1, 0, 0, 0, 0, 0],
      [0, 1, 1, 0, 0, 0, 0, 0, 0],
      [:z, 0, 0, 0, 0, 0, 0, 0, :z],
      [0, 0, 0, 0, 0, 0, 1, 1, 0],
      [0, 0, 0, 0, 0, 1, 1, 0, 0],
      [0, 0, 0, 0, 1, 1, 0, 1, 0],
      [0, 0, :z, 0, 0, 0, 1, :z, 0]
    ]

    assert GameOfLife.tick(matrix) == output
  end
end
