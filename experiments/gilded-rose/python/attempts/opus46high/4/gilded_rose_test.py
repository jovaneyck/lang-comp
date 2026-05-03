from approvaltests import verify
from approvaltests.reporters import PythonNativeReporter
from approvaltests.core.options import Options

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

    verify("\n".join(lines) + "\n", options=Options().with_reporter(PythonNativeReporter()))


def _update_once(name, sell_in, quality):
    item = Item(name, sell_in, quality)
    GildedRose([item]).update_quality()
    return item


def test_conjured_before_sell_date():
    item = _update_once("Conjured Mana Cake", 10, 20)
    assert item.sell_in == 9
    assert item.quality == 18


def test_conjured_on_sell_date():
    item = _update_once("Conjured Mana Cake", 0, 20)
    assert item.sell_in == -1
    assert item.quality == 16


def test_conjured_after_sell_date():
    item = _update_once("Conjured Mana Cake", -5, 10)
    assert item.sell_in == -6
    assert item.quality == 6


def test_conjured_quality_never_negative():
    item = _update_once("Conjured Mana Cake", 5, 1)
    assert item.sell_in == 4
    assert item.quality == 0

    item = _update_once("Conjured Mana Cake", 0, 1)
    assert item.sell_in == -1
    assert item.quality == 0

    item = _update_once("Conjured Mana Cake", 0, 3)
    assert item.sell_in == -1
    assert item.quality == 0
