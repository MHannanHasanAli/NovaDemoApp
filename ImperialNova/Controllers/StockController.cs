using ImperialNova.Services;
using ImperialNova.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImperialNova.Controllers
{
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
    }
}