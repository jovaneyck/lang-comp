use gilded_rose::{GildedRose, Item};

fn update_once(name: &str, sell_in: i32, quality: i32) -> (i32, i32) {
    let items = vec![Item::new(name, sell_in, quality)];
    let mut rose = GildedRose::new(items);
    rose.update_quality();
    (rose.items[0].sell_in, rose.items[0].quality)
}

#[test]
fn conjured_before_sell_date() {
    assert_eq!(update_once("Conjured Mana Cake", 10, 20), (9, 18));
}

#[test]
fn conjured_on_sell_date() {
    assert_eq!(update_once("Conjured Mana Cake", 0, 20), (-1, 16));
}

#[test]
fn conjured_after_sell_date() {
    assert_eq!(update_once("Conjured Mana Cake", -5, 10), (-6, 6));
}

#[test]
fn conjured_quality_never_negative() {
    assert_eq!(update_once("Conjured Mana Cake", 5, 1), (4, 0));
    assert_eq!(update_once("Conjured Mana Cake", 0, 1), (-1, 0));
    assert_eq!(update_once("Conjured Mana Cake", 0, 3), (-1, 0));
}

#[test]
fn thirty_days() {
    let items = vec![
        Item::new("+5 Dexterity Vest", 10, 20),
        Item::new("Aged Brie", 2, 0),
        Item::new("Elixir of the Mongoose", 5, 7),
        Item::new("Sulfuras, Hand of Ragnaros", 0, 80),
        Item::new("Sulfuras, Hand of Ragnaros", -1, 80),
        Item::new("Backstage passes to a TAFKAL80ETC concert", 15, 20),
        Item::new("Backstage passes to a TAFKAL80ETC concert", 10, 49),
        Item::new("Backstage passes to a TAFKAL80ETC concert", 5, 49),
        Item::new("Conjured Mana Cake", 3, 6),
    ];

    let mut rose = GildedRose::new(items);
    let mut output = String::new();

    for day in 0..=30 {
        output.push_str(&format!("-------- day {} --------\n", day));
        output.push_str("name, sellIn, quality\n");
        for item in &rose.items {
            output.push_str(&format!("{}, {}, {}\n", item.name, item.sell_in, item.quality));
        }
        output.push('\n');
        rose.update_quality();
    }

    insta::assert_snapshot!(output);
}
