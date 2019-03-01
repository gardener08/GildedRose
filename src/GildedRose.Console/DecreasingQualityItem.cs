using System;

namespace GildedRose.Console
{
    public class DecreasingQualityItem : IDailyCloseItem
    {
        private InventoryItem _dailyCloseItem = null;
        public DecreasingQualityItem(InventoryItem itemToUpdate)
        {
            _dailyCloseItem = itemToUpdate;
        }
        public void RollSellByDate()
        {
            _dailyCloseItem.SellIn = _dailyCloseItem.SellIn - 1;
        }
        public void UpdateItemQuality()
        {
            _dailyCloseItem.Quality = _dailyCloseItem.Quality - 1;
        }
    }
}