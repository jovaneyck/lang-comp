export class Item {
  constructor(name, sellIn, quality) {
    this.name = name;
    this.sellIn = sellIn;
    this.quality = quality;
  }
}

const clampQuality = (quality) => Math.max(0, Math.min(50, quality));

const updaters = {
  'Sulfuras, Hand of Ragnaros': (item) => item,

  'Aged Brie': (item) => {
    item.sellIn -= 1;
    const increase = item.sellIn < 0 ? 2 : 1;
    item.quality = clampQuality(item.quality + increase);
    return item;
  },

  'Backstage passes to a TAFKAL80ETC concert': (item) => {
    item.sellIn -= 1;
    if (item.sellIn < 0) {
      item.quality = 0;
    } else if (item.sellIn < 5) {
      item.quality = clampQuality(item.quality + 3);
    } else if (item.sellIn < 10) {
      item.quality = clampQuality(item.quality + 2);
    } else {
      item.quality = clampQuality(item.quality + 1);
    }
    return item;
  },
};

const isConjured = (name) => name.startsWith('Conjured');

const updateNormal = (item) => {
  item.sellIn -= 1;
  const degradation = item.sellIn < 0 ? 2 : 1;
  item.quality = clampQuality(item.quality - degradation);
  return item;
};

const updateConjured = (item) => {
  item.sellIn -= 1;
  const degradation = item.sellIn < 0 ? 4 : 2;
  item.quality = clampQuality(item.quality - degradation);
  return item;
};

const updateItem = (item) => {
  const updater = updaters[item.name];
  if (updater) return updater(item);
  if (isConjured(item.name)) return updateConjured(item);
  return updateNormal(item);
};

export class Shop {
  constructor(items = []) {
    this.items = items;
  }

  updateQuality() {
    this.items.forEach(updateItem);
    return this.items;
  }
}
