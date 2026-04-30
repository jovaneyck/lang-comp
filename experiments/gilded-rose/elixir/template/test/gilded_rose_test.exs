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

    assert String.replace(output, "\r\n", "\n") == expected
  end
end
