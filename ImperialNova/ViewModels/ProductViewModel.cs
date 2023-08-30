using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImperialNova.ViewModels
{
    public class ProductListingViewModel
    {
        public List<ProductsModel> Products { get; set; }
        public string SearchTerm { get; set; }
    }
    public class ProductsModel
    {
        public Product Product { get; set; }
        public Category Category { get; set; }
    }
    public class ProductActionViewModel
    {
        public List<Category> Categories { get; set; }
        public int _Id { get; set; }
        public string _Name { get; set; }
        public string _Size { get; set; }
        public string _Color { get; set; }
        public decimal _Cost { get; set; }
        public string _SKU { get; set; }
        public string _Weight { get; set; }
        public string _Thickness { get; set; }
        public int _Variations { get; set; }
        public decimal _RetailPrice { get; set; }
        public int _QuantityIn { get; set; }
        public int _QuantityOut { get; set; } = 0;
        public int _OpeningStock { get; set; }
        public string _Notes { get; set; }
        public DateTime _ExportDate { get; set; } = DateTime.Now;

        public int _CategoryId { get; set; }
        public string _Photo { get; set; }

    }
}