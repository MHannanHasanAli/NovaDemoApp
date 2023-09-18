using ImperialNova.Entities;
using ImperialNova.Services;
using ImperialNova.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImperialNova.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Index()
        {
            OrderListingViewModel model = new OrderListingViewModel();
            model.orders = OrderServices.Instance.GetOrders();
            return View("Index", model);
        }
        public ActionResult ReadyToShipOrder()
        {
            OrderListingViewModel model = new OrderListingViewModel();
            model.orders = OrderServices.Instance.GetReadyToShipOrders();
            return View("ReadyToShipOrder", model);
        }
        public ActionResult Shipped()
        {
            OrderListingViewModel model = new OrderListingViewModel();
            model.orders = OrderServices.Instance.GetShippedOrders();
            return View("Shipped", model);
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
                model._CustomerId = Order._CustomerId;
                model._Priority   = Order._Priority;
                model._ProductId  = Order._ProductId;
                model._Photo      = Order._Photo;
                model._SKU        = Order._SKU;
                model._Title      = Order._Title;
                model._Qty        = Order._Qty;
                model._Price      = Order._Price;
                model._Amount     = Order._Amount;
                model._IsPacked   = Order._IsPacked;

            }
            model.customers = CustomerServices.Instance.GetCustomers();
            model.locations = LocationsServices.Instance.GetLocations();
            return View("Action", model);
        }


        [HttpPost]
        public ActionResult Action(OrderActionViewModel model)
        {

            if (model._Id != 0)
            {
                var Order = OrderServices.Instance.GetOrderById(model._Id);
                Order._Id = model._Id;
                Order._Date = model._Date;
                Order._ShipByDate = model._ShipByDate;
                Order._Record = model._Record;
                Order._Tracking = model._Tracking;
                Order._Status = model._Status;
                Order._CustomerId = model._CustomerId;
                Order._Priority = model._Priority;
                Order._ProductId = model._ProductId;
                Order._Photo = model._Photo;
                Order._SKU = model._SKU;
                Order._Title = model._Title;
                Order._Qty = model._Qty;
                Order._Price = model._Price;
                Order._Amount = model._Amount;
                Order._IsPacked = model._IsPacked;

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
                Order._CustomerId = model._CustomerId;
                Order._Priority = model._Priority;
                Order._ProductId = model._ProductId;
                Order._Photo = model._Photo;
                Order._SKU = model._SKU;
                Order._Title = model._Title;
                Order._Qty = model._Qty;
                Order._Price = model._Price;
                Order._Amount = model._Amount;
                Order._IsPacked = model._IsPacked;

                OrderServices.Instance.CreateOrder(Order);
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
                var Order = OrderServices.Instance.GetOrderById(model._Id);

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