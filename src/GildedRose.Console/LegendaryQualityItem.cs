using System;

namespace GildedRose.Console
{
    public class LegendaryQualityItem : IDailyCloseItem
    {
        private InventoryItem _dailyCloseItem = null;
        public LegendaryQualityItem(InventoryItem itemToUpdate)
        {
            _dailyCloseItem = itemToUpdate;
        }
        public void RollSellByDate()
        {
            // Nothing happens here because a legendary item never goes bad.
        }
        public void UpdateItemQuality()
        {
            // Nothing happens here because a legendary item never goes bad.
        }
    }
}