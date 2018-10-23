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
        public void TestLegendaryItem()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item legendaryItem = programItems[3];
            Assert.Contains("Sulfuras", legendaryItem.Name);
            int currentSellIn = legendaryItem.SellIn;
            int currentQuality = legendaryItem.Quality;

            programInstance.UpdateQuality();

            int newSellIn = legendaryItem.SellIn;
            int newQuality = legendaryItem.Quality;

            Assert.Equal(currentSellIn, newSellIn);
            Assert.Equal(currentQuality, newQuality);
        }

        [Fact]
        public void TestForDecrementOfQualityOfStandardItem()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item standardItem = programItems[2];
            Assert.Equal(standardItem.SellIn, 5);
            Assert.Equal(standardItem.Quality, 7);

            programInstance.UpdateQuality();

            Assert.Equal(standardItem.SellIn, 4);
            Assert.Equal(standardItem.Quality, 6);
        }

        [Fact]
        public void TestForNoNegativeQualityValuesWhereQualityDecreases()
        {
            // Scenario 1
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item itemUnderTest = programItems[0];
            Assert.Equal(itemUnderTest.SellIn, 10);
            Assert.Equal(itemUnderTest.Quality, 20);

            int timesToRun = 16;
            RunUpdateQuality(timesToRun, programInstance);

            Assert.Equal(itemUnderTest.SellIn, -6);
            // Zero and not -2
            Assert.Equal(itemUnderTest.Quality, 0);

            // Scenario 2
            TestAssemblyTests programInstance2 = new TestAssemblyTests();
            var programItems2 = programInstance2.Items;

            Item secondItemUnderTest = programItems2[2];
            Assert.Equal(secondItemUnderTest.SellIn, 5);
            Assert.Equal(secondItemUnderTest.Quality, 7);

            int timesToRun2 = 8;
            RunUpdateQuality(timesToRun2, programInstance2);

            Assert.Equal(secondItemUnderTest.SellIn, -3); // Code instead of comments
            // Zero and not -1
            Assert.Equal(secondItemUnderTest.Quality, 0);
        }

        [Fact]
        public void TestForDoubleSpeedQualityDegradationAfterSellByDate()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item itemUnderTest = programItems[0];
            Assert.Equal(itemUnderTest.SellIn, 10);
            Assert.Equal(itemUnderTest.Quality, 20);
            
            // Sell By date reached on tenth run.
            int timesToRun = 13;
            RunUpdateQuality(timesToRun, programInstance);

            Assert.Equal(itemUnderTest.SellIn, -3);
            Assert.Equal(itemUnderTest.Quality, 4);
        }

        [Fact]
        public void TestIncreasingQualityOfAgedBrie()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item itemUnderTest = programItems[1];
            Assert.Equal(itemUnderTest.SellIn, 2);
            Assert.Equal(itemUnderTest.Quality, 0);

            int timesToRun = 5;
            RunUpdateQuality(timesToRun, programInstance);

            Assert.Equal(itemUnderTest.SellIn, -3);
            // The hypothetical product owner has updated the requirements to reflect that Aged Brie increases in quality by 2
            // after the sell by date has passed.
            Assert.Equal(itemUnderTest.Quality, 8);

        }

        /// <summary>
        /// Given the program data that is not designed to change, Aged Brie and Backstage Passes are the only inventory items that are able to reach the max value.
        /// </summary>
        [Fact]
        public void TestMaxQualityOfAgedBrie()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item itemUnderTest = programItems[1];
            Assert.Equal(itemUnderTest.SellIn, 2);
            Assert.Equal(itemUnderTest.Quality, 0);

            int timesToRun = 65;
            RunUpdateQuality(timesToRun, programInstance);

            Assert.Equal(itemUnderTest.Quality, 50);
        }

        [Fact]
        public void TestBackstagePassesOneDayBeforeConcert()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item itemUnderTest = programItems[4];
            Assert.Equal(itemUnderTest.SellIn, 15);
            Assert.Equal(itemUnderTest.Quality, 20);

            int timesToRun = 14;
            RunUpdateQuality(timesToRun, programInstance);

            Assert.Equal(itemUnderTest.SellIn, 1);
            Assert.Equal(itemUnderTest.Quality, 49);
        }

        [Fact]
        public void TestBackstagePassesOnConcertDate()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item itemUnderTest = programItems[4];
            Assert.Equal(itemUnderTest.SellIn, 15);
            Assert.Equal(itemUnderTest.Quality, 20);

            int timesToRun = 15;
            RunUpdateQuality(timesToRun, programInstance);

            Assert.Equal(itemUnderTest.SellIn, 0);
            Assert.Equal(itemUnderTest.Quality, 50);
        }

        [Fact]
        public void TestBackstagePassesAfterConcertDate()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item itemUnderTest = programItems[4];
            Assert.Equal(itemUnderTest.SellIn, 15);
            Assert.Equal(itemUnderTest.Quality, 20);

            int timesToRun = 16;
            RunUpdateQuality(timesToRun, programInstance);

            Assert.Equal(itemUnderTest.SellIn, -1);
            Assert.Equal(itemUnderTest.Quality, 0);
        }

        [Fact]
        public void TestForDecrementOfQualityOfConjuredItems()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item standardItem = programItems[5];
            Assert.Equal(standardItem.SellIn, 3);
            Assert.Equal(standardItem.Quality, 6);

            int timesToRun = 3;
            RunUpdateQuality(timesToRun, programInstance);

            Assert.Equal(standardItem.SellIn, 0);
            Assert.Equal(standardItem.Quality, 0);
        }

        [Fact]
        public void TestForDecrementOfQualityOfConjuredItemsPastSellByDate()
        {
            TestAssemblyTests programInstance = new TestAssemblyTests();
            var programItems = programInstance.Items;
            Item standardItem = programItems[5];
            Assert.Equal(standardItem.SellIn, 3);
            Assert.Equal(standardItem.Quality, 6);

            int timesToRun = 4;
            RunUpdateQuality(timesToRun, programInstance);

            Assert.Equal(standardItem.SellIn, -1);
            Assert.Equal(standardItem.Quality, 0);
        }
    }
}
 