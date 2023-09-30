using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialNova.Entities
{
    public class Expenses : DeleteUpdate
    {
        [Key]
        public int _Id { get; set; }
        public decimal _TotalSales { get; set; }
        //public decimal _ProductCost { get; set; }
        //public decimal _VanExpenses { get; set; }
        //public decimal _Car { get; set; }
        //public decimal _Logistic { get; set; }
        //public decimal _Storage { get; set; }
        //public decimal _Rent { get; set; }
        //public decimal _SalesPerson { get; set; }
        //public decimal _Vat { get; set; }
        //public decimal _BusinessRate { get; set; }
        //public decimal _Utilities { get; set; }
        public decimal _TotalExpenses { get; set; }
        //public decimal _Left { get; set; }
        //public decimal _Tax { get; set; }
        //public decimal _Bank { get; set; }

        public string _Title { get; set; }
        public DateTime _Date { get; set; }
    }
}
