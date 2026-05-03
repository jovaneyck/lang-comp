defmodule GildedRose do
  def update_quality(items) do
    Enum.map(items, &update_item/1)
  end

  def update_item(%Item{name: "Sulfuras, Hand of Ragnaros"} = item), do: item

  def update_item(%Item{name: "Aged Brie"} = item) do
    increase = if item.sell_in <= 0, do: 2, else: 1

    item
    |> decrease_sell_in()
    |> increase_quality(increase)
  end

  def update_item(%Item{name: "Backstage passes to a TAFKAL80ETC concert"} = item) do
    increase =
      cond do
        item.sell_in <= 0 -> :drop
        item.sell_in <= 5 -> 3
        item.sell_in <= 10 -> 2
        true -> 1
      end

    item = decrease_sell_in(item)

    case increase do
      :drop -> %{item | quality: 0}
      n -> increase_quality(item, n)
    end
  end

  def update_item(%Item{name: "Conjured " <> _} = item) do
    degradation = if item.sell_in <= 0, do: 4, else: 2

    item
    |> decrease_sell_in()
    |> decrease_quality(degradation)
  end

  def update_item(item) do
    degradation = if item.sell_in <= 0, do: 2, else: 1

    item
    |> decrease_sell_in()
    |> decrease_quality(degradation)
  end

  defp decrease_sell_in(item), do: %{item | sell_in: item.sell_in - 1}

  defp increase_quality(item, amount) do
    %{item | quality: min(item.quality + amount, 50)}
  end

  defp decrease_quality(item, amount) do
    %{item | quality: max(item.quality - amount, 0)}
  end
end
