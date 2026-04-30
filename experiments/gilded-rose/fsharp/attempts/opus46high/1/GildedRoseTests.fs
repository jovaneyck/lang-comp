module ApprovalTest

open System.Text
open Xunit
open VerifyXunit
open GildedRose

[<Fact>]
let ThirtyDays () =
    let items = ResizeArray<Item>([
        { Name = "+5 Dexterity Vest"; SellIn = 10; Quality = 20 }
        { Name = "Aged Brie"; SellIn = 2; Quality = 0 }
        { Name = "Elixir of the Mongoose"; SellIn = 5; Quality = 7 }
        { Name = "Sulfuras, Hand of Ragnaros"; SellIn = 0; Quality = 80 }
        { Name = "Sulfuras, Hand of Ragnaros"; SellIn = -1; Quality = 80 }
        { Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 15; Quality = 20 }
        { Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 10; Quality = 49 }
        { Name = "Backstage passes to a TAFKAL80ETC concert"; SellIn = 5; Quality = 49 }
        { Name = "Conjured Mana Cake"; SellIn = 3; Quality = 6 }
    ])
    let app = GildedRoseShop(items)
    let output = StringBuilder()
    for day = 0 to 30 do
        output.AppendLine(sprintf "-------- day %d --------" day) |> ignore
        output.AppendLine("name, sellIn, quality") |> ignore
        for i = 0 to items.Count - 1 do
            output.AppendLine(sprintf "%s, %d, %d" items.[i].Name items.[i].SellIn items.[i].Quality) |> ignore
        output.AppendLine() |> ignore
        app.UpdateQuality()
    Verifier.Verify(output.ToString())
