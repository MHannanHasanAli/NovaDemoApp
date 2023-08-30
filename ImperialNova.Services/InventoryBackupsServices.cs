using ImperialNova.Database;
using ImperialNova.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ImperialNova.Services
{
    public class InventoryBackupsServices
    {
        public static InventoryBackupsServices Instance
        {
            get
            {
                if (instance == null) instance = new InventoryBackupsServices();
                return instance;
            }
        }
        private static InventoryBackupsServices instance { get; set; }
        private InventoryBackupsServices()
        {
        }
        public List<InventoryBackups> GetInventories(string SearchTerm = "")
        {
            using (var context = new DSContext())
            {
                if (SearchTerm != "")
                {
                    return context.inventorybackups.Where(p => p._Name != null && p._Name.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x._Name)
                                            .ToList();
                }
                else
                {
                    return context.inventorybackups.OrderBy(x => x._Name).ToList();
                }
            }
        }
        public async Task<List<InventoryBackups>> GetInventoryBackupAsync()
        {
            using (var context = new DSContext())
            {
                var data = await context.inventorybackups.ToListAsync();
                data.Reverse();
                return data;
            }
        }
        public List<InventoryBackups> GetInventoryBackup()
        {
            using (var context = new DSContext())
            {
                var data = context.inventorybackups.ToList();
                data.Reverse();
                return data;
            }
        }
        public List<InventoryBackups> GetInventoryBackupByStore()
        {
            using (var context = new DSContext())
            {
                var data = context.inventorybackups.ToList();

                data.Reverse();
                return data;
            }
        }
        public List<InventoryBackups> GetInventoryBackupByStore(string locationName)
        {
            using (var context = new DSContext())
            {
                var data = context.inventorybackups
                                .Where(backup => backup._Store == locationName)
                                .ToList();

                data.Reverse();
                return data;
            }
        }

        public InventoryBackups GetInventoryById(int id)
        {
            using (var context = new DSContext())
            {
                var data = context.inventorybackups.Find(id);
                return data;
            }
        }
        public List<InventoryBackups> GetInventoryBackup(string store)
        {
            using (var context = new DSContext())
            {
                var data = context.inventorybackups.Where(x => x._Store == store).ToList();
                data.Reverse();
                return data;
            }
        }
        public void CreateInventoryBackup(InventoryBackups inventoryBackups)
        {
            using (var context = new DSContext())
            {
                context.inventorybackups.Add(inventoryBackups);
                context.SaveChanges();
            }
        }
        public void UpdateInventory(Entities.InventoryBackups Inventory)
        {
            using (var context = new DSContext())
            {
                context.Entry(Inventory).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteInventory(int id)
        {


            using (var context = new DSContext())
            {
                var Product = context.inventorybackups.Find(id);
                context.inventorybackups.Remove(Product);
                context.SaveChanges();
            }
          
        }
    }
}
