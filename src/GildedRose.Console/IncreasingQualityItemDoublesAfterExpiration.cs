using System;

namespace GildedRose.Console
{
    public class IncreasingQualityItemDoublesAfterExpiration : DailyCloseItem
    {
        public IncreasingQualityItemDoublesAfterExpiration(InventoryItem itemToUpdate) : base(itemToUpdate)
        {
        }

        public override void UpdateItemQuality()
        {
            if (_dailyCloseItem.Quality >= 50)
            {
                return;
            }

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