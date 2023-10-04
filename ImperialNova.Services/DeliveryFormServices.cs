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
                var query = context.deliveryform.Where(deliveryForm => !deliveryForm.IsDeleted);

                if (!string.IsNullOrWhiteSpace(SearchTerm))
                {
                    query = query.Where(deliveryForm => deliveryForm._CustomerName != null && deliveryForm._CustomerName.ToLower().Contains(SearchTerm.ToLower()));
                }

                var data = query.OrderBy(deliveryForm => deliveryForm._CustomerName).ToList();

                return data;
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
        public int GetLastEntryId()
        {
            using (var context = new DSContext())
            {
                var lastAdjustment = context.deliveryform.OrderByDescending(a => a._id).FirstOrDefault();
                if (lastAdjustment != null)
                {
                    return lastAdjustment._id;
                }

                return -1;
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
                Product.IsDeleted = true;
                Product.Type = "Delivery Form";
                context.Entry(Product).State = EntityState.Modified;
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
