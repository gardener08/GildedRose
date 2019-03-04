using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Console
{
    public class InventoryData
    {
        private readonly IList<InventoryItem> _inventoryItems = new List<InventoryItem>
        {
            new InventoryItem {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
            new InventoryItem {Name = "Aged Brie", SellIn = 2, Quality = 0},
            new InventoryItem {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
            new InventoryItem {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
            new InventoryItem
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 15,
                Quality = 20
            },
            new InventoryItem {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
        };

        public IList<InventoryItem> GetInventoryItems()
        {
            return _inventoryItems;
        }
    }
}
