using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialNova.Entities
{
    public class OrderProduct : DeleteUpdate
    {
        [Key] public int _Id { get; set; }
        public int _OrderId { get; set; }
        public int _ProductId { get; set; }
        public string _Photo { get; set; }
        public string _SKU { get; set; }
        public string _Title { get; set; }
        public int _Qty { get; set; }
        public decimal _Price { get; set; }
        public decimal _Cost { get; set; }
        public decimal _Amount { get; set; }
        public string _IsPacked { get; set; }
        public string  _location { get; set; }
        public string _Size { get; set; }
        public string _Color { get; set; }
    }
}
