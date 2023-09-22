using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ImperialNova.Services;
using ImperialNova.ViewModels;
using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace ImperialNova.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private AMSignInManager _signInManager;
        private AMRolesManager _rolesManager;
        private AMUserManager _userManager;
        public AMUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AMUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public AMRolesManager RolesManager
        {
            get
            {
                return _rolesManager ?? HttpContext.GetOwinContext().GetUserManager<AMRolesManager>();
            }
            private set
            {
                _rolesManager = value;
            }
        }
        public AMSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<AMSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        // GET: Admin
        public ActionResult Index()
        {
            //AdminViewModel model = new AdminViewModel();
            //var user = UserManager.FindById(User.Identity.GetUserId());
            //model.Name = user.Name;
            return View();
        }




        public ActionResult Dashboard()
        {
            AdminViewModel model = new AdminViewModel();
            model.InventoriesForChart = InventoryBackupsServices.Instance.GetInventoryBackup().Where(x => x.JustAdded == false).ToList();
            List<DataPoint> dataPoints = new List<DataPoint>();

            var readytoshiporders = new List<Order>();
            var inventories = new List<InventoryIn>();
            var todolist = new List<TodoList>();
            var reminders = new List<Reminder>();
            List<Product> fast_moving_products = new List<Product>();
            List<Product> slow_moving_products = new List<Product>();

            foreach (var item in model.InventoriesForChart)
            {
                dataPoints.Add(new DataPoint { Month = item._ExportDate, QuantityIn = item._QuantityIn, QuantityOut = item._QuantityOut });
            }
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            var orders = OrderServices.Instance.GetReadyToShipOrders();
            foreach (var item in orders)
            {
                readytoshiporders.Add(item);
            }
            
            var inventorydata = InventoryInServices.Instance.GetInventoryIns();
            foreach (var item in inventorydata)
            {
                inventories.Add(item);
            }

            var listitem = TodoListServices.Instance.GetFilteredTodoLists();
            foreach (var item in listitem)
            {
                todolist.Add(item);
            }

            var reminder = ReminderServices.Instance.GetReminders();
            foreach (var item in reminder)
            {
                reminders.Add(item);
            }

            var stockmovementdata = StockMovementServices.Instance.GetStockMovementById(1);

            var fast_products = GetProductsWithinDateRange(stockmovementdata._Fdays);
            var slow_products = GetProductsWithinDateRange(stockmovementdata._Sdays);

            var products = OrderProductServices.Instance.GetOrderProducts();

            foreach (var fastproduct in fast_products)
            {
                int totalnumber = products.Count();
                int fastcount = 0;
                foreach (var product in products)
                {
                    if (fastproduct._Id == product._ProductId)
                    {
                        fastcount++;
                    }
                }
                if (((fastcount / totalnumber) * 100) >= stockmovementdata._Ffrom && ((fastcount / totalnumber) * 100) <= stockmovementdata._Fto)
                {
                    fast_moving_products.Add(fastproduct);
                }
            }
            foreach (var slowproduct in slow_products)
            {
                int totalnumber = products.Count();
                int slowcount = 0;
                foreach (var product in products)
                {
                    if (slowproduct._Id == product._ProductId)
                    {
                        slowcount++;
                    }
                }
                if (((slowcount / totalnumber) * 100) >= stockmovementdata._Sfrom && ((slowcount / totalnumber) * 100) <= stockmovementdata._Sto)
                {
                    slow_moving_products.Add(slowproduct);
                }
            }

            int inventoryIn = 0;
            int inventoryOut = 0;
            int stock = 0;
            var inventoryins = InventoryInServices.Instance.GetInventoryIns();
            foreach (var item in inventoryins)
            {
                inventoryIn = inventoryIn + item._Quantity;
            }

            var inventoryouts = OrderServices.Instance.GetOrders();
            foreach (var item in inventoryouts)
            {
                inventoryOut = inventoryOut + item._Quantity;
            }

            var totalstock = ProductServices.Instance.GetProducts();
            foreach (var item in products)
            {
                stock = stock + item._Qty;
            }

            var customers = CustomerServices.Instance.GetCustomers();
            
            model.Order = readytoshiporders;
            model.Inventory = inventories;
            model.TodoList = todolist;
            model.Reminders = reminders;
            model.Fastmoving = fast_moving_products;
            model.Slowmoving = slow_moving_products;

            model.InventoryIn = InventoryInServices.Instance.GetInventoryIns().Select(x => x._Quantity).Sum();
           
            model.InventoryOut = OrderServices.Instance.GetOrders().Select(x => x._Quantity).Sum();

            model.CurrentStock = ProductServices.Instance.GetProduct().Select(x => x._Quantity).Sum();
            model.ActiveProducts = inventoryins.Count();
            model.Customer = customers.Count();
            //model.inventoryin = inventorybackupsservices.instance.getinventorybackup().where(x => x.justadded == false).select(x => x._quantityin).sum();
            //model.inventoryout = inventorybackupsservices.instance.getinventorybackup().where(x => x.justadded == false).select(x => x._quantityout).sum();
            //model.currentstock = inventorybackupsservices.instance.getinventorybackup().where(x => x.justadded == false).select(x => x._quantity).sum();
            //model.ActiveProducts = InventoryBackupsServices.Instance.GetInventoryBackup().Where(x => x.JustAdded == false).Count();
            model.LowStockAlert = ProductServices.Instance.GetProducts().Where(x => x._Quantity < x._LowStockAlert).Count();
            model.LowStockProducts = ProductServices.Instance.GetProducts().Where(x => x._Quantity < x._LowStockAlert).ToList();
            return View(model);
        }

        public List<Product> GetProductsWithinDateRange(int daysRange)
        {
            DateTime startDate = DateTime.Now.Date.AddDays(-daysRange);
            DateTime endDate = DateTime.Now.Date;
            var products = StockMovementServices.Instance.GetProductByRange(startDate, endDate);
            return products;
        }

        [HttpPost]
        public ActionResult Dashboard(string SearchTerm)
        {
            AdminViewModel model = new AdminViewModel();
            model.InventoriesForChart = InventoryBackupsServices.Instance.GetInventoryBackup();
            return View(model);
        }



        [HttpGet]
        public ActionResult StockHistory()
        {
            InventoryListingViewModel model = new InventoryListingViewModel();
            model.inventories = InventoryBackupsServices.Instance.GetInventoryBackup();
            return View(model);
        }

    }
}