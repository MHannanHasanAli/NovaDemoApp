using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialNova.Entities
{
    public class Order
    {
        [Key] public int _Id { get; set; }
        public DateTime _Date { get; set; }
        public string _Record { get; set; }
        public DateTime _ShipByDate { get; set; }
        public string _Tracking { get; set; }
        public string _Status { get; set; }
        public int _CustomerId { get; set; }
        public string _Priority { get; set; }
        public int _ProductId { get; set; }
        public string _Photo { get; set; }
        public string _SKU { get; set; }
        public string _Title { get; set; }
        public int _Qty { get; set; }
        public decimal _Price { get; set; }
        public decimal _Amount { get; set; }
        public string _IsPacked { get; set; }
    }
}
