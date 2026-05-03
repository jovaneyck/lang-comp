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
    item.quality = Math.max(0, item.quality - 1);
    item.sellIn -= 1;
    if (item.sellIn < 0) {
      item.quality = Math.max(0, item.quality - 1);
    }
  }
}

class AgedBrieUpdater implements ItemUpdater {
  update(item: Item): void {
    item.quality = Math.min(50, item.quality + 1);
    item.sellIn -= 1;
    if (item.sellIn < 0) {
      item.quality = Math.min(50, item.quality + 1);
    }
  }
}

class BackstagePassUpdater implements ItemUpdater {
  update(item: Item): void {
    item.quality = Math.min(50, item.quality + 1);
    if (item.sellIn < 11) {
      item.quality = Math.min(50, item.quality + 1);
    }
    if (item.sellIn < 6) {
      item.quality = Math.min(50, item.quality + 1);
    }
    item.sellIn -= 1;
    if (item.sellIn < 0) {
      item.quality = 0;
    }
  }
}

class SulfurasUpdater implements ItemUpdater {
  update(_item: Item): void { }
}

class ConjuredUpdater implements ItemUpdater {
  update(item: Item): void {
    item.quality = Math.max(0, item.quality - 2);
    item.sellIn -= 1;
    if (item.sellIn < 0) {
      item.quality = Math.max(0, item.quality - 2);
    }
  }
}

function getUpdater(item: Item): ItemUpdater {
  if (item.name === 'Sulfuras, Hand of Ragnaros') return new SulfurasUpdater();
  if (item.name === 'Aged Brie') return new AgedBrieUpdater();
  if (item.name === 'Backstage passes to a TAFKAL80ETC concert') return new BackstagePassUpdater();
  if (item.name.startsWith('Conjured')) return new ConjuredUpdater();
  return new NormalUpdater();
}

export class GildedRose {
  items: Array<Item>;

  constructor(items = [] as Array<Item>) {
    this.items = items;
  }

  updateQuality() {
    for (const item of this.items) {
      getUpdater(item).update(item);
    }
    return this.items;
  }
}
