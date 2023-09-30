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
    public class CustomerServices
    {
        #region Singleton
        public static CustomerServices Instance
        {
            get
            {
                if (instance == null) instance = new CustomerServices();
                return instance;
            }
        }
        private static CustomerServices instance { get; set; }
        private CustomerServices()
        {
        }
        #endregion
        public List<Customer> GetCustomer(string SearchTerm = "")
        {
            using (var context = new DSContext())
            {
                if (SearchTerm != "")
                {
                    return context.customers.Where(p => p._Name != null && p._Name.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x._Name)
                                            .ToList();
                }
                else
                {
                    return context.customers.OrderBy(x => x._Name).ToList();
                }
            }
        }
        public List<string> GetCustomerNames()
        {
            using (var context = new DSContext())
            {
                var data = context.customers.Select(c => c._Name).ToList();
                data.Reverse();
                return data;
            }
        }
        public Customer GetCustomerInCustomers(int Sentid)
        {
            using (var context = new DSContext())
            {
                var category = context.customers.FirstOrDefault(c => c._Id == Sentid);
                return category;

            }
        }
        public List<Customer> GetCustomers()
        {
            using (var context = new DSContext())
            {
                var data = context.customers
                    .Where(customer => !customer.IsDeleted)
                    .ToList();

                data.Reverse();
                return data;
            }
        }
        public List<Customer> GetCustomers(string SearchTerm)
        {
            using (var context = new DSContext())
            {
                return context.customers.Where(p => p._Name != null && p._Name.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x._Name)
                                            .ToList();
            }
        }



        public Entities.Customer GetCustomerById(int id)
        {
            using (var context = new DSContext())
            {
                return context.customers.Find(id);

            }
        }

        public void CreateCustomer(Customer Customer)
        {
            using (var context = new DSContext())
            {
                context.customers.Add(Customer);
                context.SaveChanges();
            }
        }

        public void UpdateCustomer(Entities.Customer Customer)
        {
            using (var context = new DSContext())
            {
                context.Entry(Customer).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


        public void DeleteCustomer(int ID)
        {
            using (var context = new DSContext())
            {

                var Product = context.customers.Find(ID);
                Product.IsDeleted = true;
                Product.Type = "Customer";
                context.Entry(Product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
