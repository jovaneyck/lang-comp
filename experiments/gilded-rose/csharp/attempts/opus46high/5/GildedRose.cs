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

public interface IItemUpdater
{
    void Update(Item item);
}

public class NormalUpdater : IItemUpdater
{
    public virtual void Update(Item item)
    {
        Degrade(item);
        item.SellIn--;
        if (item.SellIn < 0)
            Degrade(item);
    }

    protected virtual void Degrade(Item item)
    {
        if (item.Quality > 0)
            item.Quality--;
    }
}

public class ConjuredUpdater : NormalUpdater
{
    protected override void Degrade(Item item)
    {
        if (item.Quality > 0) item.Quality--;
        if (item.Quality > 0) item.Quality--;
    }
}

public class AgedBrieUpdater : IItemUpdater
{
    public void Update(Item item)
    {
        IncreaseQuality(item);
        item.SellIn--;
        if (item.SellIn < 0)
            IncreaseQuality(item);
    }

    private static void IncreaseQuality(Item item)
    {
        if (item.Quality < 50)
            item.Quality++;
    }
}

public class BackstagePassUpdater : IItemUpdater
{
    public void Update(Item item)
    {
        if (item.Quality < 50)
        {
            item.Quality++;
            if (item.SellIn < 11 && item.Quality < 50) item.Quality++;
            if (item.SellIn < 6 && item.Quality < 50) item.Quality++;
        }

        item.SellIn--;

        if (item.SellIn < 0)
            item.Quality = 0;
    }
}

public class SulfurasUpdater : IItemUpdater
{
    public void Update(Item item) { }
}
