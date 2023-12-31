﻿using ImperialNova.Database;
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
                    return context.products.Where(p => p._Name != null && p.IsDeleted == false && p._Name.ToLower()
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
        public List<Product> GetProductByFilter(DateTime? startDate, DateTime? endDate)
        {
            // Implement your logic to filter products by startDate and endDate
            // Example: return a filtered list of products
            var allProducts = GetProduct(); // You can call your existing method to get all products

            allProducts = allProducts.Where(p => p._ExportDate >= startDate && p._ExportDate <= endDate).ToList();

            return allProducts;
        }
        public List<Product> GetProducts()
        {
            using (var context = new DSContext())
            {
                var data = context.products
                    .Where(product => !product.IsDeleted)
                    .OrderBy(product => product._Name)
                    .ToList();

                return data;
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
                Product.IsDeleted = true;
                Product.Type = "Product";
                context.Entry(Product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
    
}

