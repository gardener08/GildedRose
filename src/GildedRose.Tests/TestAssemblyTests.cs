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
    }
}