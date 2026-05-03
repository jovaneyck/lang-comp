module GildedRose

open System.Collections.Generic

type Item = { Name: string; SellIn: int; Quality: int }

type GildedRoseShop(items: IList<Item>) =
    member _.Items = items

    member _.UpdateQuality() =
        for i = 0 to items.Count - 1 do
            if items.[i].Name <> "Aged Brie" && items.[i].Name <> "Backstage passes to a TAFKAL80ETC concert" then
                if items.[i].Quality > 0 then
                    if items.[i].Name <> "Sulfuras, Hand of Ragnaros" then
                        items.[i] <- { items.[i] with Quality = items.[i].Quality - 1 }
            else
                if items.[i].Quality < 50 then
                    items.[i] <- { items.[i] with Quality = items.[i].Quality + 1 }
                    if items.[i].Name = "Backstage passes to a TAFKAL80ETC concert" then
                        if items.[i].SellIn < 11 then
                            if items.[i].Quality < 50 then
                                items.[i] <- { items.[i] with Quality = items.[i].Quality + 1 }
                        if items.[i].SellIn < 6 then
                            if items.[i].Quality < 50 then
                                items.[i] <- { items.[i] with Quality = items.[i].Quality + 1 }
            if items.[i].Name <> "Sulfuras, Hand of Ragnaros" then
                items.[i] <- { items.[i] with SellIn = items.[i].SellIn - 1 }
            if items.[i].SellIn < 0 then
                if items.[i].Name <> "Aged Brie" then
                    if items.[i].Name <> "Backstage passes to a TAFKAL80ETC concert" then
                        if items.[i].Quality > 0 then
                            if items.[i].Name <> "Sulfuras, Hand of Ragnaros" then
                                items.[i] <- { items.[i] with Quality = items.[i].Quality - 1 }
                    else
                        items.[i] <- { items.[i] with Quality = items.[i].Quality - items.[i].Quality }
                else
                    if items.[i].Quality < 50 then
                        items.[i] <- { items.[i] with Quality = items.[i].Quality + 1 }
        ()
