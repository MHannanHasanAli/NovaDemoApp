using ImperialNova.Database;
using ImperialNova.Entities;

namespace ImperialNova.Services
{
    public class InventoryServices
    {
        public void CreateInventory(Inventory Inventory)
        {
            using (var context = new DSContext())
            {
                context.inventories.Add(Inventory);
                context.SaveChanges();
            }
        }
    }
}
