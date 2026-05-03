import { describe, it, expect } from '@jest/globals';
import { Item, GildedRose } from './gilded-rose';

describe('Gilded Rose', () => {
  describe('Conjured items', () => {
    it('degrades quality by 2 before sell date', () => {
      const item = new Item('Conjured Mana Cake', 10, 20);
      new GildedRose([item]).updateQuality();
      expect(item.sellIn).toBe(9);
      expect(item.quality).toBe(18);
    });

    it('degrades quality by 4 on sell date', () => {
      const item = new Item('Conjured Mana Cake', 0, 20);
      new GildedRose([item]).updateQuality();
      expect(item.sellIn).toBe(-1);
      expect(item.quality).toBe(16);
    });

    it('degrades quality by 4 after sell date', () => {
      const item = new Item('Conjured Mana Cake', -5, 10);
      new GildedRose([item]).updateQuality();
      expect(item.sellIn).toBe(-6);
      expect(item.quality).toBe(6);
    });

    it('quality never goes negative', () => {
      const item = new Item('Conjured Mana Cake', 5, 1);
      new GildedRose([item]).updateQuality();
      expect(item.sellIn).toBe(4);
      expect(item.quality).toBe(0);
    });

    it('quality never goes negative after sell date', () => {
      const item = new Item('Conjured Mana Cake', 0, 1);
      new GildedRose([item]).updateQuality();
      expect(item.sellIn).toBe(-1);
      expect(item.quality).toBe(0);
    });

    it('quality never goes negative after sell date with quality 3', () => {
      const item = new Item('Conjured Mana Cake', 0, 3);
      new GildedRose([item]).updateQuality();
      expect(item.sellIn).toBe(-1);
      expect(item.quality).toBe(0);
    });
  });

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
