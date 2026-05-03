defmodule GildedRoseTest do
  use ExUnit.Case

  test "thirty days golden master" do
    expected =
      File.read!("test/ApprovalTest.ThirtyDays.verified.txt")
      |> String.replace("\r\n", "\n")

    items = [
      %Item{name: "+5 Dexterity Vest", sell_in: 10, quality: 20},
      %Item{name: "Aged Brie", sell_in: 2, quality: 0},
      %Item{name: "Elixir of the Mongoose", sell_in: 5, quality: 7},
      %Item{name: "Sulfuras, Hand of Ragnaros", sell_in: 0, quality: 80},
      %Item{name: "Sulfuras, Hand of Ragnaros", sell_in: -1, quality: 80},
      %Item{name: "Backstage passes to a TAFKAL80ETC concert", sell_in: 15, quality: 20},
      %Item{name: "Backstage passes to a TAFKAL80ETC concert", sell_in: 10, quality: 49},
      %Item{name: "Backstage passes to a TAFKAL80ETC concert", sell_in: 5, quality: 49},
      %Item{name: "Conjured Mana Cake", sell_in: 3, quality: 6}
    ]

    {output, _} =
      Enum.reduce(0..30, {"", items}, fn day, {acc, current_items} ->
        day_output =
          "-------- day #{day} --------\n" <>
            "name, sellIn, quality\n" <>
            Enum.map_join(current_items, "\n", fn item ->
              "#{item.name}, #{item.sell_in}, #{item.quality}"
            end) <> "\n\n"

        updated = GildedRose.update_quality(current_items)
        {acc <> day_output, updated}
      end)

    actual = String.replace(output, "\r\n", "\n")

    if actual != expected do
      {pos, exp_char, act_char} =
        Enum.zip(String.to_charlist(expected), String.to_charlist(actual))
        |> Enum.with_index()
        |> Enum.find_value(fn {{e, a}, i} -> if e != a, do: {i, e, a} end)
        |> case do
          nil -> {min(byte_size(expected), byte_size(actual)), :eof, :eof}
          found -> found
        end

      ctx = 20
      exp_snippet = String.slice(expected, max(pos - ctx, 0), 2 * ctx + 1)
      act_snippet = String.slice(actual, max(pos - ctx, 0), 2 * ctx + 1)

      flunk("""
      First difference at position #{pos}:
        expected char: #{inspect(<<exp_char::utf8>>)}
        actual char:   #{inspect(<<act_char::utf8>>)}

        expected: ...#{inspect(exp_snippet)}...
        actual:   ...#{inspect(act_snippet)}...
      """)
    end
  end

  test "conjured item before sell date" do
    [item] = GildedRose.update_quality([%Item{name: "Conjured Mana Cake", sell_in: 10, quality: 20}])
    assert item.sell_in == 9
    assert item.quality == 18
  end

  test "conjured item on sell date" do
    [item] = GildedRose.update_quality([%Item{name: "Conjured Mana Cake", sell_in: 0, quality: 20}])
    assert item.sell_in == -1
    assert item.quality == 16
  end

  test "conjured item after sell date" do
    [item] = GildedRose.update_quality([%Item{name: "Conjured Mana Cake", sell_in: -5, quality: 10}])
    assert item.sell_in == -6
    assert item.quality == 6
  end

  test "conjured item quality never negative" do
    [item] = GildedRose.update_quality([%Item{name: "Conjured Mana Cake", sell_in: 5, quality: 1}])
    assert item.sell_in == 4
    assert item.quality == 0

    [item] = GildedRose.update_quality([%Item{name: "Conjured Mana Cake", sell_in: 0, quality: 1}])
    assert item.sell_in == -1
    assert item.quality == 0

    [item] = GildedRose.update_quality([%Item{name: "Conjured Mana Cake", sell_in: 0, quality: 3}])
    assert item.sell_in == -1
    assert item.quality == 0
  end
end
