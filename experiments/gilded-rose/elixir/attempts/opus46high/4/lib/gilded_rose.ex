defmodule GildedRose do
  def update_quality(items) do
    Enum.map(items, &update_item/1)
  end

  def update_item(%{name: "Sulfuras, Hand of Ragnaros"} = item), do: item

  def update_item(%{name: "Aged Brie"} = item) do
    item
    |> decrement_sell_in()
    |> increase_quality(1)
    |> apply_expired_bonus(1)
    |> cap_quality()
  end

  def update_item(%{name: "Backstage passes to a TAFKAL80ETC concert"} = item) do
    item
    |> decrement_sell_in()
    |> update_backstage_quality()
    |> cap_quality()
  end

  def update_item(%{name: "Conjured" <> _} = item) do
    item
    |> decrement_sell_in()
    |> decrease_quality(2)
    |> apply_expired_penalty(2)
    |> floor_quality()
  end

  def update_item(item) do
    item
    |> decrement_sell_in()
    |> decrease_quality(1)
    |> apply_expired_penalty(1)
    |> floor_quality()
  end

  defp decrement_sell_in(item), do: %{item | sell_in: item.sell_in - 1}

  defp increase_quality(item, amount), do: %{item | quality: item.quality + amount}

  defp decrease_quality(item, amount), do: %{item | quality: item.quality - amount}

  defp apply_expired_bonus(item, _amount) when item.sell_in >= 0, do: item
  defp apply_expired_bonus(item, amount), do: increase_quality(item, amount)

  defp apply_expired_penalty(item, _amount) when item.sell_in >= 0, do: item
  defp apply_expired_penalty(item, amount), do: decrease_quality(item, amount)

  defp update_backstage_quality(%{sell_in: sell_in} = item) when sell_in < 0 do
    %{item | quality: 0}
  end

  defp update_backstage_quality(%{sell_in: sell_in} = item) when sell_in < 5 do
    increase_quality(item, 3)
  end

  defp update_backstage_quality(%{sell_in: sell_in} = item) when sell_in < 10 do
    increase_quality(item, 2)
  end

  defp update_backstage_quality(item), do: increase_quality(item, 1)

  defp cap_quality(%{quality: q} = item) when q > 50, do: %{item | quality: 50}
  defp cap_quality(item), do: item

  defp floor_quality(%{quality: q} = item) when q < 0, do: %{item | quality: 0}
  defp floor_quality(item), do: item
end
