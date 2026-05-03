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
            GetStrategy(item).Update(item);
    }

    private static IItemStrategy GetStrategy(Item item) => item.Name switch
    {
        "Sulfuras, Hand of Ragnaros" => new SulfurasStrategy(),
        "Aged Brie" => new AgedBrieStrategy(),
        "Backstage passes to a TAFKAL80ETC concert" => new BackstagePassStrategy(),
        var name when name.StartsWith("Conjured", StringComparison.Ordinal) => new ConjuredStrategy(),
        _ => new NormalStrategy()
    };
}

public interface IItemStrategy
{
    void Update(Item item);
}

public class SulfurasStrategy : IItemStrategy
{
    public void Update(Item item) { }
}

public class AgedBrieStrategy : IItemStrategy
{
    public void Update(Item item)
    {
        item.SellIn--;
        item.Quality = Math.Min(50, item.Quality + (item.SellIn < 0 ? 2 : 1));
    }
}

public class BackstagePassStrategy : IItemStrategy
{
    public void Update(Item item)
    {
        item.SellIn--;
        if (item.SellIn < 0)
        {
            item.Quality = 0;
            return;
        }
        var increase = item.SellIn < 5 ? 3 : item.SellIn < 10 ? 2 : 1;
        item.Quality = Math.Min(50, item.Quality + increase);
    }
}

public class NormalStrategy : IItemStrategy
{
    public void Update(Item item)
    {
        item.SellIn--;
        var degradation = item.SellIn < 0 ? 2 : 1;
        item.Quality = Math.Max(0, item.Quality - degradation);
    }
}

public class ConjuredStrategy : IItemStrategy
{
    public void Update(Item item)
    {
        item.SellIn--;
        var degradation = item.SellIn < 0 ? 4 : 2;
        item.Quality = Math.Max(0, item.Quality - degradation);
    }
}
