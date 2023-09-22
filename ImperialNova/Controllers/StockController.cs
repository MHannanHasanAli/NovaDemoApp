using DocumentFormat.OpenXml.Bibliography;
using ImperialNova.Database.Migrations;
using ImperialNova.Entities;
using ImperialNova.Services;
using ImperialNova.ViewModels;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace ImperialNova.Controllers
{
    [Authorize]
    public class StockController : Controller
    {
        // GET: Stock
        public ActionResult Index()
        {
            ProductListingViewModel model = new ProductListingViewModel();
         
            var products= ProductServices.Instance.GetProduct();
            var ProductList = new List<ProductsModel>();
            foreach (var item in products)
            {
                var warehouse = LocationsServices.Instance.GetLocationsById(item._WarehouseId);
                var category = CategoryServices.Instance.GetCategoryById(item._CategoryId);
                ProductList.Add(new ProductsModel { Product = item, Category = category, Warehouse = warehouse });
            }
            model.Products = ProductList;
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Action(StockMovementActionViewModel model)
        {

                var StockMovement = StockMovementServices.Instance.GetStockMovementById(model._Id);
                StockMovement._Id = model._Id;
                StockMovement._Fdays = model._Fdays;
                StockMovement._Ffrom = model._Ffrom;
                StockMovement._Fto = model._Fto;
                StockMovement._Sdays = model._Sdays;
                StockMovement._Sfrom = model._Sfrom;
                StockMovement._Sto = model._Sto;
               
                StockMovementServices.Instance.UpdateStockMovement(StockMovement);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        List<Product> fast_moving_products = new List<Product>();
        List<Product> slow_moving_products = new List<Product>();

        [HttpGet]
        public ActionResult Action(int ID = 0)
        {

            StockMovementActionViewModel model = new StockMovementActionViewModel();
            if (ID != 0)
            {
                var stock = StockMovementServices.Instance.GetStockMovementById(ID);
                model._Id = stock._Id;
                model._Fdays = stock._Fdays;
                model._Ffrom = stock._Ffrom;
                model._Fto = stock._Fto;
                model._Sdays = stock._Sdays;
                model._Sfrom = stock._Sfrom;
                model._Sto = stock._Sto;
              
            }
            //var fast_products = GetProductsWithinDateRange(model._Fdays);
            //var slow_products = GetProductsWithinDateRange(model._Sdays);
            //var products = OrderProductServices.Instance.GetOrderProducts();
            
            //foreach (var fastproduct in fast_products)
            //{
            //    int totalnumber = products.Count();
            //    int fastcount = 0;
            //    foreach (var product in products)
            //    {
            //       if(fastproduct._Id == product._ProductId)
            //        {
            //            fastcount++;
            //        }
            //    }
            //    if (((fastcount / totalnumber) * 100) >= model._Ffrom && ((fastcount / totalnumber) * 100) <= model._Fto)
            //    {
            //        fast_moving_products.Add(fastproduct);
            //    }
            //}
            //foreach (var slowproduct in slow_products)
            //{
            //    int totalnumber = products.Count();
            //    int slowcount = 0;
            //    foreach (var product in products)
            //    {
            //        if (slowproduct._Id == product._ProductId)
            //        {
            //            slowcount++;
            //        }
            //    }
            //    if (((slowcount / totalnumber) * 100) >= model._Sfrom && ((slowcount / totalnumber) * 100) <= model._Sto)
            //    {
            //        slow_moving_products.Add(slowproduct);
            //    }
            //}

            return View("Action", model);
        }
        public List<Product> GetProductsWithinDateRange(int daysRange)
        {           
            DateTime startDate = DateTime.Now.Date.AddDays(-daysRange);          
            DateTime endDate = DateTime.Now.Date;      
            var products = StockMovementServices.Instance.GetProductByRange(startDate, endDate);
            return products;
        }

    }
    }