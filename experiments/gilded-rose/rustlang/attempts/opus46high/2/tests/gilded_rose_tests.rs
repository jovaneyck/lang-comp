use gilded_rose::{GildedRose, Item};

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
