defmodule GildedRose do
  def update_quality(items) do
    Enum.map(items, &update_item/1)
  end

  def update_item(%Item{name: "Sulfuras, Hand of Ragnaros"} = item), do: item

  def update_item(%Item{name: "Aged Brie"} = item) do
    item
    |> decrement_sell_in()
    |> adjust_quality(1)
    |> apply_expired_bonus(1)
  end

  def update_item(%Item{name: "Backstage passes to a TAFKAL80ETC concert"} = item) do
    item
    |> decrement_sell_in()
    |> update_backstage_pass()
  end

  def update_item(%Item{name: "Conjured " <> _rest} = item) do
    item
    |> decrement_sell_in()
    |> adjust_quality(-2)
    |> apply_expired_bonus(-2)
  end

  def update_item(%Item{} = item) do
    item
    |> decrement_sell_in()
    |> adjust_quality(-1)
    |> apply_expired_bonus(-1)
  end

  defp decrement_sell_in(item), do: %{item | sell_in: item.sell_in - 1}

  defp adjust_quality(item, amount) do
    %{item | quality: clamp_quality(item.quality + amount)}
  end

  defp apply_expired_bonus(item, _bonus) when item.sell_in >= 0, do: item

  defp apply_expired_bonus(item, bonus) do
    %{item | quality: clamp_quality(item.quality + bonus)}
  end

  defp update_backstage_pass(%Item{sell_in: sell_in} = item) when sell_in < 0 do
    %{item | quality: 0}
  end

  defp update_backstage_pass(%Item{sell_in: sell_in} = item) when sell_in < 5 do
    %{item | quality: clamp_quality(item.quality + 3)}
  end

  defp update_backstage_pass(%Item{sell_in: sell_in} = item) when sell_in < 10 do
    %{item | quality: clamp_quality(item.quality + 2)}
  end

  defp update_backstage_pass(%Item{} = item) do
    %{item | quality: clamp_quality(item.quality + 1)}
  end

  defp clamp_quality(quality) when quality < 0, do: 0
  defp clamp_quality(quality) when quality > 50, do: 50
  defp clamp_quality(quality), do: quality
end
