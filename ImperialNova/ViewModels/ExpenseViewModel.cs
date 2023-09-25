using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImperialNova.ViewModels
{
    public class ExpenseViewModel
    {
    }
    public class ExpenseListingViewModel
    {
        public List<Entities.Expenses> expense { get; set; }

    }

    public class ExpenseActionViewModel
    {
        public int _Id { get; set; }
        public decimal _TotalSales { get; set; }
        public decimal _ProductCost { get; set; }
        public decimal _VanExpenses { get; set; }
        public decimal _Car { get; set; }
        public decimal _Logistic { get; set; }
        public decimal _Storage { get; set; }
        public decimal _Rent { get; set; }
        public decimal _SalesPerson { get; set; }
        public decimal _Vat { get; set; }
        public decimal _BusinessRate { get; set; }
        public decimal _Utilities { get; set; }
        public decimal _TotalExpenses { get; set; }
        public decimal _Left { get; set; }
        public decimal _Tax { get; set; }
        public decimal _Bank { get; set; }
        public string _Title { get; set; }
        public DateTime _Date { get; set; }
    }

    public class UpdateExpense
    {
       public decimal price { get; set; }
       public decimal cost { get; set; }
        public decimal profit { get; set; }
        public decimal expenses { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public List<Expenses> Expenses { get; set; }

    }
}