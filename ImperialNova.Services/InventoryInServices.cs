using ImperialNova.Database;
using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialNova.Services
{
    public class InventoryInServices
    {
        #region Singleton
        public static InventoryInServices Instance
        {
            get
            {
                if (instance == null) instance = new InventoryInServices();
                return instance;
            }
        }
        private static InventoryInServices instance { get; set; }
        private InventoryInServices()
        {
        }
        #endregion


        public int GetLastEntryId()
        {
            using (var context = new DSContext())
            {
                var lastAdjustment = context.inventoryins.OrderByDescending(a => a._Id).FirstOrDefault();
                if (lastAdjustment != null)
                {
                    return lastAdjustment._Id;
                }
                // Return a default value (e.g., -1) or throw an exception if there are no entries.
                // You can decide the appropriate behavior based on your application requirements.
                return -1; // Default value when there are no entries.
            }
        }
        public List<InventoryIn> GetInventoryIns()
        {
            using (var context = new DSContext())
            {
                var data = context.inventoryins.ToList();
                data.Reverse();
                return data;
            }
        }

        public Entities.InventoryIn GetInventoryInById(int id)
        {
            using (var context = new DSContext())
            {
                return context.inventoryins.Find(id);

            }
        }

        public void CreateInventoryIn(InventoryIn InventoryIn)
        {
            using (var context = new DSContext())
            {
                context.inventoryins.Add(InventoryIn);
                context.SaveChanges();
            }
        }

        public void UpdateInventoryIn(Entities.InventoryIn InventoryIn)
        {
            using (var context = new DSContext())
            {
                context.Entry(InventoryIn).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


        public void DeleteInventoryIn(int ID)
        {
            using (var context = new DSContext())
            {

                var Product = context.inventoryins.Find(ID);
                context.inventoryins.Remove(Product);
                context.SaveChanges();
            }
        }
    }
}
