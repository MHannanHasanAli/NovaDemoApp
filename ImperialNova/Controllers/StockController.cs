using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.Excel;
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
            Session["ACTIVER"] = "Stock Index";

            ProductListingViewModel model = new ProductListingViewModel();
         
            var products= ProductServices.Instance.GetProducts();
            var ProductList = new List<ProductsModel>();
            foreach (var item in products)
            {
                var warehouse = LocationsServices.Instance.GetLocationsById(item._WarehouseId);
                var category = CategoryServices.Instance.GetCategoryById(item._CategoryId);
                model.Purchase = model.Purchase + item._Cost;
                model.Sell =model.Sell + item._RetailPrice;
                model.Quantity = model.Quantity + item._Quantity;
                item.ModifiedCost = item._RetailPrice * item._Cost;
                model.TotalModified = model.TotalModified + item.ModifiedCost;
                ProductList.Add(new ProductsModel { Product = item, Category = category, Warehouse = warehouse });
            }
            model.Products = ProductList;
            model.warehouses = LocationsServices.Instance.GetLocations();

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Action(StockMovementActionViewModel model)
        {
            
            if (model._Id != 0)
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
            }
            else
            {
                var StockMovement = new StockMovement();
                StockMovement._Fdays = model._Fdays;
                StockMovement._Ffrom = model._Ffrom;
                StockMovement._Fto = model._Fto;
                StockMovement._Sdays = model._Sdays;
                StockMovement._Sfrom = model._Sfrom;
                StockMovement._Sto = model._Sto;

                StockMovementServices.Instance.CreateStockMovement(StockMovement);

            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            if (ID != 0)
            {
                Session["ACTIVER"] = "Stock Edit";

            }
            else
            {
                Session["ACTIVER"] = "Stock Action";

            }
            StockMovementActionViewModel model = new StockMovementActionViewModel();
            var stock = StockMovementServices.Instance.GetStockMovement();
            if(stock == null) {
                return View("Action", model);
            }
            else
            {
                             
                model._Id = stock._Id;
                model._Fdays = stock._Fdays;
                model._Ffrom = stock._Ffrom;
                model._Fto = stock._Fto;
                model._Sdays = stock._Sdays;
                model._Sfrom = stock._Sfrom;
                model._Sto = stock._Sto;
              
            }
           

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