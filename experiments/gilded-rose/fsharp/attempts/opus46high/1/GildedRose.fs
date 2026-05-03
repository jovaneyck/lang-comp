module GildedRose

open System.Collections.Generic

type Item = { Name: string; SellIn: int; Quality: int }

type private ItemCategory =
    | AgedBrie
    | BackstagePass
    | Sulfuras
    | Conjured
    | Normal

let private categorize (name: string) =
    match name with
    | "Aged Brie" -> AgedBrie
    | "Backstage passes to a TAFKAL80ETC concert" -> BackstagePass
    | "Sulfuras, Hand of Ragnaros" -> Sulfuras
    | n when n.StartsWith("Conjured") -> Conjured
    | _ -> Normal

let private clampQuality quality = max 0 (min 50 quality)

let private updateItem (item: Item) =
    match categorize item.Name with
    | Sulfuras -> item
    | AgedBrie ->
        let increase = if item.SellIn <= 0 then 2 else 1
        { item with
            SellIn = item.SellIn - 1
            Quality = clampQuality (item.Quality + increase) }
    | BackstagePass ->
        let quality =
            if item.SellIn <= 0 then 0
            elif item.SellIn <= 5 then item.Quality + 3
            elif item.SellIn <= 10 then item.Quality + 2
            else item.Quality + 1
        { item with
            SellIn = item.SellIn - 1
            Quality = clampQuality quality }
    | Conjured ->
        let degradation = if item.SellIn <= 0 then 4 else 2
        { item with
            SellIn = item.SellIn - 1
            Quality = clampQuality (item.Quality - degradation) }
    | Normal ->
        let degradation = if item.SellIn <= 0 then 2 else 1
        { item with
            SellIn = item.SellIn - 1
            Quality = clampQuality (item.Quality - degradation) }

type GildedRoseShop(items: IList<Item>) =
    member _.Items = items

    member _.UpdateQuality() =
        for i = 0 to items.Count - 1 do
            items.[i] <- updateItem items.[i]
