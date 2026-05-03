class ItemUpdater:
    def update(self, item):
        self._update_quality(item)
        item.sell_in -= 1
        if item.sell_in < 0:
            self._update_quality_expired(item)

    def _update_quality(self, item):
        item.quality = max(0, item.quality - 1)

    def _update_quality_expired(self, item):
        item.quality = max(0, item.quality - 1)


class AgedBrieUpdater(ItemUpdater):
    def _update_quality(self, item):
        item.quality = min(50, item.quality + 1)

    def _update_quality_expired(self, item):
        item.quality = min(50, item.quality + 1)


class BackstagePassUpdater(ItemUpdater):
    def _update_quality(self, item):
        if item.sell_in < 6:
            item.quality = min(50, item.quality + 3)
        elif item.sell_in < 11:
            item.quality = min(50, item.quality + 2)
        else:
            item.quality = min(50, item.quality + 1)

    def _update_quality_expired(self, item):
        item.quality = 0


class SulfurasUpdater(ItemUpdater):
    def update(self, item):
        pass


class ConjuredUpdater(ItemUpdater):
    def _update_quality(self, item):
        item.quality = max(0, item.quality - 2)

    def _update_quality_expired(self, item):
        item.quality = max(0, item.quality - 2)


def _updater_for(item):
    if item.name == "Aged Brie":
        return AgedBrieUpdater()
    if item.name == "Sulfuras, Hand of Ragnaros":
        return SulfurasUpdater()
    if item.name == "Backstage passes to a TAFKAL80ETC concert":
        return BackstagePassUpdater()
    if item.name.startswith("Conjured"):
        return ConjuredUpdater()
    return ItemUpdater()


class GildedRose:
    def __init__(self, items):
        self.items = items

    def update_quality(self):
        for item in self.items:
            _updater_for(item).update(item)


class Item:
    def __init__(self, name, sell_in, quality):
        self.name = name
        self.sell_in = sell_in
        self.quality = quality

    def __repr__(self):
        return "%s, %s, %s" % (self.name, self.sell_in, self.quality)
