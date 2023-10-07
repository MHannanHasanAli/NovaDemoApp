using DocumentFormat.OpenXml.Spreadsheet;
using ImperialNova.Entities;
using ImperialNova.Services;
using ImperialNova.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImperialNova.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private AMSignInManager _signInManager;
        private AMUserManager _userManager;
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

        private AMRolesManager _rolesManager;
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
        public OrderController()
        {
        }
        public OrderController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ActionResult Index()
        {
            Session["ACTIVER"] = "Order Index";

            OrderListingViewModel model = new OrderListingViewModel();
            model.orders = OrderServices.Instance.GetOrders();
            foreach (var item in model.orders)
            {
                model.Amount = model.Amount + item._Amount;
                model.Quantity = model.Quantity + item._Quantity;
            }
            return View("Index", model);
        }
        public ActionResult MassDelete(List<int> ids)
        {
            foreach (var item in ids)
            {
                OrderServices.Instance.DeleteOrder(item);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult ReadyToShipOrder()
        {
            Session["ACTIVER"] = "Order Ready";
            OrderListingViewModel model = new OrderListingViewModel();
            model.orders = OrderServices.Instance.GetReadyToShipOrders();
            return View("ReadyToShipOrder", model);
        }
        public ActionResult Shipped()
        {
            Session["ACTIVER"] = "Order Ship";
            OrderListingViewModel model = new OrderListingViewModel();
            model.orders = OrderServices.Instance.GetShippedOrders();
            return View("Shipped", model);
        }
        public ActionResult Transfer()
        {
            Session["ACTIVER"] = "Transfer Goods";
            TransferModel model = new TransferModel();
            model.warehouses = LocationsServices.Instance.GetLocations();
            return View("Transfer", model);
        }
        [HttpGet]
        public ActionResult GetProductsInJson()
        {
            var product = ProductServices.Instance.GetProduct();
            return Json(product, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            if (ID != 0)
            {
                Session["ACTIVER"] = "Order Edit";

            }
            else
            {
                Session["ACTIVER"] = "Order Action";

            }
            OrderActionViewModel model = new OrderActionViewModel();
            if (ID != 0)
            {
                var Order = OrderServices.Instance.GetOrderById(ID);
                model._Id         = Order._Id;
                model._Date       = Order._Date;
                model._ShipByDate = Order._ShipByDate;
                model._Record     = Order._Record;
                model._Tracking   = Order._Tracking;
                model._Status     = Order._Status;
                model._Customer = Order._Customer;
                model._Priority   = Order._Priority;
                model._Quantity = Order._Quantity.ToString();
                model._Amount = Order._Amount.ToString();
                model._IsPacked = Order._IsPacked;
                savedQuantity = Order._Quantity;
                savedAmount = Order._Amount;

            }
            model.customers = CustomerServices.Instance.GetCustomers();
            model.locations = LocationsServices.Instance.GetLocations();
            model.products = OrderProductServices.Instance.GetOrderProductsByInventoryInId(model._Id);
            return View("Action", model);
        }
        public static int QuantityUpdate;

        public ActionResult ActionProducts(string products)
         {

            QuantityUpdate = 0;
            var Orderid = OrderServices.Instance.GetLastOrderId();
            var ListOfInventory = JsonConvert.DeserializeObject<List<ProductModel>>(products);
            var Orderproduct = new OrderProduct();
            foreach (var item in ListOfInventory)
            {
                if (item._Quantity == null)
                {
                    break;
                }
                var product = ProductServices.Instance.GetProductById(int.Parse(item._ProductId));
                product._Quantity = product._Quantity - int.Parse(item._Quantity);
                product._QuantityOut = product._QuantityOut + int.Parse(item._Quantity);
                product._ExportDate = DateTime.Now;
                QuantityUpdate = QuantityUpdate + int.Parse(item._Quantity);
                ProductServices.Instance.UpdateProduct(product);
                
                Orderproduct._ProductId = product._Id;
                Orderproduct._Qty = int.Parse(item._Quantity);
                Orderproduct._SKU = product._SKU;
                Orderproduct._Title = product._Name;
                Orderproduct._Photo = product._Photo;
                Orderproduct._Price = product._RetailPrice;             
                Orderproduct._Amount = decimal.Parse(item._Amount);
                Orderproduct._OrderId = Orderid;
                Orderproduct._Cost = product._Cost;
                Orderproduct._Size = product._Size;
                Orderproduct._Color = product._Color;
                var warehousedata = LocationsServices.Instance.GetLocationsById(product._WarehouseId);
                Orderproduct._location = warehousedata._LocationName;
               OrderProductServices.Instance.CreateOrderProducts(Orderproduct);

            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetProductJson(int ID)
        {
            var product = ProductServices.Instance.GetProductById(ID);
            return Json(product, JsonRequestBehavior.AllowGet);

        }
        public static decimal savedAmount = 0;
        public static int savedQuantity = 0;
        [HttpPost]
        public ActionResult Action(OrderActionViewModel model)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (model._Id != 0)
            {
                var Order = OrderServices.Instance.GetOrderById(model._Id);
                Order._Id = model._Id;
                Order._Date = model._Date;
                Order._ShipByDate = model._ShipByDate;
                Order._Record = model._Record;
                Order._Tracking = model._Tracking;
                Order._Status = model._Status;
                Order._Customer = model._Customer;
                Order._Priority = model._Priority;
                Order._IsPacked = model._IsPacked;
                Order._Amount = savedAmount;
                Order._Quantity = savedQuantity;
                //Order._ProductId = model._ProductId;
                //Order._Photo = model._Photo;
                //Order._SKU = model._SKU;
                //Order._Title = model._Title;
                //Order._Qty = model._Qty;
                //Order._Price = model._Price;
                //Order._Amount = model._Amount;
                //Order._IsPacked = model._IsPacked;

                OrderServices.Instance.UpdateOrder(Order);

            }
            else
            {
                var Order = new Entities.Order();
                Order._Date = model._Date;
                Order._ShipByDate = model._ShipByDate;
                Order._Record = model._Record;
                Order._Tracking = model._Tracking;
                Order._Status = "Ready To Ship";
                Order._Customer = model._Customer;
                Order._Priority = model._Priority;
                Order._IsPacked = model._IsPacked;
                if(model._Amount != null)
                {
                    Order._Amount = decimal.Parse(model._Amount);
                }
                if(model._Quantity != null)
                {
                    Order._Quantity = int.Parse(model._Quantity);
                }


                OrderServices.Instance.CreateOrder(Order);

                var notification = new Entities.Notification();
                notification._Description = "New Order has been Added!";
                notification._UserName = user.Name;
                NotificationServices.Instance.CreateNotification(notification);
            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            OrderActionViewModel model = new OrderActionViewModel();
            var Order = OrderServices.Instance.GetOrderById(ID);
            model._Id = Order._Id;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(OrderActionViewModel model)
        {
            if (model._Id != 0)
            {
                var products = OrderProductServices.Instance.GetOrderProducts().Where(x=>x._OrderId == model._Id);
                foreach (var item in products)
                {
                    OrderProductServices.Instance.DeleteOrderProducts(item._Id);
                }
                var Order = OrderServices.Instance.GetOrderById(model._Id);
                var backup = new Backup();
                backup.DeletionDate = DateTime.Now;
                backup.ComponenetId = Order._Id;
                backup.Aspect = Order._Amount.ToString();
                backup.Type = "Order";
                BackupServices.Instance.CreateBackup(backup);
                OrderServices.Instance.DeleteOrder(Order._Id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetItems(int store)
        {
            var products = ProductServices.Instance.GetProducts();
            List<Product> SortedProduct = new List<Product>();
            foreach (var product in products)
            {
                if (product._WarehouseId == store)
                {
                    SortedProduct.Add(product);
                }
            }
            return Json(SortedProduct, JsonRequestBehavior.AllowGet);
        }
    }
}