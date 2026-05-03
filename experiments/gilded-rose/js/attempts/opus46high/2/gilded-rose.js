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
  item.sellIn -= 1;
  const degradation = item.sellIn < 0 ? 2 : 1;
  item.quality = clampQuality(item.quality - degradation);
}

function updateAgedBrie(item) {
  item.sellIn -= 1;
  const increase = item.sellIn < 0 ? 2 : 1;
  item.quality = clampQuality(item.quality + increase);
}

function updateBackstagePasses(item) {
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
}

function updateSulfuras() { }

function updateConjured(item) {
  item.sellIn -= 1;
  const degradation = item.sellIn < 0 ? 4 : 2;
  item.quality = clampQuality(item.quality - degradation);
}

function updaterFor(item) {
  switch (item.name) {
    case 'Aged Brie': return updateAgedBrie;
    case 'Backstage passes to a TAFKAL80ETC concert': return updateBackstagePasses;
    case 'Sulfuras, Hand of Ragnaros': return updateSulfuras;
    default:
      if (item.name.startsWith('Conjured')) return updateConjured;
      return updateNormal;
  }
}

export class Shop {
  constructor(items = []) {
    this.items = items;
  }

  updateQuality() {
    for (const item of this.items) {
      updaterFor(item)(item);
    }
    return this.items;
  }
}
