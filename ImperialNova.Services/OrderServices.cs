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
    public class OrderServices
    {
        #region Singleton
        public static OrderServices Instance
        {
            get
            {
                if (instance == null) instance = new OrderServices();
                return instance;
            }
        }
        private static OrderServices instance { get; set; }
        private OrderServices()
        {
        }
        #endregion


        public List<Order> GetOrders()
        {
            using (var context = new DSContext())
            {
                var data = context.orders
                    .Where(order => !order.IsDeleted)
                    .OrderByDescending(order => order._Date) // Optionally, you can sort by a date field (e.g., OrderDate) in descending order.
                    .ToList();

                return data;
            }
        }
        public int GetLastOrderId()
        {
            using (var context = new DSContext())
            {
                var lastAdjustment = context.orders.OrderByDescending(a => a._Id).FirstOrDefault();
                if (lastAdjustment != null)
                {
                    return lastAdjustment._Id;
                }
                // Return a default value (e.g., -1) or throw an exception if there are no entries.
                // You can decide the appropriate behavior based on your application requirements.
                return -1; // Default value when there are no entries.
            }
        }
        public List<Order> GetReadyToShipOrders()
        {
            using (var context = new DSContext())
            {
                var data = context.orders
                    .Where(order => order._Status == "Ready To Ship" && !order.IsDeleted)
                    .ToList();

                data.Reverse();
                return data;
            }
        }
        public List<Order> GetShippedOrders()
        {
            using (var context = new DSContext())
            {
                var data = context.orders
                    .Where(order => order._Status == "Shipped" && !order.IsDeleted)
                    .ToList();

                data.Reverse();
                return data;
            }
        }
        public Entities.Order GetOrderById(int id)
        {
            using (var context = new DSContext())
            {
                return context.orders.Find(id);

            }
        }

        public void CreateOrder(Order Order)
        {
            using (var context = new DSContext())
            {
                context.orders.Add(Order);
                context.SaveChanges();
            }
        }

        public void UpdateOrder(Entities.Order Order)
        {
            using (var context = new DSContext())
            {
                context.Entry(Order).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


        public void DeleteOrder(int ID)
        {
            using (var context = new DSContext())
            {

                var Product = context.orders.Find(ID);
                Product.IsDeleted = true;
                Product.Type = "Order";
                context.Entry(Product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
