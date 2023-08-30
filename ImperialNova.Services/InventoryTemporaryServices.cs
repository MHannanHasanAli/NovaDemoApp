using ImperialNova.Database;
using ImperialNova.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ImperialNova.Services
{
    public class InventoryTemporaryServices
    {
        #region Singleton
        public static InventoryTemporaryServices Instance
        {
            get
            {
                if (instance == null) instance = new InventoryTemporaryServices();
                return instance;
            }
        }
        private static InventoryTemporaryServices instance { get; set; }
        private InventoryTemporaryServices()
        {
        }
        #endregion
        public void CreateInventoryTemporary(InventoryTemporary inventoryTemporary)
        {
            using (var context = new DSContext())
            {
                context.inventorytemporary.Add(inventoryTemporary);
                context.SaveChanges();
            }
        }
        public async Task<List<InventoryTemporary>> GetInventoriesAsync()
        {
            using (var context = new DSContext())
            {
                var data = await context.inventorytemporary.ToListAsync();
                data.Reverse();
                return data;
            }
        }
        public void Empty()
        {
            using (var context = new DSContext())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE inventorytemporary");
            }
        }
    }
}
