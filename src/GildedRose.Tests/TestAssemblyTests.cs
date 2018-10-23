using System.Collections.Generic;
using System.Reflection.Emit;
using Xunit;
using GildedRose.Console;

namespace GildedRose.Tests
{
    public class TestAssemblyTests : Program
    {
        private void RunUpdateQuality(int timesToRun, Program programInstance)
        {
            for (int i = 0; i < timesToRun; i++)
            {
                programInstance.UpdateQuality();
            }
        }

        [Fact]
        public void TestSetupItems()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            Assert.NotNull(programInstance.Items);
        }

        [Fact]
        public void ProgrammerDidNotChangeItemCollection()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item dexterityVest = programItems[0];
            Item agedBrie = programItems[1];
            Item standardItemMongooseElixir = programItems[2];
            Item legendaryItemSulfuras = programItems[3];
            Item backstagePasses = programItems[4];
            Item conjuredItem = programItems[5];

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
        public void TestLegendaryItem()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item legendaryItemSulfuras = programItems[3];

            int currentSellIn = legendaryItemSulfuras.SellIn;
            int currentQuality = legendaryItemSulfuras.Quality;

            programInstance.UpdateQuality();

            int newSellIn = legendaryItemSulfuras.SellIn;
            int newQuality = legendaryItemSulfuras.Quality;

            Assert.Equal(currentSellIn, newSellIn);
            Assert.Equal(currentQuality, newQuality);
        }

        [Fact]
        public void TestForDecrementOfQualityOfStandardItem()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item standardItemMongooseElixir = programItems[2];

            programInstance.UpdateQuality();

            Assert.Equal(standardItemMongooseElixir.SellIn, 4);
            Assert.Equal(standardItemMongooseElixir.Quality, 6);
        }

        [Fact]
        public void TestForNoNegativeQualityValuesWhereQualityDecreasesDexterityVest()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item dexterityVest = programItems[0];

            int timesToRun = 16;
            RunUpdateQuality(timesToRun, programInstance);

            Assert.Equal(dexterityVest.SellIn, -6);
            // Reflects bugfix to original Kata - Zero and not -2
            Assert.Equal(dexterityVest.Quality, 0);

        }

        [Fact]
        public void TestForNoNegativeQualityValuesWhereQualityDecreasesMongooseElixir()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item mongooseElixir = programItems[2];

            int timesToRun = 8;
            RunUpdateQuality(timesToRun, programInstance);

            Assert.Equal(mongooseElixir.SellIn, -3);
            // Reflects bugfix to original Kata - Zero and not -1
            Assert.Equal(mongooseElixir.Quality, 0);
        }

        [Fact]
        public void TestForDoubleSpeedQualityDegradationAfterSellByDate()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item dexterityVest = programItems[0];
            
            // Sell By date reached on tenth run.
            int timesToRun = 13;
            RunUpdateQuality(timesToRun, programInstance);

            Assert.Equal(dexterityVest.SellIn, -3);
            Assert.Equal(dexterityVest.Quality, 4);
        }

        [Fact]
        public void TestIncreasingQualityOfAgedBrie()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item agedBrie = programItems[1];

            int timesToRun = 5;
            RunUpdateQuality(timesToRun, programInstance);

            Assert.Equal(agedBrie.SellIn, -3);
            // The hypothetical product owner has updated the requirements to reflect that Aged Brie increases in quality by 2
            // after the sell by date has passed.
            Assert.Equal(agedBrie.Quality, 8);

        }

        /// <summary>
        /// Given the program data that is not designed to change, Aged Brie and Backstage Passes are the only inventory items that are able to reach the max value.
        /// </summary>
        [Fact]
        public void TestMaxQualityOfAgedBrie()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item agedBrie = programItems[1];

            int timesToRun = 65;
            RunUpdateQuality(timesToRun, programInstance);

            Assert.Equal(agedBrie.Quality, 50);
        }

        [Fact]
        public void TestBackstagePassesOneDayBeforeConcert()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item backstagePasses = programItems[4];

            int timesToRun = 14;
            RunUpdateQuality(timesToRun, programInstance);

            Assert.Equal(backstagePasses.SellIn, 1);
            Assert.Equal(backstagePasses.Quality, 49);
        }

        [Fact]
        public void TestBackstagePassesOnConcertDate()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item backstagePasses = programItems[4];

            int timesToRun = 15;
            RunUpdateQuality(timesToRun, programInstance);

            Assert.Equal(backstagePasses.SellIn, 0);
            Assert.Equal(backstagePasses.Quality, 50);
        }

        [Fact]
        public void TestBackstagePassesAfterConcertDate()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item backstagePasses = programItems[4];

            int timesToRun = 16;
            RunUpdateQuality(timesToRun, programInstance);

            Assert.Equal(backstagePasses.SellIn, -1);
            Assert.Equal(backstagePasses.Quality, 0);
        }

        [Fact]
        public void TestForDecrementOfQualityOfConjuredItems()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item conjuredItem = programItems[5];

            int timesToRun = 3;
            RunUpdateQuality(timesToRun, programInstance);

            Assert.Equal(conjuredItem.SellIn, 0);
            Assert.Equal(conjuredItem.Quality, 0);
        }

        [Fact]
        public void TestForDecrementOfQualityOfConjuredItemsPastSellByDate()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item conjuredItem = programItems[5];

            int timesToRun = 4;
            RunUpdateQuality(timesToRun, programInstance);

            Assert.Equal(conjuredItem.SellIn, -1);
            Assert.Equal(conjuredItem.Quality, 0);
        }
    }
}
 