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
            foreach (var item in model.InventoriesForChart)
            {
                dataPoints.Add(new DataPoint { Month = item._ExportDate, QuantityIn = item._QuantityIn, QuantityOut = item._QuantityOut });
            }
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);


            model.InventoryIn = InventoryBackupsServices.Instance.GetInventoryBackup().Where(x => x.JustAdded == false).Select(x => x._QuantityIn).Sum();
            model.InventoryOut = InventoryBackupsServices.Instance.GetInventoryBackup().Where(x => x.JustAdded == false).Select(x => x._QuantityOut).Sum();
            model.CurrentStock = InventoryBackupsServices.Instance.GetInventoryBackup().Where(x => x.JustAdded == false).Select(x => x._Quantity).Sum();
            model.ActiveProducts = InventoryBackupsServices.Instance.GetInventoryBackup().Where(x => x.JustAdded == false).Count();
            model.LowStockAlert = ProductServices.Instance.GetProducts().Where(x => x._Quantity < 25).Count();
            model.LowStockProducts = ProductServices.Instance.GetProducts().Where(x => x._Quantity < 25).ToList();
            return View(model);
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