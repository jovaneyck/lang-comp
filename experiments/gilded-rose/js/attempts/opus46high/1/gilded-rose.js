export class Item {
  constructor(name, sellIn, quality) {
    this.name = name;
    this.sellIn = sellIn;
    this.quality = quality;
  }
}

function clampQuality(quality) {
  return Math.max(0, Math.min(50, quality));
}

function updateNormal(item) {
  const degradation = item.sellIn <= 0 ? 2 : 1;
  item.quality = clampQuality(item.quality - degradation);
  item.sellIn -= 1;
}

function updateAgedBrie(item) {
  const increase = item.sellIn <= 0 ? 2 : 1;
  item.quality = clampQuality(item.quality + increase);
  item.sellIn -= 1;
}

function updateBackstagePass(item) {
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
}

function updateSulfuras() { }

function updateConjured(item) {
  const degradation = item.sellIn <= 0 ? 4 : 2;
  item.quality = clampQuality(item.quality - degradation);
  item.sellIn -= 1;
}

function getUpdater(name) {
  if (name === 'Aged Brie') return updateAgedBrie;
  if (name === 'Backstage passes to a TAFKAL80ETC concert') return updateBackstagePass;
  if (name === 'Sulfuras, Hand of Ragnaros') return updateSulfuras;
  if (name.startsWith('Conjured')) return updateConjured;
  return updateNormal;
}

export class Shop {
  constructor(items = []) {
    this.items = items;
  }

  updateQuality() {
    for (const item of this.items) {
      getUpdater(item.name)(item);
    }
    return this.items;
  }
}
