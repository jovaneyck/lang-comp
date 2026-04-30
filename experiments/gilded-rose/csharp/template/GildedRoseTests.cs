using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using VerifyXunit;

namespace GildedRose;

public class ApprovalTest
{
    [Fact]
    public Task ThirtyDays()
    {
        var items = new List<Item>
        {
            new() { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
            new() { Name = "Aged Brie", SellIn = 2, Quality = 0 },
            new() { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
            new() { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
            new() { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },
            new() { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 },
            new() { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 49 },
            new() { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 49 },
            new() { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 },
        };
        var app = new GildedRoseShop(items);
        var output = new StringBuilder();

        for (var day = 0; day <= 30; day++)
        {
            output.AppendLine($"-------- day {day} --------");
            output.AppendLine("name, sellIn, quality");
            foreach (var item in items)
            {
                output.AppendLine($"{item.Name}, {item.SellIn}, {item.Quality}");
            }
            output.AppendLine();
            app.UpdateQuality();
        }

        return Verifier.Verify(output.ToString());
    }
}
