using System;

namespace GildedRose.Console
{
    public class IncreasingQualityItem : DailyCloseItem
    {
        public IncreasingQualityItem(InventoryItem itemToUpdate) : base(itemToUpdate)
        {
        }

        public override void UpdateItemQuality()
        {
            if (_dailyCloseItem.Quality < 50)
            {
                _dailyCloseItem.Quality = _dailyCloseItem.Quality + 1;
            }
        }
    }
}