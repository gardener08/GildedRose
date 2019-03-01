using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Console
{
    public static class DailyCloseItemFactory
    {
        public static IDailyCloseItem CreateAppropriateDailyCloseItem(InventoryItem inventoryItem)
        {
            if ("Aged Brie".Equals(inventoryItem.Name))
            {
                return new IncreasingQualityItem(inventoryItem);
            }
            else if ("Sulfuras, Hand of Ragnaros".Equals(inventoryItem.Name))
            {
                return new LegendaryQualityItem(inventoryItem);
            }
            else if ("Backstage passes to a TAFKAL80ETC concert".Equals(inventoryItem.Name))
            {
                return new IncreasingQualityWithExpirationItem(inventoryItem);
            }
            else if (inventoryItem.Name.ToUpper().StartsWith("CONJURED"))
            {
                return new ConjuredItem(inventoryItem);
            }
            else return new DecreasingQualityItem(inventoryItem);
        }
    }
}
