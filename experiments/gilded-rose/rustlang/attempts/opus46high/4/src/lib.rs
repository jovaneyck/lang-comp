use std::fmt::{self, Display};

pub struct Item {
    pub name: String,
    pub sell_in: i32,
    pub quality: i32,
}

impl Item {
    pub fn new(name: impl Into<String>, sell_in: i32, quality: i32) -> Item {
        Item {
            name: name.into(),
            sell_in,
            quality,
        }
    }
}

impl Display for Item {
    fn fmt(&self, f: &mut fmt::Formatter<'_>) -> fmt::Result {
        write!(f, "{}, {}, {}", self.name, self.sell_in, self.quality)
    }
}

pub struct GildedRose {
    pub items: Vec<Item>,
}

impl GildedRose {
    pub fn new(items: Vec<Item>) -> GildedRose {
        GildedRose { items }
    }

    pub fn update_quality(&mut self) {
        for item in &mut self.items {
            match item.name.as_str() {
                "Sulfuras, Hand of Ragnaros" => {}
                "Aged Brie" => {
                    item.sell_in -= 1;
                    item.quality += if item.sell_in < 0 { 2 } else { 1 };
                    item.quality = item.quality.min(50);
                }
                "Backstage passes to a TAFKAL80ETC concert" => {
                    item.sell_in -= 1;
                    if item.sell_in < 0 {
                        item.quality = 0;
                    } else {
                        item.quality += match item.sell_in {
                            0..5 => 3,
                            5..10 => 2,
                            _ => 1,
                        };
                        item.quality = item.quality.min(50);
                    }
                }
                name if name.starts_with("Conjured") => {
                    item.sell_in -= 1;
                    let degradation = if item.sell_in < 0 { 4 } else { 2 };
                    item.quality = (item.quality - degradation).max(0);
                }
                _ => {
                    item.sell_in -= 1;
                    let degradation = if item.sell_in < 0 { 2 } else { 1 };
                    item.quality = (item.quality - degradation).max(0);
                }
            }
        }
    }
}
