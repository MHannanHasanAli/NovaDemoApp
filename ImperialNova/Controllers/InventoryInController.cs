using DocumentFormat.OpenXml.Spreadsheet;
using ImperialNova.Entities;
using ImperialNova.Services;
using ImperialNova.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImperialNova.Controllers
{
    [Authorize]
    public class InventoryInController : Controller
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
        public InventoryInController()
        {
        }
        public InventoryInController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ActionResult Index()
        {
            Session["ACTIVER"] = "Inventory Index";

            InventoryInListingViewModel model = new InventoryInListingViewModel();
            model.inventoryins = InventoryInServices.Instance.GetInventoryIns();
            foreach (var item in model.inventoryins)
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
                InventoryInServices.Instance.DeleteInventoryIn(item);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult PendingOrder()
        {
            Session["ACTIVER"] = "Inventory Pending";

            InventoryInListingViewModel model = new InventoryInListingViewModel();
            model.inventoryins = InventoryInServices.Instance.GetPendingOrderInventoryIns();

            return View("PendingOrder", model);
        }
        public ActionResult CompletedOrder()
        {
            Session["ACTIVER"] = "Inventory Completed";

            InventoryInListingViewModel model = new InventoryInListingViewModel();
            model.inventoryins = InventoryInServices.Instance.GetCompletedOrderInventoryIns();

            return View("CompletedOrder", model);
        }
        public static decimal savedAmount = 0;
        public static int savedQuantity = 0;

        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            if (ID != 0)
            {
                Session["ACTIVER"] = "Inventory Edit";

            }
            else
            {
                Session["ACTIVER"] = "Inventory Action";

            }
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
                model._Supplier = InventoryIn._Supplier;
                savedQuantity = InventoryIn._Quantity;
                savedAmount = InventoryIn._Amount;
            }

            //model.suppliers = SupplierServices.Instance.GetSuppliers();
            model.locations = LocationsServices.Instance.GetLocations();
            model.products = InventoryInProductServices.Instance.GetInventoryInProductsByInventoryInId(model._Id);
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
                if (item._Quantity == null)
                {
                    break;
                }
                    var product = ProductServices.Instance.GetProductById(int.Parse(item._ProductId));
                product._Quantity = product._Quantity + int.Parse(item._Quantity);
                product._QuantityIn = product._QuantityIn + int.Parse(item._Quantity);
                product._ExportDate = DateTime.Now;
                QuantityUpdate = QuantityUpdate + int.Parse(item._Quantity);
                ProductServices.Instance.UpdateProduct(product);

                Invproduct._Qty = int.Parse(item._Quantity);
                Invproduct._SKU = product._SKU;
                Invproduct._Title = product._Name;
                Invproduct._Photo = product._Photo;
                Invproduct._Price = product._Cost;
                Invproduct._ExpiryDate =DateTime.Parse(item._ExpiryDate);
                Invproduct._Amount = decimal.Parse(item._Amount);
                Invproduct._InventoryInId = InventoryInid;
                Invproduct._Size = product._Size;
                Invproduct._Color = product._Color;
                var location = LocationsServices.Instance.GetLocationsById(product._WarehouseId);
                Invproduct._Warehouse = location._LocationName;
                
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
            var user = UserManager.FindById(User.Identity.GetUserId());
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
                if(model._Quantity != null)
                {
                    InventoryIn._Quantity = int.Parse(model._Quantity);

                }
                if(model._Amount != null)
                {
                    InventoryIn._Amount = decimal.Parse(model._Amount);

                }
                InventoryIn._Date = model._Date;
                InventoryIn._Supplier = model._Supplier;
                InventoryInServices.Instance.CreateInventoryIn(InventoryIn);

                var notification = new Entities.Notification();
                notification._Description = "New Inventory has been Added!";
                notification._UserName = user.Name;
                NotificationServices.Instance.CreateNotification(notification);
            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Import()
        {
            Session["ACTIVER"] = "Inventory Import";

            return View();
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
                var ProductAddedList = new List<InventoryInProduct>();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial

                if (excelfile != null && excelfile.ContentLength > 0)
                {
                    using (var package = new ExcelPackage(excelfile.InputStream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++) // Assuming the first row is header
                        {
                            var inventory = new InventoryInProduct();
                           
                            if(worksheet.Cells[row, 1].Value == null)
                            {
                                continue;
                            }

                           if(worksheet.Cells[row, 1].Value != null)
                            {
                                string name = worksheet.Cells[row, 1].Value.ToString();
                                inventory._Title = worksheet.Cells[row, 1].Value.ToString();
                            }
                            else
                            {
                                inventory._Title = "Not Specified";
                            }
                            if(worksheet.Cells[row, 2].Value != null)
                            {
                                var CategoryName = worksheet.Cells[row, 2].Value.ToString();//Category Name
                                var CategoryFilter = CategoryServices.Instance.GetCategorys(CategoryName).FirstOrDefault();
                                if (CategoryFilter != null)
                                {
                                    inventory._CategoryId = CategoryFilter._Id;
                                }
                                else
                                {
                                    var category = new Entities.Category();
                                    category._CName = CategoryName;

                                    CategoryServices.Instance.CreateCategory(category);
                                }
                            }
                            
                            if(worksheet.Cells[row, 3].Value != null)
                            {
                                var location = worksheet.Cells[row, 3].Value.ToString();
                                var Warehouse = LocationsServices.Instance.GetLocations(location).FirstOrDefault();
                                if(Warehouse != null)
                                {
                                    inventory._WarehouseId = Warehouse._Id;
                                }
                                else
                                {
                                    var loc = new Locations();
                                    loc._LocationName = location;

                                    LocationsServices.Instance.CreateLocations(loc);
                                }
                            }
                           
                            if (worksheet.Cells[row, 4].Value != null)
                            {
                                inventory._SKU = worksheet.Cells[row, 4].Value.ToString(); //Size

                            }
                            else
                            {
                                inventory._SKU = "000000";
                            }
                            if (worksheet.Cells[row, 5].Value != null)
                            {
                                inventory._Qty = int.Parse(worksheet.Cells[row, 5].Value.ToString()); //Color

                            }
                            else
                            {
                                inventory._Qty = 0;
                            }
                            if (worksheet.Cells[row, 6].Value != null)
                            {
                                inventory._Price = decimal.Parse(worksheet.Cells[row, 6].Value.ToString()); //Cost

                            }
                            else
                            {
                                inventory._Price = 0.000m;
                            }

                            if (worksheet.Cells[row, 7].Value != null)
                            {
                                inventory._Amount = decimal.Parse(worksheet.Cells[row, 7].Value.ToString());

                            }
                            else
                            {
                                inventory._Amount = 0.0000m;
                            }
                            inventory._ExpiryDate = DateTime.Now;

                            InventoryInProductServices.Instance.CreateInventoryInProducts(inventory);

                            var List = ProductServices.Instance.GetProduct();


                            if (List.Count != 0)
                            {
                                foreach (var item in List)
                                {
                                    if (item._Name == inventory._Title && item._CategoryId == inventory._CategoryId)
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
                                    ProductAddedList.Add(inventory);
                                    InventoryInProductServices.Instance.CreateInventoryInProducts(inventory);
                                }
                            }
                            else
                            {
                                ProductAddedList.Add(inventory);

                                InventoryInProductServices.Instance.CreateInventoryInProducts(inventory);
                                //var Stores = LocationsServices.Instance.GetLocations();
                                //foreach (var item in Stores)
                                //{
                                //    var inventorybkp = new InventoryBackups();
                                //    inventorybkp._ProductId = product._Id;
                                //    inventorybkp._Name = product._Name;
                                //    inventorybkp._Size = product._Size;
                                //    inventorybkp._Color = product._Color;
                                //    inventorybkp._Store = item._LocationName;
                                //    inventorybkp._SKU = product._SKU;
                                //    inventorybkp._Weight = product._Weight;
                                //    inventorybkp._Thickness = product._Thickness;
                                //    inventorybkp._Variations = product._Variations;
                                //    inventorybkp._Cost = product._Cost;
                                //    inventorybkp._RetailPrice = product._RetailPrice;

                                //    if (product._OpeningStock == 0)
                                //    {
                                //        inventorybkp._QuantityIn = 0;

                                //    }
                                //    else
                                //    {
                                //        inventorybkp._QuantityIn = product._OpeningStock;
                                //    }
                                //    inventorybkp._QuantityOut = 0;
                                //    inventorybkp._Notes = product._Notes;
                                //    inventorybkp.JustAdded = true;
                                //    InventoryBackupsServices.Instance.CreateInventoryBackup(inventorybkp);
                                //}

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
                var products = InventoryInProductServices.Instance.GetInventoryInProducts().Where(x => x._InventoryInId == model._Id);
                foreach (var item in products)
                {
                    InventoryInProductServices.Instance.DeleteInventoryInProducts(item._Id);
                }
                var InventoryIn = InventoryInServices.Instance.GetInventoryInById(model._Id);
                var backup = new Backup();
                backup.DeletionDate = DateTime.Now;
                backup.ComponenetId = InventoryIn._Id;
                backup.Aspect = InventoryIn._Amount.ToString();
                backup.Type = "Inventory In";
                BackupServices.Instance.CreateBackup(backup);
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