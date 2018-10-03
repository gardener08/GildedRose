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
            Assert.NotNull(Program.Items);
        }

        [Fact]
        public void TestLegendaryItem()
        {
            var programItems = Program.Items;
            Item legendaryItem = programItems[3];
            Assert.Contains("Sulfuras",legendaryItem.Name);
            int currentSellIn = legendaryItem.SellIn;
            int currentQuality = legendaryItem.Quality;

            Program.UpdateQuality();

            int newSellIn = legendaryItem.SellIn;
            int newQuality = legendaryItem.Quality;

            Assert.Equal(currentSellIn, newSellIn);
            Assert.Equal(currentQuality, newQuality);
        }
    }
}