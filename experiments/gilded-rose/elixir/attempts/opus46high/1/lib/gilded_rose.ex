defmodule GildedRose do
  def update_quality(items) do
    Enum.map(items, &update_item/1)
  end

  def update_item(%Item{name: "Sulfuras, Hand of Ragnaros"} = item), do: item

  def update_item(%Item{name: "Aged Brie"} = item) do
    item
    |> decrease_sell_in()
    |> increase_quality(if(item.sell_in <= 0, do: 2, else: 1))
  end

  def update_item(%Item{name: "Backstage passes to a TAFKAL80ETC concert"} = item) do
    item
    |> decrease_sell_in()
    |> update_backstage_quality()
  end

  def update_item(%Item{name: "Conjured " <> _} = item) do
    item
    |> decrease_sell_in()
    |> decrease_quality(if(item.sell_in <= 0, do: 4, else: 2))
  end

  def update_item(item) do
    item
    |> decrease_sell_in()
    |> decrease_quality(if(item.sell_in <= 0, do: 2, else: 1))
  end

  defp decrease_sell_in(item), do: %{item | sell_in: item.sell_in - 1}

  defp increase_quality(item, amount) do
    %{item | quality: min(item.quality + amount, 50)}
  end

  defp decrease_quality(item, amount) do
    %{item | quality: max(item.quality - amount, 0)}
  end

  defp update_backstage_quality(%Item{sell_in: sell_in} = item) when sell_in < 0 do
    %{item | quality: 0}
  end

  defp update_backstage_quality(%Item{sell_in: sell_in} = item) when sell_in < 5 do
    increase_quality(item, 3)
  end

  defp update_backstage_quality(%Item{sell_in: sell_in} = item) when sell_in < 10 do
    increase_quality(item, 2)
  end

  defp update_backstage_quality(item) do
    increase_quality(item, 1)
  end
end
