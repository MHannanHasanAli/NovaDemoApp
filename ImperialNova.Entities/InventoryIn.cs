using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialNova.Entities
{
    public class InventoryIn
    {
        [Key] public int _Id { get; set; }
        public DateTime _Date { get; set; }
        public string _ShippingCompany { get; set; }
        public string _Tracking { get; set; }
        public string _Supplier { get; set; }
        public string _Status { get; set; }     
        public int _Quantity { get; set; }
        public decimal _Amount { get; set; }

    }
}
