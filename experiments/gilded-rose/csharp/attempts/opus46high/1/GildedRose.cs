using System;
using System.Collections.Generic;

namespace GildedRose;

public class GildedRoseShop
{
    IList<Item> Items;

    public GildedRoseShop(IList<Item> items)
    {
        this.Items = items;
    }

    public void UpdateQuality()
    {
        foreach (var item in Items)
        {
            GetUpdater(item).Update(item);
        }
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

public interface IItemUpdater
{
    void Update(Item item);
}

public class NormalUpdater : IItemUpdater
{
    public void Update(Item item)
    {
        var degradation = item.SellIn <= 0 ? 2 : 1;
        item.Quality = Math.Max(0, item.Quality - degradation);
        item.SellIn--;
    }
}

public class ConjuredUpdater : IItemUpdater
{
    public void Update(Item item)
    {
        var degradation = item.SellIn <= 0 ? 4 : 2;
        item.Quality = Math.Max(0, item.Quality - degradation);
        item.SellIn--;
    }
}

public class AgedBrieUpdater : IItemUpdater
{
    public void Update(Item item)
    {
        var increase = item.SellIn <= 0 ? 2 : 1;
        item.Quality = Math.Min(50, item.Quality + increase);
        item.SellIn--;
    }
}

public class BackstagePassUpdater : IItemUpdater
{
    public void Update(Item item)
    {
        if (item.SellIn <= 0)
        {
            item.Quality = 0;
        }
        else if (item.SellIn <= 5)
        {
            item.Quality = Math.Min(50, item.Quality + 3);
        }
        else if (item.SellIn <= 10)
        {
            item.Quality = Math.Min(50, item.Quality + 2);
        }
        else
        {
            item.Quality = Math.Min(50, item.Quality + 1);
        }
        item.SellIn--;
    }
}

public class SulfurasUpdater : IItemUpdater
{
    public void Update(Item item)
    {
        // Sulfuras never changes
    }
}
