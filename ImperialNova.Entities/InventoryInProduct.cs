using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialNova.Entities
{
    public class InventoryInProduct : Backup
    {
        [Key] public int _Id { get; set; }
        public int _InventoryInId { get; set; }
        public string _Photo { get; set; }
        public string _SKU { get; set; }
        public string _Title { get; set; }
        public DateTime _ExpiryDate { get; set; }
        public string _Location { get; set; }
        public int _Qty { get; set; }
        public decimal _Price { get; set; }
        public decimal _Amount { get; set; }
        public int _CategoryId { get; set; }
        public int _WarehouseId { get; set; }
        public string _Warehouse { get; set; }
        public string _Size { get; set; }
        public string _Color { get; set; }

    }
}
