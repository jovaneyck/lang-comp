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

const clampQuality = (quality: number): number => Math.max(0, Math.min(50, quality));

const normalUpdater: ItemUpdater = {
  update(item: Item) {
    const degradation = item.sellIn <= 0 ? 2 : 1;
    item.quality = clampQuality(item.quality - degradation);
    item.sellIn -= 1;
  },
};

const agedBrieUpdater: ItemUpdater = {
  update(item: Item) {
    const increase = item.sellIn <= 0 ? 2 : 1;
    item.quality = clampQuality(item.quality + increase);
    item.sellIn -= 1;
  },
};

const sulfurasUpdater: ItemUpdater = {
  update(_item: Item) { },
};

const backstagePassUpdater: ItemUpdater = {
  update(item: Item) {
    if (item.sellIn <= 0) {
      item.quality = 0;
    } else if (item.sellIn <= 5) {
      item.quality = clampQuality(item.quality + 3);
    } else if (item.sellIn <= 10) {
      item.quality = clampQuality(item.quality + 2);
    } else {
      item.quality = clampQuality(item.quality + 1);
    }
    item.sellIn -= 1;
  },
};

const conjuredUpdater: ItemUpdater = {
  update(item: Item) {
    const degradation = item.sellIn <= 0 ? 4 : 2;
    item.quality = clampQuality(item.quality - degradation);
    item.sellIn -= 1;
  },
};

function getUpdater(item: Item): ItemUpdater {
  if (item.name === 'Sulfuras, Hand of Ragnaros') return sulfurasUpdater;
  if (item.name === 'Aged Brie') return agedBrieUpdater;
  if (item.name === 'Backstage passes to a TAFKAL80ETC concert') return backstagePassUpdater;
  if (item.name.startsWith('Conjured')) return conjuredUpdater;
  return normalUpdater;
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
