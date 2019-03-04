using System;

namespace GildedRose.Console
{
    public class IncreasingQualityWithExpirationItem : DailyCloseItem
    {
        public IncreasingQualityWithExpirationItem(InventoryItem itemToUpdate) : base(itemToUpdate)
        {
        }
        public override void UpdateItemQuality()
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