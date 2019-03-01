using System.Collections.Generic;
using GildedRose.Console;
using Xunit;

namespace GildedRose.Tests
{
    public class InventoryManagementTests
    {
        private void RunUpdateQuality(int timesToRun, InventoryManagement inventoryManagementInstance)
        {
            for (int i = 0; i < timesToRun; i++)
            {
                inventoryManagementInstance.UpdateQuality();
            }
        }

        [Fact]
        public void DoesSetupOfItemsWorkCorrectly()
        {
            InventoryManagement inventoryManagementInstance = new InventoryManagement();
            IList<InventoryItem> programItems = inventoryManagementInstance.getInventoryItems();
            Assert.NotNull(programItems);
        }

        [Fact]
        public void ProgrammerDidNotChangeItemCollection()
        {
            InventoryManagement inventoryManagementInstance = new InventoryManagement();
            IList<InventoryItem> programItems = inventoryManagementInstance.getInventoryItems();
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

        [Fact]
        public void LegendaryItemNoChangeAfterUpdatingQuality()
        {
            InventoryManagement inventoryManagementInstance = new InventoryManagement();
            IList<InventoryItem> programItems = inventoryManagementInstance.getInventoryItems();
            InventoryItem legendaryItemSulfuras = programItems[3];

            int currentSellIn = legendaryItemSulfuras.SellIn;
            int currentQuality = legendaryItemSulfuras.Quality;

            inventoryManagementInstance.UpdateQuality();

            int newSellIn = legendaryItemSulfuras.SellIn;
            int newQuality = legendaryItemSulfuras.Quality;

            Assert.Equal(currentSellIn, newSellIn);
            Assert.Equal(currentQuality, newQuality);
        }

        [Fact]
        public void DecrementQualityOfStandardItem()
        {
            InventoryManagement inventoryManagementInstance = new InventoryManagement();
            IList<InventoryItem> programItems = inventoryManagementInstance.getInventoryItems();
            InventoryItem standardItemMongooseElixir = programItems[2];

            inventoryManagementInstance.UpdateQuality();

            int expectedSellInAfterOneRunOfUpdateQuality = 4;
            int expectedQualityAfterOneRunOfUpdateQuality = 6;

            Assert.Equal(expectedSellInAfterOneRunOfUpdateQuality, standardItemMongooseElixir.SellIn);
            Assert.Equal(expectedQualityAfterOneRunOfUpdateQuality, standardItemMongooseElixir.Quality);
        }

        [Fact]
        public void NoNegativeQualityValuesWhereQualityDecreasesOnDexterityVest()
        {
            InventoryManagement inventoryManagementInstance = new InventoryManagement();
            IList<InventoryItem> programItems = inventoryManagementInstance.getInventoryItems();
            InventoryItem dexterityVest = programItems[0];

            int timesToRun = 16;
            RunUpdateQuality(timesToRun, inventoryManagementInstance);

            int expectedSellIn = -6;
            // Reflects bugfix to original Kata - Zero and not -2
            int expectedQuality = 0;

            Assert.Equal(expectedSellIn, dexterityVest.SellIn);
            Assert.Equal(expectedQuality, dexterityVest.Quality);

        }

        [Fact]
        public void NoNegativeQualityValuesWhereQualityDecreasesOnMongooseElixir()
        {
            InventoryManagement inventoryManagementInstance = new InventoryManagement();
            IList<InventoryItem> programItems = inventoryManagementInstance.getInventoryItems();
            InventoryItem mongooseElixir = programItems[2];

            int timesToRun = 8;
            RunUpdateQuality(timesToRun, inventoryManagementInstance);

            int expectedSellIn = -3;
            int expectedQuality = 0;

            Assert.Equal(expectedSellIn, mongooseElixir.SellIn);
            // Reflects bugfix to original Kata - Zero and not -1
            Assert.Equal(expectedQuality, mongooseElixir.Quality);
        }

        [Fact]
        public void DoubleSpeedQualityDegradationAfterSellByDate()
        {
            InventoryManagement inventoryManagementInstance = new InventoryManagement();
            IList<InventoryItem> programItems = inventoryManagementInstance.getInventoryItems();
            InventoryItem dexterityVest = programItems[0];
            
            // Sell By date reached on tenth run.
            int timesToRun = 13;
            RunUpdateQuality(timesToRun, inventoryManagementInstance);

            int expectedSellIn = -3;
            int expectedQuality = 4;

            Assert.Equal(expectedSellIn, dexterityVest.SellIn);
            Assert.Equal(expectedQuality, dexterityVest.Quality);
        }

        [Fact]
        public void IncreasingQualityOfAgedBrie()
        {
            InventoryManagement inventoryManagementInstance = new InventoryManagement();
            IList<InventoryItem> programItems = inventoryManagementInstance.getInventoryItems();
            InventoryItem agedBrie = programItems[1];

            int timesToRun = 5;
            RunUpdateQuality(timesToRun, inventoryManagementInstance);

            int expectedSellIn = -3;
            // The hypothetical product owner has updated the requirements to reflect that Aged Brie increases in quality by 2
            // after the sell by date has passed.
            int expectedQuality = 8;

            Assert.Equal(expectedSellIn, agedBrie.SellIn);
            Assert.Equal(expectedQuality, agedBrie.Quality);

        }

        /// <summary>
        /// Given the program data that is not designed to change, Aged Brie and Backstage Passes are the only inventory items that are able to reach the max value.
        /// </summary>
        [Fact]
        public void MaxQualityOfAgedBrie()
        {
            InventoryManagement inventoryManagementInstance = new InventoryManagement();
            IList<InventoryItem> programItems = inventoryManagementInstance.getInventoryItems();
            InventoryItem agedBrie = programItems[1];

            int timesToRun = 65;
            RunUpdateQuality(timesToRun, inventoryManagementInstance);

            int expectedQuality = 50;

            Assert.Equal(expectedQuality, agedBrie.Quality);
        }

        [Fact]
        public void BackstagePassesOneDayBeforeConcert()
        {
            InventoryManagement inventoryManagementInstance = new InventoryManagement();
            IList<InventoryItem> programItems = inventoryManagementInstance.getInventoryItems();
            InventoryItem backstagePasses = programItems[4];

            int timesToRun = 14;
            RunUpdateQuality(timesToRun, inventoryManagementInstance);

            int expectedSellIn = 1;
            int expectedQuality = 49;

            Assert.Equal(expectedSellIn, backstagePasses.SellIn);
            Assert.Equal(expectedQuality, backstagePasses.Quality);
        }

        [Fact]
        public void BackstagePassesOnConcertDate()
        {
            InventoryManagement inventoryManagementInstance = new InventoryManagement();
            IList<InventoryItem> programItems = inventoryManagementInstance.getInventoryItems();
            InventoryItem backstagePasses = programItems[4];

            int timesToRun = 15;
            RunUpdateQuality(timesToRun, inventoryManagementInstance);

            int expectedSellIn = 0;
            int expectedQuality = 50;

            Assert.Equal(expectedSellIn, backstagePasses.SellIn);
            Assert.Equal(expectedQuality, backstagePasses.Quality);
        }

        [Fact]
        public void BackstagePassesAfterConcertDate()
        {
            InventoryManagement inventoryManagementInstance = new InventoryManagement();
            IList<InventoryItem> programItems = inventoryManagementInstance.getInventoryItems();
            InventoryItem backstagePasses = programItems[4];

            int timesToRun = 16;
            RunUpdateQuality(timesToRun, inventoryManagementInstance);

            int expectedSellIn = -1;
            int expectedQuality = 0;

            Assert.Equal(expectedSellIn, backstagePasses.SellIn);
            Assert.Equal(expectedQuality, backstagePasses.Quality);
        }

        [Fact]
        public void DecrementQualityOfConjuredItems()
        {
            InventoryManagement inventoryManagementInstance = new InventoryManagement();
            IList<InventoryItem> programItems = inventoryManagementInstance.getInventoryItems();
            InventoryItem conjuredItem = programItems[5];

            int timesToRun = 3;
            RunUpdateQuality(timesToRun, inventoryManagementInstance);

            int expectedSellIn = 0;
            int expectedQuality = 0;

            Assert.Equal(expectedSellIn, conjuredItem.SellIn);
            Assert.Equal(expectedQuality, conjuredItem.Quality);
        }

        [Fact]
        public void DecrementQualityOfConjuredItemsPastSellByDate()
        {
            InventoryManagement inventoryManagementInstance = new InventoryManagement();
            IList<InventoryItem> programItems = inventoryManagementInstance.getInventoryItems();
            InventoryItem conjuredItem = programItems[5];

            int timesToRun = 4;
            RunUpdateQuality(timesToRun, inventoryManagementInstance);

            int expectedSellIn = -1;
            int expectedQuality = 0;

            Assert.Equal(expectedSellIn, conjuredItem.SellIn);
            Assert.Equal(expectedQuality, conjuredItem.Quality);
        }
    }
}
 