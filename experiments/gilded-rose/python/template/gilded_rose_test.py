from approvaltests import verify

from gilded_rose import GildedRose, Item


def test_thirty_days():
    items = [
        Item("+5 Dexterity Vest", 10, 20),
        Item("Aged Brie", 2, 0),
        Item("Elixir of the Mongoose", 5, 7),
        Item("Sulfuras, Hand of Ragnaros", 0, 80),
        Item("Sulfuras, Hand of Ragnaros", -1, 80),
        Item("Backstage passes to a TAFKAL80ETC concert", 15, 20),
        Item("Backstage passes to a TAFKAL80ETC concert", 10, 49),
        Item("Backstage passes to a TAFKAL80ETC concert", 5, 49),
        Item("Conjured Mana Cake", 3, 6),
    ]

    app = GildedRose(items)
    lines = []

    for day in range(31):
        lines.append(f"-------- day {day} --------")
        lines.append("name, sellIn, quality")
        for item in items:
            lines.append(f"{item.name}, {item.sell_in}, {item.quality}")
        lines.append("")
        app.update_quality()

    verify("\n".join(lines) + "\n")
