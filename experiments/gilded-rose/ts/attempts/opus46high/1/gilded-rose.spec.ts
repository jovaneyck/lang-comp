import { describe, it, expect } from '@jest/globals';
import { Item, GildedRose } from './gilded-rose';

describe('Gilded Rose', () => {
  it('thirty days golden master', () => {
    const items = [
      new Item('+5 Dexterity Vest', 10, 20),
      new Item('Aged Brie', 2, 0),
      new Item('Elixir of the Mongoose', 5, 7),
      new Item('Sulfuras, Hand of Ragnaros', 0, 80),
      new Item('Sulfuras, Hand of Ragnaros', -1, 80),
      new Item('Backstage passes to a TAFKAL80ETC concert', 15, 20),
      new Item('Backstage passes to a TAFKAL80ETC concert', 10, 49),
      new Item('Backstage passes to a TAFKAL80ETC concert', 5, 49),
      new Item('Conjured Mana Cake', 3, 6),
    ];

    const shop = new GildedRose(items);
    const lines: string[] = [];

    for (let day = 0; day <= 30; day++) {
      lines.push(`-------- day ${day} --------`);
      lines.push('name, sellIn, quality');
      for (const item of items) {
        lines.push(`${item.name}, ${item.sellIn}, ${item.quality}`);
      }
      lines.push('');
      shop.updateQuality();
    }

    expect(lines.join('\n')).toMatchSnapshot();
  });
});
