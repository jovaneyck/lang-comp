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
        for item in self.items.iter_mut() {
            match item.name.as_str() {
                "Sulfuras, Hand of Ragnaros" => {}
                "Aged Brie" => update_aged_brie(item),
                "Backstage passes to a TAFKAL80ETC concert" => update_backstage_pass(item),
                name if name.starts_with("Conjured") => update_conjured(item),
                _ => update_normal(item),
            }
        }
    }
}

fn clamp_quality(quality: i32) -> i32 {
    quality.clamp(0, 50)
}

fn update_normal(item: &mut Item) {
    let degradation = if item.sell_in <= 0 { 2 } else { 1 };
    item.quality = clamp_quality(item.quality - degradation);
    item.sell_in -= 1;
}

fn update_conjured(item: &mut Item) {
    let degradation = if item.sell_in <= 0 { 4 } else { 2 };
    item.quality = clamp_quality(item.quality - degradation);
    item.sell_in -= 1;
}

fn update_aged_brie(item: &mut Item) {
    let increase = if item.sell_in <= 0 { 2 } else { 1 };
    item.quality = clamp_quality(item.quality + increase);
    item.sell_in -= 1;
}

fn update_backstage_pass(item: &mut Item) {
    if item.sell_in <= 0 {
        item.quality = 0;
    } else {
        let increase = match item.sell_in {
            1..=5 => 3,
            6..=10 => 2,
            _ => 1,
        };
        item.quality = clamp_quality(item.quality + increase);
    }
    item.sell_in -= 1;
}
