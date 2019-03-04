using System.Collections.Generic;
using System.Linq;

namespace GildedRose.Console
{
    public class InventoryUpdater
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            InventoryUpdater app = new InventoryUpdater();

            app.UpdateQuality();

            System.Console.ReadKey();

        }

        public void UpdateQuality()
        {
            InventoryManagement inventoryManager = new InventoryManagement();
            inventoryManager.UpdateQuality();
        }
    }
}
