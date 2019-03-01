using System.Collections.Generic;
using GildedRose.Console;
using Xunit;

namespace GildedRose.Tests
{
    public class InventoryManagementTests
    {
        private InventoryManagement _inventoryManagement = null;
        public InventoryManagementTests()
        {
            _inventoryManagement = new InventoryManagement();
        }
        private void RunUpdateQuality(int timesToRun, InventoryManagement _inventoryManagement)
        {
            for (int i = 0; i < timesToRun; i++)
            {
                _inventoryManagement.UpdateQuality();
            }
        }

        [Fact]
        public void DoesSetupOfItemsWorkCorrectly()
        {
            IList<InventoryItem> programItems = _inventoryManagement.getInventoryItems();
            Assert.NotNull(programItems);
        }

        [Fact]
        public void ProgrammerDidNotChangeItemCollection()
        {
            IList<InventoryItem> programItems = _inventoryManagement.getInventoryItems();
            InventoryItem dexterityVest = programItems[0];
            InventoryItem agedBrie = programItems[1];
            InventoryItem standardItemMongooseElixir = programItems[2];
            InventoryItem legendaryItemSulfuras = programItems[3];
            InventoryItem backstagePasses = programItems[4];
            InventoryItem conjuredItem = programItems[5];

            Assert.Equal(dexterityVest.SellIn, 10);
            Assert.Equal(dexterityVest.Quality, 20);

            Assert.Equal(agedBrie.SellIn, 2);
            Assert.Equal(agedBrie.Quality, 0);

            Assert.Equal(standardItemMongooseElixir.SellIn, 5);
            Assert.Equal(standardItemMongooseElixir.Quality, 7);

            Assert.Equal(legendaryItemSulfuras.SellIn, 0);
            Assert.Equal(legendaryItemSulfuras.Quality, 80);
            Assert.Contains("Sulfuras", legendaryItemSulfuras.Name);

            Assert.Equal(backstagePasses.SellIn, 15);
            Assert.Equal(backstagePasses.Quality, 20);

            Assert.Equal(conjuredItem.SellIn, 3);
            Assert.Equal(conjuredItem.Quality, 6);
        }

        private void AssertInventoryItemStateAfterUpdate(InventoryItemAssertData inventoryItemComparisonData)
        {
            Assert.Equal(inventoryItemComparisonData.ExpectedSellIn, inventoryItemComparisonData.ActualSellIn);
            Assert.Equal(inventoryItemComparisonData.ExpectedQuality, inventoryItemComparisonData.ActualQuality);
        }

        [Fact]
        public void LegendaryItemNoChangeAfterUpdatingQuality()
        {
            IList<InventoryItem> programItems = _inventoryManagement.getInventoryItems();
            InventoryItem legendaryItemSulfuras = programItems[3];

            int currentSellIn = legendaryItemSulfuras.SellIn;
            int currentQuality = legendaryItemSulfuras.Quality;

            _inventoryManagement.UpdateQuality();

            int newSellIn = legendaryItemSulfuras.SellIn;
            int newQuality = legendaryItemSulfuras.Quality;

            InventoryItemAssertData comparisonData = new InventoryItemAssertData()
            {
                ExpectedSellIn = currentSellIn,
                ActualSellIn = newSellIn,
                ExpectedQuality = currentQuality,
                ActualQuality = newQuality
            };

            AssertInventoryItemStateAfterUpdate(comparisonData);
        }

        [Fact]
        public void DecrementQualityOfStandardItem()
        {
            IList<InventoryItem> programItems = _inventoryManagement.getInventoryItems();
            InventoryItem standardItemMongooseElixir = programItems[2];

            _inventoryManagement.UpdateQuality();

            InventoryItemAssertData comparisonData = new InventoryItemAssertData()
            {
                ExpectedSellIn = 4,
                ActualSellIn = standardItemMongooseElixir.SellIn,
                ExpectedQuality = 6,
                ActualQuality = standardItemMongooseElixir.Quality
            };

            AssertInventoryItemStateAfterUpdate(comparisonData);
        }

        [Fact]
        public void NoNegativeQualityValuesWhereQualityDecreasesOnDexterityVest()
        {
            IList<InventoryItem> programItems = _inventoryManagement.getInventoryItems();
            InventoryItem dexterityVest = programItems[0];

            int timesToRun = 16;
            RunUpdateQuality(timesToRun, _inventoryManagement);

            InventoryItemAssertData comparisonData = new InventoryItemAssertData()
            {
                ExpectedSellIn = -6,
                ActualSellIn = dexterityVest.SellIn,
                ExpectedQuality = 0,
                ActualQuality = dexterityVest.Quality
            };

            AssertInventoryItemStateAfterUpdate(comparisonData);
        }

        [Fact]
        public void NoNegativeQualityValuesWhereQualityDecreasesOnMongooseElixir()
        {
            IList<InventoryItem> programItems = _inventoryManagement.getInventoryItems();
            InventoryItem mongooseElixir = programItems[2];

            int timesToRun = 8;
            RunUpdateQuality(timesToRun, _inventoryManagement);

            InventoryItemAssertData comparisonData = new InventoryItemAssertData()
            {
                ExpectedSellIn = -3,
                ActualSellIn = mongooseElixir.SellIn,
                ExpectedQuality = 0,
                ActualQuality = mongooseElixir.Quality
            };

            AssertInventoryItemStateAfterUpdate(comparisonData);
        }

