using System;

namespace GildedRose.Console
{
    public class ConjuredItem : DailyCloseItem
    {
        public ConjuredItem(InventoryItem itemToUpdate) : base(itemToUpdate)
        {
        }

        public override void UpdateItemQuality()
        {
            _dailyCloseItem.Quality = _dailyCloseItem.Quality - 2;
            if (_dailyCloseItem.SellIn <= 0)
            {
                _dailyCloseItem.Quality = _dailyCloseItem.Quality - _dailyCloseItem.Quality;
            }
        }
    }
}