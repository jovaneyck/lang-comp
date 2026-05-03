defmodule GildedRose do
  def update_quality(items) do
    Enum.map(items, &update_item/1)
  end

  defp update_item(%Item{name: "Sulfuras, Hand of Ragnaros"} = item), do: item

  defp update_item(%Item{name: "Aged Brie"} = item) do
    item
    |> decrement_sell_in()
    |> increase_quality(if(item.sell_in <= 0, do: 2, else: 1))
  end

  defp update_item(%Item{name: "Backstage passes to a TAFKAL80ETC concert"} = item) do
    item = decrement_sell_in(item)

    cond do
      item.sell_in < 0 -> %{item | quality: 0}
      item.sell_in < 5 -> increase_quality(item, 3)
      item.sell_in < 10 -> increase_quality(item, 2)
      true -> increase_quality(item, 1)
    end
  end

  defp update_item(%Item{name: "Conjured " <> _rest} = item) do
    item
    |> decrement_sell_in()
    |> degrade_quality(if(item.sell_in <= 0, do: 4, else: 2))
  end

  defp update_item(item) do
    item
    |> decrement_sell_in()
    |> degrade_quality(if(item.sell_in <= 0, do: 2, else: 1))
  end

  defp decrement_sell_in(item), do: %{item | sell_in: item.sell_in - 1}

  defp increase_quality(item, amount) do
    %{item | quality: min(item.quality + amount, 50)}
  end

  defp degrade_quality(item, amount) do
    %{item | quality: max(item.quality - amount, 0)}
  end
end