        [Fact]
        public void DoubleSpeedQualityDegradationAfterSellByDate()
        {
            IList<InventoryItem> programItems = _inventoryManagement.getInventoryItems();
            InventoryItem dexterityVest = programItems[0];
            
            // Sell By date reached on tenth run.
            int timesToRun = 13;
            RunUpdateQuality(timesToRun, _inventoryManagement);

            InventoryItemAssertData comparisonData = new InventoryItemAssertData()
            {
                ExpectedSellIn = -3,
                ActualSellIn = dexterityVest.SellIn,
                ExpectedQuality = 4,
                ActualQuality = dexterityVest.Quality
            };

            AssertInventoryItemStateAfterUpdate(comparisonData);
        }

        [Fact]
        public void IncreasingQualityOfAgedBrie()
        {
            IList<InventoryItem> programItems = _inventoryManagement.getInventoryItems();
            InventoryItem agedBrie = programItems[1];

            int timesToRun = 5;
            RunUpdateQuality(timesToRun, _inventoryManagement);

            InventoryItemAssertData comparisonData = new InventoryItemAssertData()
            {
                ExpectedSellIn = -3,
                ActualSellIn = agedBrie.SellIn,
                ExpectedQuality = 8,
                ActualQuality = agedBrie.Quality
            };

            AssertInventoryItemStateAfterUpdate(comparisonData);
        }

        /// <summary>
        /// Given the program data that is not designed to change, Aged Brie and Backstage Passes are the only inventory items that are able to reach the max value.
        /// </summary>
        [Fact]
        public void MaxQualityOfAgedBrie()
        {
            IList<InventoryItem> programItems = _inventoryManagement.getInventoryItems();
            InventoryItem agedBrie = programItems[1];

            int timesToRun = 65;
            RunUpdateQuality(timesToRun, _inventoryManagement);

            int expectedQuality = 50;

            Assert.Equal(expectedQuality, agedBrie.Quality);
        }

        [Fact]
        public void BackstagePassesOneDayBeforeConcert()
        {
            IList<InventoryItem> programItems = _inventoryManagement.getInventoryItems();
            InventoryItem backstagePasses = programItems[4];

            int timesToRun = 14;
            RunUpdateQuality(timesToRun, _inventoryManagement);

            InventoryItemAssertData comparisonData = new InventoryItemAssertData()
            {
                ExpectedSellIn = 1,
                ActualSellIn = backstagePasses.SellIn,
                ExpectedQuality = 49,
                ActualQuality = backstagePasses.Quality
            };

            AssertInventoryItemStateAfterUpdate(comparisonData);
        }

        [Fact]
        public void BackstagePassesOnConcertDate()
        {
            IList<InventoryItem> programItems = _inventoryManagement.getInventoryItems();
            InventoryItem backstagePasses = programItems[4];

            int timesToRun = 15;
            RunUpdateQuality(timesToRun, _inventoryManagement);

            InventoryItemAssertData comparisonData = new InventoryItemAssertData()
            {
                ExpectedSellIn = 0,
                ActualSellIn = backstagePasses.SellIn,
                ExpectedQuality = 50,
                ActualQuality = backstagePasses.Quality
            };

            AssertInventoryItemStateAfterUpdate(comparisonData);
        }

        [Fact]
        public void BackstagePassesAfterConcertDate()
        {
            IList<InventoryItem> programItems = _inventoryManagement.getInventoryItems();
            InventoryItem backstagePasses = programItems[4];

            int timesToRun = 16;
            RunUpdateQuality(timesToRun, _inventoryManagement);

            InventoryItemAssertData comparisonData = new InventoryItemAssertData()
            {
                ExpectedSellIn = -1,
                ActualSellIn = backstagePasses.SellIn,
                ExpectedQuality = 0,
                ActualQuality = backstagePasses.Quality
            };

            AssertInventoryItemStateAfterUpdate(comparisonData);
        }

        [Fact]
        public void DecrementQualityOfConjuredItems()
        {
            IList<InventoryItem> programItems = _inventoryManagement.getInventoryItems();
            InventoryItem conjuredItem = programItems[5];

            int timesToRun = 3;
            RunUpdateQuality(timesToRun, _inventoryManagement);

            InventoryItemAssertData comparisonData = new InventoryItemAssertData()
            {
                ExpectedSellIn = 0,
                ActualSellIn = conjuredItem.SellIn,
                ExpectedQuality = 0,
                ActualQuality = conjuredItem.Quality
            };

            AssertInventoryItemStateAfterUpdate(comparisonData);
        }

        [Fact]
        public void DecrementQualityOfConjuredItemsPastSellByDate()
        {
            IList<InventoryItem> programItems = _inventoryManagement.getInventoryItems();
            InventoryItem conjuredItem = programItems[5];

            int timesToRun = 4;
            RunUpdateQuality(timesToRun, _inventoryManagement);

            InventoryItemAssertData comparisonData = new InventoryItemAssertData()
            {
                ExpectedSellIn = -1,
                ActualSellIn = conjuredItem.SellIn,
                ExpectedQuality = 0,
                ActualQuality = conjuredItem.Quality
            };

            AssertInventoryItemStateAfterUpdate(comparisonData);
        }
    }

    internal class InventoryItemAssertData
    {
        public int ExpectedSellIn { get; set; }
        public int ExpectedQuality { get; set; }
        public int ActualSellIn { get; set; }
        public int ActualQuality { get; set; }
    }
}
 