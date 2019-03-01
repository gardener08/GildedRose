using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Console
{
    public class InventoryManagement
    {
        public InventoryManagement()
        {
            InventoryData inventoryDalInventory = new InventoryData();
            _inventoryItems = inventoryDalInventory.GetInventoryItems();
        }

        private readonly IList<InventoryItem> _inventoryItems = null;

        public IList<InventoryItem> getInventoryItems()
        {
            return _inventoryItems;
        }

        public void UpdateQuality()
        {
            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                InventoryItem currentItem = _inventoryItems[i];
                IDailyCloseItem dailyCloseItem =
                    DailyCloseItemFactory.CreateAppropriateDailyCloseItem(currentItem);

                dailyCloseItem.RollSellByDate();
                dailyCloseItem.UpdateItemQuality();
            }
        }
    }
}
