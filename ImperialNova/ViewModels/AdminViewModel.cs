﻿using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImperialNova.ViewModels
{

    public class DataPoint
    {
        public DateTime Month { get; set; }
        public double QuantityIn { get; set; }
        public double QuantityOut { get; set; }
    }
    public class InventoriesModel
    {
        public List<InventoryInProduct> Inventories { get; set; }
    }
    public class OrdersModel
    {
        public List<OrderProduct> Orders { get; set; }

    }
    public class StockModel
    {
        public List<Product> Products { get; set; }

    }
    public class AdminViewModel
    {
        public int InventoryIn { get; set; }
        public int InventoryOut { get; set; }
        public int CurrentStock { get; set; }
        public int ActiveProducts { get; set; }
        public int LowStockAlert { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalCost { get; set; }
        public decimal Profit { get; set; }
        public int Customer { get; set; }
        public List<InventoryBackups> InventoriesForChart { get; set; }
        public List<Product> LowStockProducts { get; set; }
        public User SignedInUser { get; set; }
        public string Name { get; set; }

        public string ID { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Contact { get; set; }
        public string SearchTerm { get; set; }

        public List<Order> Order { get; set; }
        public List<InventoryIn> Inventory { get; set; }
        public List<TodoList> TodoList { get; set; }
        public List<Reminder> Reminders { get; set; }
        public List<Expenses> Expenses { get; set; }
        public List<Product> Fastmoving { get; set; }
        public List<Product> Slowmoving { get; set; }

        public List<Locations> warehouses { get; set; }
    }


    public class ManagerViewModel
    {
        public User SignedInUser { get; set; }
    }
    public class UpdateExpenseAdmin
    {
        public string price { get; set; }
        public string cost { get; set; }
        public string profit { get; set; }
        public string expenses { get; set; }

    }

    public class warehousedata
    {
        public int inventorycount { get; set; }
        public int orderscount { get; set; }
        public int stockcount { get; set; }
        public int lowstockcount { get; set; }
    }
}

