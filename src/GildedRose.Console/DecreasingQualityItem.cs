using System;

namespace GildedRose.Console
{
    public class DecreasingQualityItem : DailyCloseItem
    {
        public DecreasingQualityItem(InventoryItem itemToUpdate) : base(itemToUpdate)
        {
        }
        public override void UpdateItemQuality()
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