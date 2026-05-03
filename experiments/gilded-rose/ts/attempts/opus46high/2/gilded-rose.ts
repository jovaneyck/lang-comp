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

class NormalItemUpdater implements ItemUpdater {
  update(item: Item): void {
    item.sellIn -= 1;
    const degradation = item.sellIn < 0 ? 2 : 1;
    item.quality = Math.max(0, item.quality - degradation);
  }
}

class AgedBrieUpdater implements ItemUpdater {
  update(item: Item): void {
    item.sellIn -= 1;
    const increase = item.sellIn < 0 ? 2 : 1;
    item.quality = Math.min(50, item.quality + increase);
  }
}

class BackstagePassUpdater implements ItemUpdater {
  update(item: Item): void {
    item.sellIn -= 1;
    if (item.sellIn < 0) {
      item.quality = 0;
    } else if (item.sellIn < 5) {
      item.quality = Math.min(50, item.quality + 3);
    } else if (item.sellIn < 10) {
      item.quality = Math.min(50, item.quality + 2);
    } else {
      item.quality = Math.min(50, item.quality + 1);
    }
  }
}

class SulfurasUpdater implements ItemUpdater {
  update(_item: Item): void {
    // Sulfuras never changes
  }
}

class ConjuredItemUpdater implements ItemUpdater {
  update(item: Item): void {
    item.sellIn -= 1;
    const degradation = item.sellIn < 0 ? 4 : 2;
    item.quality = Math.max(0, item.quality - degradation);
  }
}

function getUpdater(item: Item): ItemUpdater {
  switch (item.name) {
    case 'Aged Brie':
      return new AgedBrieUpdater();
    case 'Backstage passes to a TAFKAL80ETC concert':
      return new BackstagePassUpdater();
    case 'Sulfuras, Hand of Ragnaros':
      return new SulfurasUpdater();
    default:
      if (item.name.startsWith('Conjured')) {
        return new ConjuredItemUpdater();
      }
      return new NormalItemUpdater();
  }
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
