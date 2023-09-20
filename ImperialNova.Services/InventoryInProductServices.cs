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
    public class InventoryInProductServices
    {
        #region Singleton
        public static InventoryInProductServices Instance
        {
            get
            {
                if (instance == null) instance = new InventoryInProductServices();
                return instance;
            }
        }
        private static InventoryInProductServices instance { get; set; }
        private InventoryInProductServices()
        {
        }
        #endregion


        public List<InventoryInProduct> GetInventoryInProducts()
        {
            using (var context = new DSContext())
            {
                var data = context.inventoryinproducts.ToList();
                data.Reverse();
                return data;
            }
        }

        public Entities.InventoryInProduct GetInventoryInProductsById(int id)
        {
            using (var context = new DSContext())
            {
                return context.inventoryinproducts.Find(id);

            }
        }

        public void CreateInventoryInProducts(InventoryInProduct InventoryInProducts)
        {
            using (var context = new DSContext())
            {
                context.inventoryinproducts.Add(InventoryInProducts);
                context.SaveChanges();
            }
        }

        public void UpdateInventoryInProducts(Entities.InventoryInProduct InventoryInProducts)
        {
            using (var context = new DSContext())
            {
                context.Entry(InventoryInProducts).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


        public void DeleteInventoryInProducts(int ID)
        {
            using (var context = new DSContext())
            {

                var Product = context.inventoryinproducts.Find(ID);
                context.inventoryinproducts.Remove(Product);
                context.SaveChanges();
            }
        }
    }
}
