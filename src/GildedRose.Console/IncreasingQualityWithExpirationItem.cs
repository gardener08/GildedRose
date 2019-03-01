﻿using System;

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
            if (_dailyCloseItem.SellIn < 11)
            {
                if (_dailyCloseItem.Quality < 50)
                {
                    _dailyCloseItem.Quality = _dailyCloseItem.Quality + 1;
                }
            }

            if (_dailyCloseItem.SellIn < 6)
            {
                if (_dailyCloseItem.Quality < 50)
                {
                    _dailyCloseItem.Quality = _dailyCloseItem.Quality + 1;
                }
            }
        }
    }
}