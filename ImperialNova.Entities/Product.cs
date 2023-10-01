using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImperialNova.Entities
{

    public class Product
    {
        [Key]
        public int _Id { get; set; }
        public string _Name { get; set; }
        public string _Size { get; set; }
        public string _Color { get; set; }
        public decimal _Cost { get; set; }
        public string _SKU { get; set; }
        public string _Weight { get; set; }
        public string _Thickness { get; set; }
        public int _Variations { get; set; }
        public int _OpeningStock { get; set; }
        public decimal _RetailPrice { get; set;}
        public int _Quantity { get; set; }
        public int _QuantityIn { get; set; }
        public int _QuantityOut { get; set; } = 0;
        public string _Notes { get; set; }
        public DateTime _ExportDate { get; set; } = DateTime.Now;

        public int _CategoryId { get; set; }
        public int _WarehouseId { get; set; }
        public int _LowStockAlert { get; set; }
        public string _Photo { get; set; }
        public string _Category { get; set; }
        public string _Warehouse { get; set; }
        public bool IsDeleted { get; set; }
        public string Type { get; set; }
    }
}
