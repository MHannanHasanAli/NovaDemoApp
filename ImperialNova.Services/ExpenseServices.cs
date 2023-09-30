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
    public class ExpenseServices
    {
        #region Singleton
        public static ExpenseServices Instance
        {
            get
            {
                if (instance == null) instance = new ExpenseServices();
                return instance;
            }
        }
        private static ExpenseServices instance { get; set; }
        private ExpenseServices()
        {
        }
        #endregion
        public List<Expenses> GetExpenses()
        {
            using (var context = new DSContext())
            {
                var data = context.expenses
                    .Where(expense => !expense.IsDeleted)
                    .ToList();

                data.Reverse();
                return data;
            }
        }
        public void CreateExpense(Expenses Expense)
        {
            using (var context = new DSContext())
            {
                context.expenses.Add(Expense);
                context.SaveChanges();
            }
        }
        public Expenses GetExpense(int ID)
        {
            using (var context = new DSContext())
            {
                var dta = context.expenses.Find(ID);
                

                return dta;

            }
        }
        public Entities.Expenses GetExpenseById(int id)
        {
            using (var context = new DSContext())
            {
                var dta = context.expenses.Find(id);
                return dta;

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
        public void UpdateExpense(Entities.Expenses Expense)
        {
            using (var context = new DSContext())
            {
                context.Entry(Expense).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteExpense(int ID)
        {
            using (var context = new DSContext())
            {

                var Product = context.expenses.Find(ID);
                Product.IsDeleted = true;
                Product.Type = "Expense";
                context.Entry(Product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
