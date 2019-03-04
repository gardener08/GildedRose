using System;

namespace GildedRose.Console
{
    public class IncreasingQualityWithExpirationItem : IDailyCloseItem
    {
        private InventoryItem _dailyCloseItem = null;
        public IncreasingQualityWithExpirationItem(InventoryItem itemToUpdate)
        {
            _dailyCloseItem = itemToUpdate;
        }
        public void RollSellByDate()
        {
            _dailyCloseItem.SellIn = _dailyCloseItem.SellIn - 1;
        }
        public void UpdateItemQuality()
        {
            if (_dailyCloseItem.SellIn < 0)
            {
                _dailyCloseItem.Quality = _dailyCloseItem.Quality - _dailyCloseItem.Quality;
                return;
            }

            if (_dailyCloseItem.Quality >= 50)
            {
                return;
            }

            if (_dailyCloseItem.SellIn < 6)
            {
                _dailyCloseItem.Quality = _dailyCloseItem.Quality + 3;
            }
            else if (_dailyCloseItem.SellIn < 11)
            {
                _dailyCloseItem.Quality = _dailyCloseItem.Quality + 2;
            }
            else
            {
                _dailyCloseItem.Quality = _dailyCloseItem.Quality + 1;
            }

            if (_dailyCloseItem.Quality > 50)
            {
                _dailyCloseItem.Quality = 50;
            }
        }
    }
}