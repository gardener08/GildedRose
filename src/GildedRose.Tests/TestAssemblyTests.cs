using System.Collections.Generic;
using System.Reflection.Emit;
using Xunit;
using GildedRose.Console;

namespace GildedRose.Tests
{
    public class TestAssemblyTests : Program
    {
        [Fact]
        public void TestTheTruth()
        {
            Assert.True(true);
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

            for (int i = 0; i < 16; i++)
            {
                programInstance.UpdateQuality();
            }

            Assert.Equal(itemUnderTest.SellIn, -6);
            // Zero and not -2
            Assert.Equal(itemUnderTest.Quality, 0);

            // Scenario 2
            TestAssemblyTests programInstance2 = new TestAssemblyTests();
            var programItems2 = programInstance2.Items;

            Item secondItemUnderTest = programItems2[2];
            Assert.Equal(secondItemUnderTest.SellIn, 5);
            Assert.Equal(secondItemUnderTest.Quality, 7);

            for (int i = 0; i < 8; i++)
            {
                programInstance2.UpdateQuality();
            }

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

            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality(); // Sell By date reached
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();

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

            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();

            Assert.Equal(itemUnderTest.SellIn, -3);
            // The following is failing.  The acceptance criteria specifies that "Aged Brie" actually increases in Quality the older it gets.
            // I assumed that means by one quality point.  It is increasing by two after passing its expiration date.
            // Because I can't find this behavior specified in the story, I will need to have a conversation with the Product Owner to verify
            // that this is intended behavior and then we can either put in a fix or update the story.

            // I will forgo the general rule of making this test pass before proceeding as this is a roadblock that I can navigate around for now.
            Assert.Equal(itemUnderTest.Quality, 5);

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

            for (int i = 0; i < 65; i++)
            {
                programInstance.UpdateQuality();
            }

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

            // TODO: Make this a loop (14 times)
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();

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

            // TODO: Make this a loop (15 times)
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();

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

            // TODO: Make this a loop (16 times)
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();
            programInstance.UpdateQuality();

            Assert.Equal(itemUnderTest.SellIn, -1);
            Assert.Equal(itemUnderTest.Quality, 0);
        }
    }
}