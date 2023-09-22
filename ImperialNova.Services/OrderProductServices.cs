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
    public class OrderProductServices
    {
        #region Singleton
        public static OrderProductServices Instance
        {
            get
            {
                if (instance == null) instance = new OrderProductServices();
                return instance;
            }
        }
        private static OrderProductServices instance { get; set; }
        private OrderProductServices()
        {
        }
        #endregion
        public List<Entities.OrderProduct> GetOrderProductsByInventoryInId(int inventoryInId)
        {
            using (var context = new DSContext())
            {
                return context.orderproducts
                    .Where(product => product._OrderId == inventoryInId)
                    .ToList();
            }
        }
        public List<Entities.OrderProduct> GetOrderProductsByProductId(int inventoryInId)
        {
            using (var context = new DSContext())
            {
                return context.orderproducts
                    .Where(product => product._ProductId == inventoryInId)
                    .ToList();
            }
        }
        public List<OrderProduct> GetOrderProducts()
        {
            using (var context = new DSContext())
            {
                var data = context.orderproducts.ToList();
                data.Reverse();
                return data;
            }
        }

        public Entities.OrderProduct GetOrderProductsById(int id)
        {
            using (var context = new DSContext())
            {
                return context.orderproducts.Find(id);

            }
        }

        public void CreateOrderProducts(OrderProduct OrderProducts)
        {
            using (var context = new DSContext())
            {
                context.orderproducts.Add(OrderProducts);
                context.SaveChanges();
            }
        }

        public void UpdateOrderProducts(Entities.OrderProduct OrderProducts)
        {
            using (var context = new DSContext())
            {
                context.Entry(OrderProducts).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


        public void DeleteOrderProducts(int ID)
        {
            using (var context = new DSContext())
            {

                var Product = context.orderproducts.Find(ID);
                context.orderproducts.Remove(Product);
                context.SaveChanges();
            }
        }
    }
}
