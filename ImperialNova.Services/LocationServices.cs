
using ImperialNova.Database;
using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace ImperialNova.Services
{
    public class LocationsServices
    {
        #region Singleton
        public static LocationsServices Instance
        {
            get
            {
                if (instance == null) instance = new LocationsServices();
                return instance;
            }
        }
        private static LocationsServices instance { get; set; }
        private LocationsServices()
        {
        }
        #endregion
        public List<Locations> GetLocations(string SearchTerm = "")
        {
            using (var context = new DSContext())
            {
                var query = context.locations.Where(location => !location.IsDeleted);

                if (!string.IsNullOrWhiteSpace(SearchTerm))
                {
                    query = query.Where(location => location._LocationName != null && location._LocationName.ToLower().Contains(SearchTerm.ToLower()));
                }

                var data = query.OrderBy(location => location._LocationName).ToList();
                return data;
            }
        }

        public List<string> GetLocationsNames()
        {
            using (var context = new DSContext())
            {
                var data = context.locations.Select(c => c._LocationName).ToList();
                data.Reverse();
                return data;
            }
        }
        public Locations GetLocationsInLocationss(int Sentid)
        {
            using (var context = new DSContext())
            {
                var Locations = context.locations.FirstOrDefault(c => c._Id == Sentid);
                return Locations;

            }
        }
        public List<Locations> GetLocationss()
        {
            using (var context = new DSContext())
            {
                var data = context.locations
                    .Where(location => !location.IsDeleted)
                    .ToList();

                data.Reverse();
                return data;
            }
        }


        public Entities.Locations GetLocationsById(int id)
        {
            using (var context = new DSContext())
            {
                return context.locations.Find(id);

            }
        }

        public void CreateLocations(Locations Locations)
        {
            using (var context = new DSContext())
            {
                context.locations.Add(Locations);
                context.SaveChanges();
            }
        }

        public void UpdateLocations(Entities.Locations Locations)
        {
            using (var context = new DSContext())
            {
                context.Entry(Locations).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteLocations(int id)
        {

            using (var context = new DSContext())
            {
                var Product = context.locations.Find(id);
                Product.IsDeleted = true;
                Product.Type = "Warehouse";
                context.Entry(Product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
