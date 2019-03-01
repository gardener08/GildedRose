using System;

namespace GildedRose.Console
{
    public class IncreasingQualityItem : IDailyCloseItem
    {
        private InventoryItem _dailyCloseItem = null;
        public IncreasingQualityItem(InventoryItem itemToUpdate)
        {
            _dailyCloseItem = itemToUpdate;
        }
        public void RollSellByDate()
        {
            _dailyCloseItem.SellIn = _dailyCloseItem.SellIn - 1;
        }

        public void UpdateItemQuality()
        {
            if (_dailyCloseItem.Quality < 50)
            {
                _dailyCloseItem.Quality = _dailyCloseItem.Quality + 1;
            }
        }
    }
}