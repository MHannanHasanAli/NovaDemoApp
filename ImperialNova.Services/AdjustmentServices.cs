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
    public class AdjustmentServices
    {
        #region Singleton
        public static AdjustmentServices Instance
        {
            get
            {
                if (instance == null) instance = new AdjustmentServices();
                return instance;
            }
        }
        private static AdjustmentServices instance { get; set; }
        private AdjustmentServices()
        {
        }
        #endregion


        public List<Adjustment> GetAdjustments()
        {
            using (var context = new DSContext())
            {
                var data = context.adjustments.ToList();
                data.Reverse();
                return data;
            }
        }
        public int GetLastAdjustmentId()
        {
            using (var context = new DSContext())
            {
                var lastAdjustment = context.adjustments.OrderByDescending(a => a._Id).FirstOrDefault();
                if (lastAdjustment != null)
                {
                    return lastAdjustment._Id;
                }
                // Return a default value (e.g., -1) or throw an exception if there are no entries.
                // You can decide the appropriate behavior based on your application requirements.
                return -1; // Default value when there are no entries.
            }
        }
        public Entities.Adjustment GetAdjustmentById(int id)
        {
            using (var context = new DSContext())
            {
                return context.adjustments.Find(id);

            }
        }

        public void CreateAdjustment(Adjustment Adjustment)
        {
            using (var context = new DSContext())
            {
                context.adjustments.Add(Adjustment);
                context.SaveChanges();
            }
        }

        public void UpdateAdjustment(Entities.Adjustment Adjustment)
        {
            using (var context = new DSContext())
            {
                context.Entry(Adjustment).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


        public void DeleteAdjustment(int ID)
        {
            using (var context = new DSContext())
            {

                var Product = context.adjustments.Find(ID);
                context.adjustments.Remove(Product);
                context.SaveChanges();
            }
        }
    }
}
