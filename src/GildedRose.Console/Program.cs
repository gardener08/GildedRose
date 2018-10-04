using System.Collections.Generic;
using System.Linq;

namespace GildedRose.Console
{
    public class Program
    {
        protected internal IList<Item> Items = new List<Item>
        {
            new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
            new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
            new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
            new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
            new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 15,
                Quality = 20
            },
            new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
        };

        private readonly string[] increasingValueNames = {"Aged Brie", "Backstage passes to a TAFKAL80ETC concert"};

        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program();

            app.UpdateQuality();

            System.Console.ReadKey();

        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                // Base level decrement of sell by date.
                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }

                Item currentItem = Items[i];
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

        private void DecrementDecreasingQualityItem(Item currentItem)
        {
            if (currentItem.Quality > 0)
            {
                if (currentItem.Name != "Sulfuras, Hand of Ragnaros")
                {
                    currentItem.Quality = currentItem.Quality - 1;
                }
            }
        }

        private void IncrementIncreasingQualityItems(Item currentItem)
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

        private void ProcessItemPastSellDate(Item currentItem)
        {
            if (increasingValueNames.Contains(currentItem.Name))
            {
                ProcessIncreasingValueItemPastSellDate(currentItem);
            }
            else
            {
                ProcessDecreasingOrStableValueItemPastSellDate(currentItem);
            }
        }

        private void ProcessDecreasingOrStableValueItemPastSellDate(Item currentItem)
        { 
            if (currentItem.Quality > 0)
            {
                if (currentItem.Name != "Sulfuras, Hand of Ragnaros")
                {
                    currentItem.Quality = currentItem.Quality - 1;
                }
            }
            else
            {
                currentItem.Quality = currentItem.Quality - currentItem.Quality;
            }
        }

        private void ProcessIncreasingValueItemPastSellDate(Item currentItem)
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

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
