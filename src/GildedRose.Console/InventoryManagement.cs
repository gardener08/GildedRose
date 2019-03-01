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

        private readonly string[] _increasingValueNames = { "Aged Brie", "Backstage passes to a TAFKAL80ETC concert" };
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

                if (currentItem.Name != "Aged Brie" && currentItem.Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    DecrementDecreasingQualityItem(currentItem);
                }
                else
                {
                    IncrementIncreasingQualityItems(currentItem);
                }
                if (currentItem.SellIn < 0)
                {
                    ProcessItemPastSellDate(currentItem);
                }
            }
        }

        private void DecrementDecreasingQualityItem(InventoryItem currentItem)
        {
            if (currentItem.Quality > 0)
            {
                if (currentItem.Name.ToUpper().StartsWith("CONJURED"))
                {
                    currentItem.Quality = currentItem.Quality - 2;
                }
                else if (currentItem.Name != "Sulfuras, Hand of Ragnaros")
                {
                    currentItem.Quality = currentItem.Quality - 1;
                }
            }
        }

        private void IncrementIncreasingQualityItems(InventoryItem currentItem)
        {
            if (currentItem.Quality < 50)
            {
                currentItem.Quality = currentItem.Quality + 1;

                if (currentItem.Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (currentItem.SellIn < 11)
                    {
                        if (currentItem.Quality < 50)
                        {
                            currentItem.Quality = currentItem.Quality + 1;
                        }
                    }

                    if (currentItem.SellIn < 6)
                    {
                        if (currentItem.Quality < 50)
                        {
                            currentItem.Quality = currentItem.Quality + 1;
                        }
                    }
                }
            }
        }

        private void ProcessItemPastSellDate(InventoryItem currentItem)
        {
            if (_increasingValueNames.Contains(currentItem.Name))
            {
                ProcessIncreasingValueItemPastSellDate(currentItem);
            }
            else
            {
                ProcessDecreasingOrStableValueItemPastSellDate(currentItem);
            }
        }

        private void ProcessDecreasingOrStableValueItemPastSellDate(InventoryItem currentItem)
        {
            if (currentItem.Quality > 0)
            {
                // Conjured already decreases by two.
                if ((currentItem.Name != "Sulfuras, Hand of Ragnaros") && (!(currentItem.Name.ToUpper().StartsWith("CONJURED"))))
                {
                    currentItem.Quality = currentItem.Quality - 1;
                }
            }
            else
            {
                currentItem.Quality = currentItem.Quality - currentItem.Quality;
            }
        }

        private void ProcessIncreasingValueItemPastSellDate(InventoryItem currentItem)
        {
            if (currentItem.Name != "Backstage passes to a TAFKAL80ETC concert")
            {
                if (currentItem.Quality < 50)
                {
                    currentItem.Quality = currentItem.Quality + 1;
                }
            }
            else
            {
                currentItem.Quality = currentItem.Quality - currentItem.Quality;
            }
        }
    }
}
