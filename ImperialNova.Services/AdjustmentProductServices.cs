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
    public class AdjustmentProductServices
    {
        #region Singleton
        public static AdjustmentProductServices Instance
        {
            get
            {
                if (instance == null) instance = new AdjustmentProductServices();
                return instance;
            }
        }
        private static AdjustmentProductServices instance { get; set; }
        private AdjustmentProductServices()
        {
        }
        #endregion


        public List<AdjustmentProduct> GetAdjustmentProducts()
        {
            using (var context = new DSContext())
            {
                var data = context.adjustmentproducts.ToList();
                data.Reverse();
                return data;
            }
        }
        public List<AdjustmentProduct> GetAdjustmentProducts(int adjustmentId)
        {
            using (var context = new DSContext())
            {
                var data = context.adjustmentproducts
                    .Where(ap => ap._AdjustmentId == adjustmentId)
                    .ToList();

                return data;
            }
        }
        public Entities.AdjustmentProduct GetAdjustmentProductById(int id)
        {
            using (var context = new DSContext())
            {
                return context.adjustmentproducts.Find(id);

            }
        }

        public void CreateAdjustmentProduct(AdjustmentProduct AdjustmentProduct)
        {
            using (var context = new DSContext())
            {
                context.adjustmentproducts.Add(AdjustmentProduct);
                context.SaveChanges();
            }
        }

        public void UpdateAdjustmentProduct(Entities.AdjustmentProduct AdjustmentProduct)
        {
            using (var context = new DSContext())
            {
                context.Entry(AdjustmentProduct).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


        public void DeleteAdjustmentProduct(int ID)
        {
            using (var context = new DSContext())
            {

                var Product = context.adjustmentproducts.Find(ID);
                context.adjustmentproducts.Remove(Product);
                context.SaveChanges();
            }
        }
    }
}
