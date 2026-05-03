defmodule GildedRose do
  def update_quality(items) do
    Enum.map(items, &update_item/1)
  end

  def update_item(%Item{name: "Sulfuras, Hand of Ragnaros"} = item), do: item

  def update_item(%Item{name: "Aged Brie"} = item) do
    item
    |> decrement_sell_in()
    |> increase_quality(1)
    |> apply_expired_bonus(1)
    |> cap_quality()
  end

  def update_item(%Item{name: "Backstage passes to a TAFKAL80ETC concert"} = item) do
    item
    |> increase_quality(1)
    |> increase_quality(if item.sell_in < 11, do: 1, else: 0)
    |> increase_quality(if item.sell_in < 6, do: 1, else: 0)
    |> decrement_sell_in()
    |> expire_backstage_pass()
    |> cap_quality()
  end

  def update_item(%Item{name: "Conjured" <> _} = item) do
    item
    |> decrement_sell_in()
    |> decrease_quality(2)
    |> apply_expired_penalty(2)
    |> floor_quality()
  end

  def update_item(%Item{} = item) do
    item
    |> decrement_sell_in()
    |> decrease_quality(1)
    |> apply_expired_penalty(1)
    |> floor_quality()
  end

  defp decrement_sell_in(item), do: %{item | sell_in: item.sell_in - 1}

  defp increase_quality(item, amount) do
    %{item | quality: item.quality + amount}
  end

  defp decrease_quality(item, amount) do
    %{item | quality: item.quality - amount}
  end

  defp apply_expired_bonus(item, bonus) do
    if item.sell_in < 0, do: increase_quality(item, bonus), else: item
  end

  defp apply_expired_penalty(item, penalty) do
    if item.sell_in < 0, do: decrease_quality(item, penalty), else: item
  end

  defp expire_backstage_pass(item) do
    if item.sell_in < 0, do: %{item | quality: 0}, else: item
  end

  defp cap_quality(item), do: %{item | quality: min(item.quality, 50)}
  defp floor_quality(item), do: %{item | quality: max(item.quality, 0)}
end
