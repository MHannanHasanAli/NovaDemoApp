using DocumentFormat.OpenXml.Spreadsheet;
using ImperialNova.Entities;
using ImperialNova.Services;
using ImperialNova.ViewModels;
using Newtonsoft.Json;
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
        public static decimal savedAmount = 0;
        public static int savedQuantity = 0;

        [HttpGet]
        public ActionResult Action(int ID = 0)
        {

            InventoryInActionViewModel model = new InventoryInActionViewModel();
            if (ID != 0)
            {
                var InventoryIn = InventoryInServices.Instance.GetInventoryInById(ID);
                model._Id = InventoryIn._Id;
                model._ShippingCompany = InventoryIn._ShippingCompany;
                model._Tracking = InventoryIn._Tracking;
                model._Status = InventoryIn._Status;
                model._Quantity = InventoryIn._Quantity.ToString();
                model._Amount = InventoryIn._Amount.ToString();
                model._Date = InventoryIn._Date;
                savedQuantity = InventoryIn._Quantity;
                savedAmount = InventoryIn._Amount;
            }

            model.suppliers = SupplierServices.Instance.GetSuppliers();
            model.locations = LocationsServices.Instance.GetLocations();
            model.products = InventoryInProductServices.Instance.GetInventoryInProducts();
            return View("Action", model);
        }
        public static int QuantityUpdate;

        public ActionResult ActionProducts(string products)
        {
            QuantityUpdate = 0;
            var InventoryInid = InventoryInServices.Instance.GetLastEntryId();
            var ListOfInventory = JsonConvert.DeserializeObject<List<ProductModel>>(products);
            var Invproduct = new InventoryInProduct();
            foreach (var item in ListOfInventory)
            {

                var product = ProductServices.Instance.GetProductById(int.Parse(item._ProductId));
                product._Quantity = product._Quantity + int.Parse(item._Quantity);
                QuantityUpdate = QuantityUpdate + int.Parse(item._Quantity);
                ProductServices.Instance.UpdateProduct(product);

                Invproduct._Qty = product._Quantity;
                Invproduct._SKU = product._SKU;
                Invproduct._Title = product._Name;
                Invproduct._Photo = product._Photo;
                Invproduct._Price = product._Cost;
                Invproduct._ExpiryDate =DateTime.Parse(item._ExpiryDate);
                Invproduct._Amount = decimal.Parse(item._Amount);
                Invproduct._InventoryInId = InventoryInid;

                InventoryInProductServices.Instance.CreateInventoryInProducts(Invproduct);

            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
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
                InventoryIn._Status = model._Status;              
                InventoryIn._Quantity = savedQuantity;
                InventoryIn._Amount = savedAmount;
                InventoryIn._Date = model._Date;
                InventoryIn._Supplier = model._Supplier;
                InventoryInServices.Instance.UpdateInventoryIn(InventoryIn);

            }
            else
            {
                var InventoryIn = new Entities.InventoryIn();
                InventoryIn._ShippingCompany = model._ShippingCompany;
                InventoryIn._Tracking = model._Tracking;
                InventoryIn._Status = "Pending Order";            
                InventoryIn._Quantity = int.Parse(model._Quantity);
                InventoryIn._Amount = decimal.Parse(model._Amount);              
                InventoryIn._Date = model._Date;
                InventoryIn._Supplier = model._Supplier;
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