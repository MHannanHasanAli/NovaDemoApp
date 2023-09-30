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
    public class CSVServices
    {
        #region Singleton
        public static CSVServices Instance
        {
            get
            {
                if (instance == null) instance = new CSVServices();
                return instance;
            }
        }
        private static CSVServices instance { get; set; }
        private CSVServices()
        {
        }
        #endregion


        public List<CSV> GetCSVs()
        {
            using (var context = new DSContext())
            {
                var data = context.csvs
                    .Where(csv => !csv.IsDeleted)
                    .ToList();

                data.Reverse();
                return data;
            }
        }

        public Entities.CSV GetCSVById(int id)
        {
            using (var context = new DSContext())
            {
                return context.csvs.Find(id);

            }
        }

        public void CreateCSV(CSV CSV)
        {
            using (var context = new DSContext())
            {
                context.csvs.Add(CSV);
                context.SaveChanges();
            }
        }

        public void UpdateCSV(Entities.CSV CSV)
        {
            using (var context = new DSContext())
            {
                context.Entry(CSV).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


        public void DeleteCSV(int ID)
        {
            using (var context = new DSContext())
            {

                var Product = context.csvs.Find(ID);
                Product.IsDeleted = true;
                Product.Type = "CSV";
                context.Entry(Product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
