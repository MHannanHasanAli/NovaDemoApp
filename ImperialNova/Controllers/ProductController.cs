using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.ExtendedProperties;
using ImperialNova.Database.Migrations;
using ImperialNova.Entities;
using ImperialNova.Services;
using ImperialNova.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Build.Framework.XamlTypes;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Order = ImperialNova.Entities.Order;

namespace ImperialNova.Controllers
{

    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
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
        public ProductController()
        {
        }
        public ProductController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            Session["ACTIVER"] = "Product Index";

            List<Product> products;
            ProductListingViewModel model = new ProductListingViewModel();
            if(startDate ==  null && endDate == null)
            {
                products = ProductServices.Instance.GetProduct();
            }
            else
            {
                products = ProductServices.Instance.GetProductByFilter(startDate, endDate);

            }
            var ProductList = new List<ProductsModel>();

            foreach (var item in products)
            {
                var warehouse = LocationsServices.Instance.GetLocationsById(item._WarehouseId);
                var category = CategoryServices.Instance.GetCategoryById(item._CategoryId);
                model.Quantity = model.Quantity + item._Quantity;
                model.Purchase = model.Purchase + item._RetailPrice;
                model.Sell = model.Sell + item._Cost;
                ProductList.Add(new ProductsModel { Product = item, Category = category, Warehouse = warehouse });
            }

