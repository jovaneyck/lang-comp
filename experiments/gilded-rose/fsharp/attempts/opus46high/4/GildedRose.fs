module GildedRose

open System.Collections.Generic

type Item =
    { Name: string
      SellIn: int
      Quality: int }

let private clampQuality quality = max 0 (min 50 quality)

let private isConjured (name: string) = name.StartsWith("Conjured")

let private updateItem item =
    match item.Name with
    | "Sulfuras, Hand of Ragnaros" -> item
    | "Aged Brie" ->
        let quality =
            if item.SellIn <= 0 then
                item.Quality + 2
            else
                item.Quality + 1

        { item with
            SellIn = item.SellIn - 1
            Quality = clampQuality quality }
    | "Backstage passes to a TAFKAL80ETC concert" ->
        let quality =
            if item.SellIn <= 0 then
                0
            elif item.SellIn <= 5 then
                item.Quality + 3
            elif item.SellIn <= 10 then
                item.Quality + 2
            else
                item.Quality + 1

        { item with
            SellIn = item.SellIn - 1
            Quality = clampQuality quality }
    | name when isConjured name ->
        let degradation = if item.SellIn <= 0 then 4 else 2

        { item with
            SellIn = item.SellIn - 1
            Quality = clampQuality (item.Quality - degradation) }
    | _ ->
        let degradation = if item.SellIn <= 0 then 2 else 1

        { item with
            SellIn = item.SellIn - 1
            Quality = clampQuality (item.Quality - degradation) }

type GildedRoseShop(items: IList<Item>) =
    member _.Items = items

    member _.UpdateQuality() =
        for i = 0 to items.Count - 1 do
            items.[i] <- updateItem items.[i]
