def _clamp_quality(quality):
    return max(0, min(50, quality))


def _update_normal(item):
    degradation = 1 if item.sell_in > 0 else 2
    item.quality = _clamp_quality(item.quality - degradation)
    item.sell_in -= 1


def _update_aged_brie(item):
    increase = 1 if item.sell_in > 0 else 2
    item.quality = _clamp_quality(item.quality + increase)
    item.sell_in -= 1


def _update_backstage_passes(item):
    if item.sell_in <= 0:
        item.quality = 0
    elif item.sell_in <= 5:
        item.quality = _clamp_quality(item.quality + 3)
    elif item.sell_in <= 10:
        item.quality = _clamp_quality(item.quality + 2)
    else:
        item.quality = _clamp_quality(item.quality + 1)
    item.sell_in -= 1


def _update_sulfuras(item):
    pass


def _update_conjured(item):
    degradation = 2 if item.sell_in > 0 else 4
    item.quality = _clamp_quality(item.quality - degradation)
    item.sell_in -= 1


_UPDATERS = {
    "Aged Brie": _update_aged_brie,
    "Backstage passes to a TAFKAL80ETC concert": _update_backstage_passes,
    "Sulfuras, Hand of Ragnaros": _update_sulfuras,
    "Conjured Mana Cake": _update_conjured,
}


class GildedRose:
    def __init__(self, items):
        self.items = items

    def update_quality(self):
        for item in self.items:
            updater = _UPDATERS.get(item.name, _update_normal)
            updater(item)


class Item:
    def __init__(self, name, sell_in, quality):
        self.name = name
        self.sell_in = sell_in
        self.quality = quality

    def __repr__(self):
        return "%s, %s, %s" % (self.name, self.sell_in, self.quality)