            model.Products = ProductList;
            //model.warehouses = LocationsServices.Instance.GetLocations();
            return View("Index", model);
        }
        public ActionResult MassDelete(List<int> ids)
        {
            foreach (var item in ids)
            {
                ProductServices.Instance.DeleteProduct(item);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult GetFilteredData(DateTime? startDate, DateTime? endDate)
        {
      

            var filteredProducts = ProductServices.Instance.GetProductByFilter(startDate, endDate); // Replace with your actual filtering logic

            foreach (var item in filteredProducts)
            {
                var warehouse = LocationsServices.Instance.GetLocationsById(item._WarehouseId);
                var category = CategoryServices.Instance.GetCategoryById(item._CategoryId);
                item._Warehouse = warehouse._LocationName;
            }
            // Return the filtered data as JSON
            return Json(filteredProducts, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Import()
        {
            Session["ACTIVER"] = "Product Import";
            return View();
        }
        public ActionResult ExportToExcel()
        {


            var products = ProductServices.Instance.GetProduct();
            var ProductList = new List<ProductsModel>();
            foreach (var item in products)
            {
                var category = CategoryServices.Instance.GetCategoryById(item._CategoryId);
                ProductList.Add(new ProductsModel { Product = item, Category = category });
            }

            // Create a DataTable and populate it with the site data
            System.Data.DataTable tableData = new System.Data.DataTable();
            tableData.Columns.Add("ID", typeof(int)); // Replace "Column1" with the actual column name
            tableData.Columns.Add("Name", typeof(string)); // Replace "Column2" with the actual column name
            tableData.Columns.Add("Category", typeof(string)); // Replace "Column2" with the actual column name
            tableData.Columns.Add("Size", typeof(string)); // Replace "Column2" with the actual column nam
            tableData.Columns.Add("Color", typeof(string)); // Replace "Column2" with the actual column name
            tableData.Columns.Add("Cost", typeof(decimal)); // Replace "Column2" with the actual column name
            tableData.Columns.Add("SKU", typeof(string)); // Replace "Column1" with the actual column name
            tableData.Columns.Add("Weight", typeof(string)); // Replace "Column2" with the actual column name
            tableData.Columns.Add("Variation", typeof(int)); // Replace "Column2" with the actual column name
            tableData.Columns.Add("Thickness", typeof(string)); // Replace "Column2" with the actual column nam
            tableData.Columns.Add("Retail Price", typeof(string)); // Replace "Column2" with the actual column name
            tableData.Columns.Add("Quantity", typeof(int)); // Replace "Column2" with the actual column name
            tableData.Columns.Add("Notes", typeof(string)); // Replace "Column2" with the actual column nam
            tableData.Columns.Add("Date", typeof(DateTime)); // Replace "Column2" with the actual column name
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            foreach (var product in ProductList)
            {
                DataRow row = tableData.NewRow();

                row["ID"] = product.Product._Id;
                row["Name"] = product.Product._Name;
                row["Category"] = product.Category._CName;
                row["Size"] = product.Product._Size;
                row["Color"] = product.Product._Color;
                row["Cost"] = product.Product._Cost;
                row["SKU"] = product.Product._SKU;
                row["Weight"] = product.Product._Weight;
                row["Variation"] = product.Product._Variations;
                row["Thickness"] = product.Product._Thickness;
                row["Retail Price"] = product.Product._RetailPrice;
                row["Quantity"] = product.Product._Quantity;
                row["Notes"] = product.Product._Notes;
                row["Date"] = product.Product._ExportDate;

                tableData.Rows.Add(row);
            }

            // Create the Excel package
            using (ExcelPackage package = new ExcelPackage())
            {
                // Create a new worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Products");

                // Set the column names
                for (int i = 0; i < tableData.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = tableData.Columns[i].ColumnName;
                }

                // Set the row data
                for (int row = 0; row < tableData.Rows.Count; row++)
                {
                    for (int col = 0; col < tableData.Columns.Count; col++)
                    {
                        worksheet.Cells[row + 2, col + 1].Value = tableData.Rows[row][col];
                    }
                }

                // Auto-fit columns for better readability
                worksheet.Cells.AutoFitColumns();

                // Convert the Excel package to a byte array
                byte[] excelBytes = package.GetAsByteArray();

                // Return the Excel file as a downloadable file
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Buildings.xlsx");
            }
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase excelfile)
        {
            if (excelfile == null || excelfile.ContentLength == 0)
            {
                ViewBag.Error = "Please Select Excel File";
                return View();
            }
            else
            {
                bool isPresent = false;
                var ProductAddedList = new List<Product>();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial

                if (excelfile != null && excelfile.ContentLength > 0)
                {
                    using (var package = new ExcelPackage(excelfile.InputStream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++) // Assuming the first row is header
                        {
                            var product = new Product();
                            string name = worksheet.Cells[row, 1].Value.ToString();
                            product._Name = worksheet.Cells[row, 1].Value.ToString(); //ProductName
                            var CategoryName = worksheet.Cells[row, 2].Value.ToString();//Category Name
                            var Category = CategoryServices.Instance.GetCategorys(CategoryName).FirstOrDefault();
                            product._CategoryId = Category._Id;
                            product._Size = worksheet.Cells[row, 3].Value.ToString(); //Size
                            product._Color = worksheet.Cells[row, 4].Value.ToString(); //Color
                            product._Cost = decimal.Parse(worksheet.Cells[row, 5].Value.ToString()); //Cost
                            product._SKU = worksheet.Cells[row, 6].Value.ToString(); //SKU
                            product._Weight = worksheet.Cells[row, 7].Value.ToString(); //Weight
                            product._Thickness = worksheet.Cells[row, 8].Value.ToString(); //Thickness
                            product._Variations = int.Parse(worksheet.Cells[row, 9].Value.ToString()); //Variations
                            product._RetailPrice = decimal.Parse(worksheet.Cells[row, 10].Value.ToString()); // Retail Price
                            product._QuantityIn = int.Parse(worksheet.Cells[row, 11].Value.ToString()); // Quantity In
                            product._QuantityOut = int.Parse(worksheet.Cells[row, 12].Value.ToString()); // Quantity Out
                            product._Notes = worksheet.Cells[row, 13].Value.ToString(); //Notes
                            product._ExportDate = DateTime.Now;
                            var List = ProductServices.Instance.GetProduct();


                            if (List.Count != 0)
                            {
                                foreach (var item in List)
                                {
                                    if (item._Name == product._Name && item._CategoryId == product._CategoryId)
                                    {
                                        isPresent = true;
                                        break;
                                    }
                                    else
                                    {
                                        isPresent = false;
                                    }




                                }
                                if (isPresent == false)
                                {
                                    ProductAddedList.Add(product);
                                    ProductServices.Instance.CreateProduct(product);
                                }
                            }
                            else
                            {
                                ProductAddedList.Add(product);

                                ProductServices.Instance.CreateProduct(product);
                                var Stores = LocationsServices.Instance.GetLocations();
                                foreach (var item in Stores)
                                {
                                    var inventorybkp = new InventoryBackups();
                                    inventorybkp._ProductId = product._Id;
                                    inventorybkp._Name = product._Name;
                                    inventorybkp._Size = product._Size;
                                    inventorybkp._Color = product._Color;
                                    inventorybkp._Store = item._LocationName;
                                    inventorybkp._SKU = product._SKU;
                                    inventorybkp._Weight = product._Weight;
                                    inventorybkp._Thickness = product._Thickness;
                                    inventorybkp._Variations = product._Variations;
                                    inventorybkp._Cost = product._Cost;
                                    inventorybkp._RetailPrice = product._RetailPrice;

                                    if (product._OpeningStock == 0)
                                    {
                                        inventorybkp._QuantityIn = 0;

                                    }
                                    else
                                    {
                                        inventorybkp._QuantityIn = product._OpeningStock;
                                    }
                                    inventorybkp._QuantityOut = 0;
                                    inventorybkp._Notes = product._Notes;
                                    inventorybkp.JustAdded = true;
                                    InventoryBackupsServices.Instance.CreateInventoryBackup(inventorybkp);
                                }

                            }
                        }

                    }
                    ViewBag.Products = ProductAddedList;
                    return View();

                }



                else
                {
                    ViewBag.Error = "Incorrect File";

                    return View();
                }
            }

            //var Prcoess = Process.GetProcessesByName("EXCEL.EXE").FirstOrDefault();
            //Prcoess.Kill();

        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            if (ID != 0)
            {
                Session["ACTIVER"] = "Product Edit";

            }
            else
            {
                Session["ACTIVER"] = "Product Action";

            }
            ProductActionViewModel model = new ProductActionViewModel();
            model.Categories = CategoryServices.Instance.GetCategorys();
            model.Warehouses = LocationsServices.Instance.GetLocations();
            if (ID != 0)
            {
                var Product = ProductServices.Instance.GetProductById(ID);
                model._Id = Product._Id;
                model._Name = Product._Name;
                model._Size = Product._Size;
                model._Color = Product._Color;
                model._Cost = Product._Cost;
                model._SKU = Product._SKU;
                model._Weight = Product._Weight;
                model._Thickness = Product._Thickness;
                model._Variations = Product._Variations;
                model._RetailPrice = Product._RetailPrice;
                model._QuantityIn = Product._QuantityIn;
                model._QuantityOut = Product._QuantityOut;
                model._Notes = Product._Notes;
                model._ExportDate = Product._ExportDate;
                model._CategoryId = Product._CategoryId;
                model._WarehouseId = Product._WarehouseId;
                model._LowStockAlert = Product._LowStockAlert;
                model._Photo = Product._Photo;
                model._OpeningStock = Product._OpeningStock;
            }
            return View("Action", model);
        }


        [HttpPost]
        public ActionResult Action(ProductActionViewModel model)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (model._Id != 0)
            {
                var Product = ProductServices.Instance.GetProductById(model._Id);
                Product._Id = model._Id;
                Product._Name = model._Name;
                Product._Size = model._Size;
                Product._Color = model._Color;
                Product._Cost = model._Cost;
                Product._SKU = model._SKU;
                Product._Weight = model._Weight;
                Product._Thickness = model._Thickness;
                Product._Variations = model._Variations;
                Product._RetailPrice = model._RetailPrice;
                Product._QuantityIn = model._QuantityIn;
                Product._Quantity = model._OpeningStock;
                Product._QuantityOut = model._QuantityOut;
                Product._Notes = model._Notes;
                Product._ExportDate = model._ExportDate;
                Product._CategoryId = model._CategoryId;
                Product._WarehouseId = model._WarehouseId;
                var data = LocationsServices.Instance.GetLocationsById(model._WarehouseId);
                Product._Warehouse = data._LocationName;
                Product._LowStockAlert = model._LowStockAlert;
                Product._Photo = model._Photo;
                Product._OpeningStock = model._OpeningStock;
                ProductServices.Instance.UpdateProduct(Product);

            }
            else
            {
                var Product = new Product();
                Product._Name = model._Name;
                Product._Size = model._Size;
                Product._Color = model._Color;
                Product._Cost = model._Cost;
                Product._SKU = model._SKU;
                Product._Weight = model._Weight;
                Product._Thickness = model._Thickness;
                Product._Variations = model._Variations;
                Product._RetailPrice = model._RetailPrice;
                Product._QuantityIn = model._QuantityIn;
                Product._Quantity = model._OpeningStock;
                Product._QuantityOut = model._QuantityOut;
                Product._Notes = model._Notes;
                Product._ExportDate = model._ExportDate;
                Product._CategoryId = model._CategoryId;
                Product._WarehouseId = model._WarehouseId;
                Product._LowStockAlert = model._LowStockAlert;
                Product._Photo = model._Photo;
                Product._OpeningStock = model._OpeningStock;
                var data = LocationsServices.Instance.GetLocationsById(model._WarehouseId);
                Product._Warehouse = data._LocationName;
                ProductServices.Instance.CreateProduct(Product);

                var notification = new Entities.Notification();
                notification._Description = "New Product has been Added!";
                notification._UserName = user.Name;
                NotificationServices.Instance.CreateNotification(notification);

                var Stores = LocationsServices.Instance.GetLocations();
                foreach (var item in Stores)
                {
                    var inventorybkp = new InventoryBackups();
                    inventorybkp._ProductId = Product._Id;
                    inventorybkp._Name = model._Name;
                    inventorybkp._Size = model._Size;
                    inventorybkp._Color = model._Color;
                    inventorybkp._Store = item._LocationName;
                    inventorybkp._SKU = model._SKU;
                    inventorybkp._Weight = model._Weight;
                    inventorybkp._Thickness = model._Thickness;
                    inventorybkp._Variations = model._Variations;
                    inventorybkp._Cost = model._Cost;
                    inventorybkp._RetailPrice = model._RetailPrice;
                    
                    if(model._OpeningStock == 0)
                    {
                        inventorybkp._QuantityIn = 0;

                    }
                    else
                    {
                        inventorybkp._QuantityIn = model._OpeningStock;
                    }
                    inventorybkp._QuantityOut = 0;
                    inventorybkp._Notes = model._Notes;
                    inventorybkp.JustAdded = true;
                    InventoryBackupsServices.Instance.CreateInventoryBackup(inventorybkp);
                }
              
            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            ProductActionViewModel model = new ProductActionViewModel();
            var Product = ProductServices.Instance.GetProductById(ID);
            model._Id = Product._Id;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(ProductActionViewModel model)
        {
            if (model._Id != 0)
            {
                var Product = ProductServices.Instance.GetProductById(model._Id);
                ProductServices.Instance.DeleteProduct(Product._Id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult update(Product product)
        {
            ProductServices.Instance.UpdateProduct(product);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Preview(int ID)
        {
            ProductListingViewModel model = new ProductListingViewModel();

            var product = ProductServices.Instance.GetProductById(ID);
            var ProductList = new ProductPreviewModel();

                var warehouse = LocationsServices.Instance.GetLocationsById(product._WarehouseId);
                var category = CategoryServices.Instance.GetCategoryById(product._CategoryId);
            ProductList.Product = product;
            ProductList.Warehouse = warehouse;
            ProductList.Category = category;
            var orderlist = new List<Order>();
            ProductList.orderProducts = OrderProductServices.Instance.GetOrderProductsByProductId(ID);
            foreach (var item in ProductList.orderProducts)
            {
               var orderinfo= OrderServices.Instance.GetOrderById(item._OrderId);
                orderlist.Add(orderinfo);
            }
            model.Product = ProductList;
            model.Product.order = orderlist;
            return View("Preview", model);

            
        }
    }
}
