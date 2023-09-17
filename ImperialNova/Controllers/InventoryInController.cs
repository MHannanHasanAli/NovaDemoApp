using DocumentFormat.OpenXml.Spreadsheet;
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
    public class InventoryInController : Controller
    {
        public ActionResult Index()
        {
            InventoryInListingViewModel model = new InventoryInListingViewModel();
            model.inventoryins = InventoryInServices.Instance.GetInventoryIns();
           
            return View("Index", model);
        }

        public ActionResult PendingOrder()
        {
            InventoryInListingViewModel model = new InventoryInListingViewModel();
            model.inventoryins = InventoryInServices.Instance.GetInventoryIns();

            return View("PendingOrder", model);
        }
        public ActionResult CompletedOrder()
        {
            InventoryInListingViewModel model = new InventoryInListingViewModel();
            model.inventoryins = InventoryInServices.Instance.GetInventoryIns();

            return View("PendingOrder", model);
        }
        [HttpGet]
        public ActionResult Action(int ID = 0)
        {

            InventoryInActionViewModel model = new InventoryInActionViewModel();
            if (ID != 0)
            {
                var InventoryIn = InventoryInServices.Instance.GetInventoryInById(ID);
                model._Id              = InventoryIn._Id;
                model._ShippingCompany = InventoryIn._ShippingCompany;
                model._Tracking        = InventoryIn._Tracking;
                model._SupplierId        = InventoryIn._SupplierId;
                model._Status          = InventoryIn._Status;
                model._ProductId       = InventoryIn._ProductId;
                model._SKU             = InventoryIn._SKU;
                model._Title           = InventoryIn._Title;
                model._Location        = InventoryIn._Location;
                model._Quantity        = InventoryIn._Quantity;
                model._ExpiryDate      = InventoryIn._ExpiryDate;
                model._Amount          = InventoryIn._Amount;
                model._Price           = InventoryIn._Price;
                model._Photo           = InventoryIn._Photo;
                model._Date = InventoryIn._Date;


            }

            model.suppliers = SupplierServices.Instance.GetSuppliers();
            model.locations = LocationsServices.Instance.GetLocations();

            return View("Action", model);
        }

        public ActionResult Autocomplete(string term)
        {
            var products = ProductServices.Instance.GetProducts();
            var matchingProducts = products
                .Where(c => c._Name.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
                .Select(c => c._Name)
                .ToList();

            return Json(matchingProducts);
        }

        [HttpPost]
        public ActionResult Action(InventoryInActionViewModel model)
        {

            if (model._Id != 0)
            {
                var InventoryIn = InventoryInServices.Instance.GetInventoryInById(model._Id);
                InventoryIn._Id = model._Id;
                InventoryIn._ShippingCompany = model._ShippingCompany;
                InventoryIn._Tracking = model._Tracking;
                InventoryIn._SupplierId = model._SupplierId;
                InventoryIn._Status = model._Status;
                InventoryIn._ProductId = model._ProductId;
                InventoryIn._SKU = model._SKU;
                InventoryIn._Title = model._Title;
                InventoryIn._Location = model._Location;
                InventoryIn._Quantity = model._Quantity;
                InventoryIn._ExpiryDate = model._ExpiryDate;
                InventoryIn._Amount = model._Amount;
                InventoryIn._Price = model._Price;
                InventoryIn._Photo = model._Photo;
                InventoryIn._Date = model._Date;
                InventoryInServices.Instance.UpdateInventoryIn(InventoryIn);

            }
            else
            {
                var InventoryIn = new Entities.InventoryIn();
                InventoryIn._ShippingCompany = model._ShippingCompany;
                InventoryIn._Tracking = model._Tracking;
                InventoryIn._SupplierId = model._SupplierId;
                InventoryIn._Status = model._Status;
                InventoryIn._ProductId = model._ProductId;
                InventoryIn._SKU = model._SKU;
                InventoryIn._Title = model._Title;
                InventoryIn._Location = model._Location;
                InventoryIn._Quantity = model._Quantity;
                InventoryIn._ExpiryDate = model._ExpiryDate;
                InventoryIn._Amount = model._Amount;
                InventoryIn._Price = model._Price;
                InventoryIn._Photo = model._Photo;
                InventoryIn._Date = model._Date;

                InventoryInServices.Instance.CreateInventoryIn(InventoryIn);
            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            InventoryInActionViewModel model = new InventoryInActionViewModel();
            var InventoryIn = InventoryInServices.Instance.GetInventoryInById(ID);
            model._Id = InventoryIn._Id;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(InventoryInActionViewModel model)
        {
            if (model._Id != 0)
            {
                var InventoryIn = InventoryInServices.Instance.GetInventoryInById(model._Id);

                InventoryInServices.Instance.DeleteInventoryIn(InventoryIn._Id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetItems(int store)
        {
            var products = ProductServices.Instance.GetProducts();
            List<Product> SortedProduct = new List<Product>();
            foreach (var product in products)
            {
                if(product._WarehouseId == store)
                {
                    SortedProduct.Add(product);
                }
            }
            return Json(SortedProduct, JsonRequestBehavior.AllowGet);
        }
    }
}