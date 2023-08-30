
using ImperialNova.Database;
using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace ImperialNova.Services
{
    public class CategoryServices
    {
        #region Singleton
        public static CategoryServices Instance
        {
            get
            {
                if (instance == null) instance = new CategoryServices();
                return instance;
            }
        }
        private static CategoryServices instance { get; set; }
        private CategoryServices()
        {
        }
        #endregion
        public List<Category> GetCategory(string SearchTerm = "")
        {
            using (var context = new DSContext())
            {
                if (SearchTerm != "")
                {
                    return context.categories.Where(p => p._CName != null && p._CName.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x._CName)
                                            .ToList();
                }
                else
                {
                    return context.categories.OrderBy(x => x._CName).ToList();
                }
            }
        }
        public List<string> GetCategoryNames()
        {
            using (var context = new DSContext())
            {
                var data = context.categories.Select(c => c._CName).ToList();
                data.Reverse();
                return data;
            }
        }
        public Category GetCategoryInCategorys(int Sentid)
        {
             using (var context = new DSContext())
            {
                var category =context.categories.FirstOrDefault(c => c._Id == Sentid);
                return category;

            }
        }
        public List<Category> GetCategorys()
        {
            using (var context = new DSContext())
            {
                var data = context.categories.ToList();
                data.Reverse();
                return data;
            }
        } 
        public List<Category> GetCategorys(string SearchTerm)
        {
            using (var context = new DSContext())
            {
                return context.categories.Where(p => p._CName != null && p._CName.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x._CName)
                                            .ToList();
            }
        }



        public Entities.Category GetCategoryById(int id)
        {
            using (var context = new DSContext())
            {
                return context.categories.Find(id);

            }
        }

        public void CreateCategory(Category Category)
        {
            using (var context = new DSContext())
            {
                context.categories.Add(Category);
                context.SaveChanges();
            }
        }

        public void UpdateCategory(Entities.Category Category)
        {
            using (var context = new DSContext())
            {
                context.Entry(Category).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


        public void DeleteCategory(int ID)
        {
            using (var context = new DSContext())
            {

                var Product = context.categories.Find(ID);
                context.categories.Remove(Product);
                context.SaveChanges();
            }
        }
      
    }
}
