using ImperialNova.Database;
using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ImperialNova.Services
{
    public class ProductServices
    {
        #region Singleton
        public static ProductServices Instance
        {
            get
            {
                if (instance == null) instance = new ProductServices();
                return instance;
            }
        }
        private static ProductServices instance { get; set; }
        private ProductServices()
        {
        }
        #endregion

        public List<Product> GetProductsByStore(int storeId)
        {
            using (var context = new DSContext())
            {
                return context.products
                    .Where(x => x._WarehouseId == storeId)
                    .OrderBy(x => x._Name)
                    .ToList();
            }
        }
        public List<Product> GetProduct(string SearchTerm = "")
        {
            using (var context = new DSContext())
            {
                if (SearchTerm != "")
                {
                    return context.products.Where(p => p._Name != null && p._Name.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x._Name)
                                            .ToList();
                }
                else
                {
                    return context.products.OrderBy(x=>x._Name).ToList();
                }
            }
        }
        public List<Product> GetProducts()
        {
            using (var context = new DSContext())
            {
                
               return context.products.OrderBy(x => x._Name).ToList();
               
            }
        }

        public Product GetProductById(int id)
        {
            using (var context = new DSContext())
            {
                return context.products.Find(id);

            }
        }

        public void CreateProduct(Product Product)
        {
            using (var context = new DSContext())
            {
                context.products.Add(Product);
                context.SaveChanges();
            }
        }

        public void UpdateProduct(Product Product)
        {
            using (var context = new DSContext())
            {
                context.Entry(Product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteProduct(int ID)
        {
            using (var context = new DSContext())
            {

                var Product = context.products.Find(ID);
                context.products.Remove(Product);
                context.SaveChanges();
            }
        }
    }
    
}

