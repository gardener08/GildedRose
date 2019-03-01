using System;

namespace GildedRose.Console
{
    public class IncreasingQualityItemDoublesAfterExpiration : IDailyCloseItem
    {
        private InventoryItem _dailyCloseItem = null;
        public IncreasingQualityItemDoublesAfterExpiration(InventoryItem itemToUpdate)
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
                if (_dailyCloseItem.SellIn < 0)
                {
                    _dailyCloseItem.Quality = _dailyCloseItem.Quality + 2;
                }
                else
                {
                    _dailyCloseItem.Quality = _dailyCloseItem.Quality + 1;
                }
            }
        }
    }
}