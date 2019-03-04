using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Console
{
    public abstract class DailyCloseItem : IDailyCloseItem
    {
        protected InventoryItem _dailyCloseItem = null;

        protected DailyCloseItem(InventoryItem itemToUpdate)
        {
            _dailyCloseItem = itemToUpdate;
        }
        public virtual void RollSellByDate()
        {
            _dailyCloseItem.SellIn = _dailyCloseItem.SellIn - 1;
        }
        public abstract void UpdateItemQuality();
    }
}
