using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImperialNova.ViewModels
{
    public class ProductModel
    {
        public string _ProductId { get; set; }
        public string _Photo { get; set; }
        public string _Name { get; set; }
        public string _SKU { get; set; }
        public string _Quantity { get; set; }
        public string _Price { get; set; }
        public string _Amount { get; set; }
        public string _ExpiryDate { get; set; }
    }
    public class ProductListingViewModel
    {
       
        public List<ProductsModel> Products { get; set; }
        public ProductPreviewModel Product { get; set; }
        public string Category { get; set; }
        public string Warehouse { get; set; }
        public string SearchTerm { get; set; }

        public List<Order> order { get; set; }
    }
    public class ProductPreviewModel
    {
        public Product Product { get; set; }
        public Category Category { get; set; }

        public Locations Warehouse { get; set; }
        public List<Order> order { get; set; }
        public List<OrderProduct> orderProducts { get; set; }
    }
    public class ProductsModel
    {
        public Product Product { get; set; }
        public Category Category { get; set; }

        public Locations Warehouse { get; set; }
    }
    public class ProductActionViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Locations> Warehouses { get; set; }
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
        public int _Quantity { get; set; }
        public int _QuantityOut { get; set; } = 0;
        public int _OpeningStock { get; set; }
        public string _Notes { get; set; }
        public DateTime _ExportDate { get; set; } = DateTime.Now;

        public int _CategoryId { get; set; }
        public int _WarehouseId { get; set; }
        public int _LowStockAlert { get; set; }
        public string _Photo { get; set; }

    }
}