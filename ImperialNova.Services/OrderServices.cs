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
                var data = context.orders.ToList();
                data.Reverse();
                return data;
            }
        }
        public List<Order> GetReadyToShipOrders()
        {
            using (var context = new DSContext())
            {
                var data = context.orders
                    .Where(order => order._Status == "Ready To Ship")
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
                    .Where(order => order._Status == "Shipped")
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
                context.orders.Remove(Product);
                context.SaveChanges();
            }
        }
    }
}
