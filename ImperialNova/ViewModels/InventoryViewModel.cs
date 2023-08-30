using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImperialNova.ViewModels
{


    public class InventoryModel
    {
        public string _ProductId { get; set; }
        public string _Name { get; set; }
        public string _Size { get; set; }
        public string _Color { get; set; }
        public string _Cost { get; set; }
        public string _RetailPrice { get; set; }
        
        public string _QuantityIn { get; set; }
        public string _QuantityOut { get; set; }
        public string _Notes { get; set; }
        public string _Store { get; set; }
        public string _CategoryId { get; set; }
    }
    public class InventoryListingViewModel
    {
        public string SearchTerm { get; set; }

        public List<InventoryBackups> inventories { get; set; }

    }
    public class InventoryActionViewModel
    {
        public List<Product> Products { get; set; }
        public List<Locations> Locations { get; set; }
        public int _Id { get; set; }
        public int _ProductId { get; set; }
        public string _Name { get; set; }
        public string _Size { get; set; }
        public string _Color { get; set; }
        public decimal _Cost { get; set; }
        public decimal _RetailPrice { get; set; }
        public int _QuantityIn { get; set; }
        public int _QuantityOut { get; set; } = 0;
        public string _Notes { get; set; }
        public DateTime _ExportDate { get; set; }

        public int _CategoryId { get; set; }
        public string _Action { get; set; }

        public string _UserId { get; set; }
        public string _UserFullName { get; set; }
        public string _UserEmail { get; set; }
        public string _Store { get; set; }
    }
}