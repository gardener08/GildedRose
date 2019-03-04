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
            if (_dailyCloseItem.SellIn >= 0 && _dailyCloseItem.Quality >= 1)
            {
                _dailyCloseItem.Quality = _dailyCloseItem.Quality - 1;
                return;
            }

            if (_dailyCloseItem.Quality >= 2)
            {
                _dailyCloseItem.Quality = _dailyCloseItem.Quality - 2;
            }
            else if (_dailyCloseItem.Quality >= 1)
            {
                _dailyCloseItem.Quality = _dailyCloseItem.Quality - 1;
            }
        }
    }
}