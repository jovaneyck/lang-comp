using System;
using System.Collections.Generic;

namespace GildedRose;

public class GildedRoseShop
{
    private readonly IList<Item> _items;

    public GildedRoseShop(IList<Item> items)
    {
        _items = items;
    }

    public void UpdateQuality()
    {
        foreach (var item in _items)
            GetUpdater(item).Update(item);
    }

    private static IItemUpdater GetUpdater(Item item) => item.Name switch
    {
        "Aged Brie" => new AgedBrieUpdater(),
        "Backstage passes to a TAFKAL80ETC concert" => new BackstagePassUpdater(),
        "Sulfuras, Hand of Ragnaros" => new SulfurasUpdater(),
        _ when item.Name.StartsWith("Conjured", StringComparison.Ordinal) => new ConjuredUpdater(),
        _ => new NormalUpdater()
    };
}

internal interface IItemUpdater
{
    void Update(Item item);
}

internal class NormalUpdater : IItemUpdater
{
    public void Update(Item item)
    {
        var degradation = item.SellIn <= 0 ? 2 : 1;
        item.Quality = Math.Max(0, item.Quality - degradation);
        item.SellIn--;
    }
}

internal class AgedBrieUpdater : IItemUpdater
{
    public void Update(Item item)
    {
        var increase = item.SellIn <= 0 ? 2 : 1;
        item.Quality = Math.Min(50, item.Quality + increase);
        item.SellIn--;
    }
}

internal class BackstagePassUpdater : IItemUpdater
{
    public void Update(Item item)
    {
        if (item.SellIn <= 0)
        {
            item.Quality = 0;
        }
        else
        {
            var increase = item.SellIn <= 5 ? 3 : item.SellIn <= 10 ? 2 : 1;
            item.Quality = Math.Min(50, item.Quality + increase);
        }
        item.SellIn--;
    }
}

internal class SulfurasUpdater : IItemUpdater
{
    public void Update(Item item) { }
}

internal class ConjuredUpdater : IItemUpdater
{
    public void Update(Item item)
    {
        var degradation = item.SellIn <= 0 ? 4 : 2;
        item.Quality = Math.Max(0, item.Quality - degradation);
        item.SellIn--;
    }
}
