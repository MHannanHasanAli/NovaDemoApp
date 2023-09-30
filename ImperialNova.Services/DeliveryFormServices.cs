using ImperialNova.Database;
using ImperialNova.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ImperialNova.Services
{
    public class DeliveryFormServices
    {
        #region Singleton
        public static DeliveryFormServices Instance
        {
            get
            {
                if (instance == null) instance = new DeliveryFormServices();
                return instance;
            }
        }
        private static DeliveryFormServices instance { get; set; }
        public DeliveryFormServices()
        {
        }
        #endregion
        public List<DeliveryForm> GetDeliveryFormInfo(string SearchTerm = "")
        {
            using (var context = new DSContext())
            {
                if (SearchTerm == "")
                {
                    return context.deliveryform.OrderBy(x => x._CustomerName).ToList();
                }
                else
                {
                    return context.deliveryform.Where(p => p._CustomerName != null && p._CustomerName.ToLower().Contains(SearchTerm.ToLower()))
                                       .OrderBy(x => x._CustomerName)
                                       .ToList();
                }

            }
        }
        public void CreateDeliveryForm(DeliveryForm DeliveryForm)
        {
            using (var context = new DSContext())
            {
                context.deliveryform.Add(DeliveryForm);
                context.SaveChanges();
            }
        }

        public void UpdateDeliveryForm(DeliveryForm DeliveryForm)
        {
            using (var context = new DSContext())
            {
                context.Entry(DeliveryForm).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


        public void DeleteDeliveryForms(int id)
        {

            using (var context = new DSContext())
            {
                var Product = context.deliveryform.Find(id);
                context.deliveryform.Remove(Product);
                context.SaveChanges();
            }
        }
        public DeliveryForm GetLastEnteredDeliveryForm()
        {
            using (var context = new DSContext())
            {
                var lastEnteredForm = context.deliveryform.OrderByDescending(c => c._id).FirstOrDefault();
                return lastEnteredForm;
            }
        }
        public DeliveryForm GetFormById(int id)
        {
            using (var context = new DSContext())
            {
                return context.deliveryform.Find(id);

            }
        }
    }
}
