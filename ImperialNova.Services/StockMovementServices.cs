using ImperialNova.Database;
using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace ImperialNova.Services
{
    public class StockMovementServices
    {
        #region Singleton
        public static StockMovementServices Instance
        {
            get
            {
                if (instance == null) instance = new StockMovementServices();
                return instance;
            }
        }
        private static StockMovementServices instance { get; set; }
        private StockMovementServices()
        {
        }
        #endregion

        public Entities.StockMovement GetStockMovementById(int id)
        {
            using (var context = new DSContext())
            {
                return context.stockmovements.Find(id);

            }
        }
        public List<Product> GetProductByRange(DateTime startDate, DateTime endDate)
        {
            using (var context = new DSContext())
            {
                return context.products.Where(p => p._ExportDate >= startDate && p._ExportDate <= endDate)
                .ToList();

            }
        }
        public void UpdateStockMovement(Entities.StockMovement StockMovement)
        {
            using (var context = new DSContext())
            {
                context.Entry(StockMovement).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
