export class Item {
  name: string;
  sellIn: number;
  quality: number;

  constructor(name: string, sellIn: number, quality: number) {
    this.name = name;
    this.sellIn = sellIn;
    this.quality = quality;
  }
}

interface ItemUpdater {
  update(item: Item): void;
}

class NormalUpdater implements ItemUpdater {
  update(item: Item): void {
    item.sellIn -= 1;
    item.quality -= item.sellIn < 0 ? 2 : 1;
    item.quality = Math.max(item.quality, 0);
  }
}

class AgedBrieUpdater implements ItemUpdater {
  update(item: Item): void {
    item.sellIn -= 1;
    item.quality += item.sellIn < 0 ? 2 : 1;
    item.quality = Math.min(item.quality, 50);
  }
}

class BackstagePassUpdater implements ItemUpdater {
  update(item: Item): void {
    if (item.sellIn <= 0) {
      item.quality = 0;
    } else if (item.sellIn <= 5) {
      item.quality += 3;
    } else if (item.sellIn <= 10) {
      item.quality += 2;
    } else {
      item.quality += 1;
    }
    item.quality = Math.min(item.quality, 50);
    item.sellIn -= 1;
  }
}

class SulfurasUpdater implements ItemUpdater {
  update(_item: Item): void {
    // Sulfuras never changes
  }
}

class ConjuredUpdater implements ItemUpdater {
  update(item: Item): void {
    item.sellIn -= 1;
    item.quality -= item.sellIn < 0 ? 4 : 2;
    item.quality = Math.max(item.quality, 0);
  }
}

function getUpdater(name: string): ItemUpdater {
  if (name === 'Aged Brie') return new AgedBrieUpdater();
  if (name === 'Backstage passes to a TAFKAL80ETC concert') return new BackstagePassUpdater();
  if (name === 'Sulfuras, Hand of Ragnaros') return new SulfurasUpdater();
  if (name.startsWith('Conjured')) return new ConjuredUpdater();
  return new NormalUpdater();
}

export class GildedRose {
  items: Array<Item>;

  constructor(items = [] as Array<Item>) {
    this.items = items;
  }

  updateQuality() {
    for (const item of this.items) {
      getUpdater(item.name).update(item);
    }
    return this.items;
  }
}
