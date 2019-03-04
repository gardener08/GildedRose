using System;

namespace GildedRose.Console
{
    public class LegendaryQualityItem : DailyCloseItem
    {
        public LegendaryQualityItem(InventoryItem itemToUpdate) : base(itemToUpdate)
        {
        }
        public override void RollSellByDate()
        {
            // Nothing happens here because a legendary item never goes bad.
        }
        public override void UpdateItemQuality()
        {
            // Nothing happens here because a legendary item never goes bad.
        }
    }
}