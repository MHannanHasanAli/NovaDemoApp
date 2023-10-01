using ImperialNova.Database;
using ImperialNova.Entities;
using ImperialNova.Services;
using Microsoft.CodeAnalysis;
using System.Data;
using System;
using System.Security.Claims;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.IO;
using ClosedXML.Excel;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Text;
using ImperialNova.ViewModels;
using DocumentFormat.OpenXml.Bibliography;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.EMMA;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;
using Microsoft.AspNet.Identity.Owin;

namespace ImperialNova.Controllers
{

    [Authorize]
    public class InventoryController : Controller
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
       
        public InventoryController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        private readonly IEmailSender _emailSender;
        private readonly DSContext _context;

        public InventoryController()
        {

        }
        public InventoryController(DSContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        public void CheckProductQuantities()
        {
            List<InventoryBackups> emailproducts = new List<InventoryBackups>();
            var products = InventoryBackupsServices.Instance.GetInventories();
            foreach (var product in products)
            {
                if (product._Quantity <= 25 && product.JustAdded == false)
                {
                    emailproducts.Add(product);
                }
            }
            if (emailproducts.Count > 0)
            {
                LowStockAlertSendEmail(emailproducts);
            }


        }
        public async Task<ActionResult> ExportProductInExcelAndEmail()
        {
            var inventory = InventoryBackupsServices.Instance.GetInventoryBackup().OrderBy(x=>x._Store).ToList();
            var fileName = "Stock Update.xlsx";
            //var memoryStream = GenerateExcel(inventory);
            SendEmailWithExcelAttachment(inventory, "hannanhassan1012@gmail.com");
            //SendEmailWithAttachment(fileName, memoryStream);
            return RedirectToAction("Index");
        }

        private async Task LowStockAlertSendEmail(List<InventoryBackups> products)
        {
            var userEmail = "Imperialbamboologistics@outlook.com"; // Replace with the recipient's email address
            var subject = "Low Quantity Alert";
            var body = "";

            foreach (var product in products)
            {

                body += "<p>Order " + product._Name + " for " + product._Store + " stock before it runs out!</p>";

            }

            SendEmail(userEmail, subject, body);

        }
        public bool SendEmail(string toEmail, string subject, string emailBody)
        {
            try
            {
                string senderEmail = "Imperialbamboologistics@outlook.com";
                string senderPassword = "Logistics1234";

                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                client.Host = "smtp.office365.com";
                client.EnableSsl = true;
                client.Port = 587;

                MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, emailBody);
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(toEmail);
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;

                client.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                //Session["EmailStatus"] = ex.ToString();
                return false;
            }

        }
        public void SendEmailWithExcelAttachment(IEnumerable<InventoryBackups> inventory, string toEmail)
        {
            try
            {
                using (var excelStream = GenerateExcel(inventory))
                {
                    // Configure SMTP settings based on your web hosting provider's specifications
                    var smtpHost = "smtp.mubixsol.com"; // e.g., "smtp.example.com"
                    var smtpPort = 465; // Update with the appropriate port
                    var smtpUsername = "info@mubixsol.com";
                    var smtpPassword = "MubinDon123@#$";

                    var smtpClient = new SmtpClient(smtpHost)
                    {
                        Port = smtpPort,
                        Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                        EnableSsl = true // Use SSL for secure connection
                    };

                    using (var mailMessage = new MailMessage("your-email@example.com", toEmail)
                    {
                        Subject = "Inventory Report",
                        Body = "Please find the attached inventory report."
                    })
                    {
                        var attachment = new Attachment(excelStream, "InventoryReport.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                        mailMessage.Attachments.Add(attachment);

                        smtpClient.Send(mailMessage);
                    }
                }
            }
            catch (Exception ex)

            {
                // Handle exceptions here
            }
        }


        //public bool SendEmailWithAttachment(string toEmail, string subject, string emailBody, MemoryStream attachmentStream, string attachmentFileName,string contenttype)
        //{
        //    try
        //    {
        //        string senderEmail = "Imperialbamboologistics@outlook.com";
        //        string senderPassword = "Logistics1234";



        //        using (MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, emailBody))
        //        {
        //            SmtpClient client = new SmtpClient();
        //            client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //            client.UseDefaultCredentials = false;
        //            client.Credentials = new NetworkCredential(senderEmail, senderPassword);
        //            client.Host = "smtp.office365.com";
        //            client.EnableSsl = true;
        //            client.Port = 587;

        //            mailMessage.IsBodyHtml = true;
        //            mailMessage.BodyEncoding = Encoding.UTF8;
        //            mailMessage.To.Add(toEmail);
        //            mailMessage.Attachments.Add(new Attachment(attachmentStream, attachmentFileName));
        //            client.Send(mailMessage);

        //            return true;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //Session["EmailStatus"] = ex.ToString();
        //        return false;
        //    }
        //}

        //public bool SendEmailWithAttachment(string fileName, MemoryStream memoryStream)
        //{
        //    var userEmail = "Imperialbamboologistics@outlook.com"; // Replace with the recipient's email address
        //    var subject = "Stock Updated";
        //    var body = "Please find the attached Excel file containing data.";
        //    bool emailSent = false;
        //    // Clone the memory stream to a new memory stream to prevent it from being closed
        //    using (var emailStream = new MemoryStream(memoryStream.ToArray()))
        //    {
        //        // Set the position of the emailStream back to the beginning
        //        emailStream.Position = 0;

        //         emailSent = SendEmailWithAttachment(userEmail, subject, body, emailStream, fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        //    }
        //    return emailSent;

        //}
        private MemoryStream GenerateExcel(IEnumerable<InventoryBackups> Inventory)
        {
            try
            {
                System.Data.DataTable dataTable = new System.Data.DataTable("InventoryBackups");
                dataTable.Columns.AddRange(new DataColumn[]
                {

            new DataColumn("Product"),
            new DataColumn("Size"),
            new DataColumn("Color"),
            new DataColumn("SKU"),
            new DataColumn("Weight"),
            new DataColumn("Thickness"),
            new DataColumn("Variation"),
            new DataColumn("Quantity In"),
            new DataColumn("Quantity Out"),
            new DataColumn("Quantity Available"),
            new DataColumn("Export Date"),
            new DataColumn("Export Store")
                });

                foreach (var products in Inventory)
                {
                    dataTable.Rows.Add(products._Name, products._Size, products._Color, products._SKU, products._Weight, products._Thickness, products._Variations, products._QuantityIn, products._QuantityOut, products._Quantity, products._ExportDate, products._Store);
                }

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dataTable);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return stream;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public ActionResult Index(string SearchTerm = "")
        {
            CheckProductQuantities();
            InventoryListingViewModel model = new InventoryListingViewModel();
            model.SearchTerm = SearchTerm;
            model.inventories = InventoryBackupsServices.Instance.GetInventories(SearchTerm).Where(x=>x.JustAdded == false).ToList();          
            return View("Index", model);
        }

        [HttpGet]
        public ActionResult Action(int ID = 0)
        {

            InventoryActionViewModel model = new InventoryActionViewModel();
            //model.Products = ProductServices.Instance.GetProduct();
            model.Locations = LocationsServices.Instance.GetLocations();
            if (ID != 0)
            {
               
                var inventory = InventoryBackupsServices.Instance.GetInventoryById(ID);
                model._Id = inventory._Id;
                model._ProductId = inventory._ProductId;
                model._Name = inventory._Name;
                model._Size = inventory._Size;
                model._Color = inventory._Color;
                model._Cost = inventory._Cost;
                model._RetailPrice = inventory._RetailPrice;
                model._QuantityIn = inventory._QuantityIn;
                model._QuantityOut = inventory._QuantityOut;
                model._Notes = inventory._Notes;
                model._ExportDate = inventory._ExportDate;

                model._CategoryId = inventory._CategoryId;
                model._Action = inventory._Action;

                model._UserId = inventory._UserId;
                model._UserFullName = inventory._UserFullName;
                model._UserEmail = inventory._UserEmail;
                model._Store = inventory._Store;

            }
            return View("Action", model);
        }
        [HttpGet]
        public ActionResult ActionOut(int ID = 0)
        {

            InventoryActionViewModel model = new InventoryActionViewModel();
            //model.Products = InventoryBackupsServices.Instance.GetInventoryBackupByStore();
            model.Locations = LocationsServices.Instance.GetLocations();
            if (ID != 0)
            {

                var inventory = InventoryBackupsServices.Instance.GetInventoryById(ID);
                model._Id = inventory._Id;
                model._ProductId = inventory._ProductId;
                model._Name = inventory._Name;
                model._Size = inventory._Size;
                model._Color = inventory._Color;
                model._Cost = inventory._Cost;
                model._RetailPrice = inventory._RetailPrice;
                model._QuantityIn = inventory._QuantityIn;
                model._QuantityOut = inventory._QuantityOut;
                model._Notes = inventory._Notes;
                model._ExportDate = inventory._ExportDate;

                model._CategoryId = inventory._CategoryId;
                model._Action = inventory._Action;

                model._UserId = inventory._UserId;
                model._UserFullName = inventory._UserFullName;
                model._UserEmail = inventory._UserEmail;
                model._Store = inventory._Store;

            }
            return View("ActionOut", model);
        }
        [HttpGet]
        public ActionResult Transfer()
        {
            Session["ACTIVER"] = "Transfer Goods";

            InventoryActionViewModel model = new InventoryActionViewModel();
            model.Locations = LocationsServices.Instance.GetLocations();
            return View("Transfer",model);
        }

        [HttpGet]
        public ActionResult GetProductInventoryJson(string location)
        {
            var ProductWithStores = InventoryBackupsServices.Instance.GetInventoryBackupByStore(location);
            var Allproducts = InventoryBackupsServices.Instance.GetInventories();
            var InventoryList = new List<InventoryBackups>();


            foreach (var product in ProductWithStores)
            {
                InventoryList.Add(product);
            }
            foreach (var product in Allproducts)
            {
                foreach (var AddedProduct in ProductWithStores)
                {
                    if (product._ProductId != AddedProduct._ProductId)
                    {
                        product._QuantityIn = 0;
                        product._QuantityOut = 0;
                        InventoryList.Add(product);
                    }
                }
            }
            if (ProductWithStores.Count == 0)
            {
                foreach (var product in Allproducts)
                {
                    product._QuantityIn = 0;
                    product._QuantityOut = 0;
                    InventoryList.Add(product);
                }
            }
            return Json(InventoryList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetProductInventoryJsonOut(string location)
        {
            var ProductWithStores = InventoryBackupsServices.Instance.GetInventoryBackupByStore(location);
            var products = ProductServices.Instance.GetProducts();
            var dropdownproducts = new List<Product>();
            foreach (var item in products)
            {
                if(item._Warehouse == location)
                {
                    dropdownproducts.Add(item);
                }
            }
            return Json(dropdownproducts, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetProductInJson(int ID)
        {
            var product = InventoryBackupsServices.Instance.GetInventoryById(ID);
            return Json(product, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetProductJson(int ID)
        {
            var product = ProductServices.Instance.GetProductById(ID);
            return Json(product, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Action(string products)
        {
            var ListOfInventory = JsonConvert.DeserializeObject<List<InventoryModel>>(products);
            foreach (var item in ListOfInventory)
            {
                var product = ProductServices.Instance.GetProductById(int.Parse(item._ProductId));
                var inventoryBackup = InventoryBackupsServices.Instance.GetInventoryBackupByStore(item._Store).Where(x => x._ProductId == int.Parse(item._ProductId)).FirstOrDefault();
                inventoryBackup._ProductId = int.Parse(item._ProductId);
                if(inventoryBackup._QuantityIn == 0)
                {
                    inventoryBackup._QuantityIn = int.Parse(item._QuantityIn);
                    inventoryBackup._QuantityOut = int.Parse(item._QuantityOut);
                    inventoryBackup._Store = item._Store;
                    inventoryBackup._Notes = item._Notes;
                    product._QuantityIn = inventoryBackup._QuantityIn;
                    inventoryBackup.JustAdded = false;
                    ProductServices.Instance.UpdateProduct(product);
                    InventoryBackupsServices.Instance.UpdateInventory(inventoryBackup);
                }
                else
                {
                    inventoryBackup._QuantityIn = int.Parse(item._QuantityIn) + product._QuantityIn;
                    inventoryBackup._QuantityOut = int.Parse(item._QuantityOut);
                    inventoryBackup._Store = item._Store;
                    inventoryBackup._Notes = item._Notes;
                    product._QuantityIn = product._QuantityIn + inventoryBackup._QuantityIn;
                    inventoryBackup.JustAdded = false;
                    ProductServices.Instance.UpdateProduct(product);
                    InventoryBackupsServices.Instance.UpdateInventory(inventoryBackup);
                }
                //inventoryBackup._Name = item._Name;
                //inventoryBackup._Size = item._Size;
                //inventoryBackup._Color = item._Color;
                //inventoryBackup._Cost = decimal.Parse(item._Cost);
                //inventoryBackup._RetailPrice = decimal.Parse(item._RetailPrice);
               
                //inventoryBackup._Notes = item._Notes;
               
                //inventoryBackup._CategoryId = int.Parse(item._CategoryId);
                //Saray daal
                

            }
            ExportProductInExcelAndEmail();
            return Json(new {success=true},JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ActionOut(string products)
        {
            var ListOfInventory = JsonConvert.DeserializeObject<List<InventoryModel>>(products);
            foreach (var item in ListOfInventory)
            {
                var product = ProductServices.Instance.GetProductById(int.Parse(item._ProductId));
                var inventoryBackup = InventoryBackupsServices.Instance.GetInventoryBackupByStore(item._Store).Where(x => x._ProductId == int.Parse(item._ProductId)).FirstOrDefault();
                inventoryBackup._ProductId = int.Parse(item._ProductId);
                if (inventoryBackup._QuantityOut == 0)
                {
                    inventoryBackup._QuantityIn = int.Parse(item._QuantityIn);
                    inventoryBackup._QuantityOut = int.Parse(item._QuantityOut);
                    inventoryBackup._Store = item._Store;
                    inventoryBackup._Notes = item._Notes;
                    product._QuantityIn = inventoryBackup._QuantityIn;
                    inventoryBackup.JustAdded = false;
                    ProductServices.Instance.UpdateProduct(product);
                    InventoryBackupsServices.Instance.UpdateInventory(inventoryBackup);
                }
                else
                {
                    inventoryBackup._QuantityIn = int.Parse(item._QuantityIn);
                    inventoryBackup._QuantityOut = int.Parse(item._QuantityOut) + product._QuantityOut;
                    inventoryBackup._Store = item._Store;
                    inventoryBackup._Notes = item._Notes;
                    product._QuantityIn = product._QuantityIn + inventoryBackup._QuantityIn;
                    inventoryBackup.JustAdded = false;
                    ProductServices.Instance.UpdateProduct(product);
                    InventoryBackupsServices.Instance.UpdateInventory(inventoryBackup);
                }
                //var ListOfInventory = JsonConvert.DeserializeObject<List<InventoryModel>>(products);
                //foreach (var item in ListOfInventory)
                //{
                //    var product = ProductServices.Instance.GetProductById(int.Parse(item._ProductId));
                //    var inventoryBackup = new InventoryBackups();
                //    inventoryBackup._ProductId = int.Parse(item._ProductId);
                //    inventoryBackup._Name = item._Name;
                //    inventoryBackup._Size = item._Size;
                //    inventoryBackup._Color = item._Color;
                //    inventoryBackup._Cost = decimal.Parse(item._Cost);
                //    inventoryBackup._RetailPrice = decimal.Parse(item._RetailPrice);
                //    inventoryBackup._QuantityIn = int.Parse(item._QuantityIn);
                //    inventoryBackup._QuantityOut = int.Parse(item._QuantityOut) + product._QuantityOut;
                //    inventoryBackup._Notes = item._Notes;
                //    inventoryBackup._Store = item._Store;
                //    inventoryBackup._CategoryId = int.Parse(item._CategoryId);
                //    //Saray daal
                //    product._QuantityOut = inventoryBackup._QuantityOut;
                //    ProductServices.Instance.UpdateProduct(product);
                //    InventoryBackupsServices.Instance.CreateInventoryBackup(inventoryBackup);

                //}

            }
            ExportProductInExcelAndEmail();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ActionTransfer(string products, string Tostore)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            var ListOfInventory = JsonConvert.DeserializeObject<List<InventoryModel>>(products);
            foreach (var item in ListOfInventory)
            {
                var product = ProductServices.Instance.GetProductById(int.Parse(item._ProductId));
                if(item._QuantityIn != null)
                {
                    product._Quantity = product._Quantity - int.Parse(item._QuantityIn);
                    ProductServices.Instance.UpdateProduct(product);
                }

                var flag = 0;
                var allproducts = ProductServices.Instance.GetProducts();
                foreach (var check in allproducts)
                {
                    if (product._Name == check._Name && check._Warehouse == Tostore && product._Size == check._Size && product._Color == check._Color)
                    {
                        if (item._QuantityIn != null)
                        {
                            check._Quantity = check._Quantity + int.Parse(item._QuantityIn);
                            ProductServices.Instance.UpdateProduct(check);
                            flag = 1;
                        }

                    }
                }
                if(flag != 1)
                {
                        if (item._QuantityIn != null)
                        {
                            var Product = new Product();
                            Product._Name = product._Name;
                            Product._Size = product._Size;
                            Product._Color = product._Color;
                            Product._Cost = product._Cost;
                            Product._SKU = product._SKU;
                            Product._Weight = product._Weight;
                            Product._Thickness = product._Thickness;
                            Product._Variations = product._Variations;
                            Product._RetailPrice = product._RetailPrice;
                            //Product._QuantityIn = check._QuantityIn;
                            //Product._Quantity = check._OpeningStock;
                            //Product._QuantityOut = check._QuantityOut;
                            Product._Notes = product._Notes;
                            Product._ExportDate = DateTime.Now;
                            Product._CategoryId = product._CategoryId;
                            //Product._WarehouseId = check._WarehouseId;
                            Product._LowStockAlert = product._LowStockAlert;
                            Product._Photo = product._Photo;
                            Product._Quantity = int.Parse(item._QuantityIn);
                            var data = LocationsServices.Instance.GetLocations();
                            foreach (var location in data)
                            {
                                if(Tostore == location._LocationName)
                                {
                                    Product._WarehouseId = location._Id;
                                    Product._Warehouse = Tostore;
                                    break;
                                }
                            }
                            
                            ProductServices.Instance.CreateProduct(Product);
                        }
                    
                }
                
                //ProductServices.Instance.UpdateProduct(product);

                //var inventoryBackupIn = InventoryBackupsServices.Instance.GetInventoryBackupByStore(Tostore).Where(x => x._ProductId == int.Parse(item._ProductId)).FirstOrDefault();
                //if(inventoryBackupIn._QuantityIn == 0)
                //{
                //    inventoryBackupIn._ProductId = int.Parse(item._ProductId);
                //    inventoryBackupIn._QuantityIn = int.Parse(item._QuantityOut);
                //    //inventoryBackupIn._QuantityOut = int.Parse(item._QuantityOut);
                //    inventoryBackupIn._Store = Tostore;
                //    product._QuantityIn = inventoryBackupIn._QuantityIn;
                //    inventoryBackupIn.JustAdded = false;
                //    ProductServices.Instance.UpdateProduct(product);
                //    InventoryBackupsServices.Instance.UpdateInventory(inventoryBackupIn);
                //}
                //else
                //{
                //    inventoryBackupIn._ProductId = int.Parse(item._ProductId);
                //    inventoryBackupIn._QuantityIn = int.Parse(item._QuantityIn) + product._QuantityOut;
                //    inventoryBackupIn._QuantityOut = int.Parse(item._QuantityOut);
                //    inventoryBackupIn._Store = Tostore;
                //    product._QuantityIn = inventoryBackupIn._QuantityIn;
                //    inventoryBackupIn.JustAdded = false;
                //    ProductServices.Instance.UpdateProduct(product);
                //    InventoryBackupsServices.Instance.UpdateInventory(inventoryBackupIn);

                //}

                //var ListOfInventory = JsonConvert.DeserializeObject<List<InventoryModel>>(products);
                //foreach (var item in ListOfInventory)
                //{
                //    var product = ProductServices.Instance.GetProductById(int.Parse(item._ProductId));
                //    var inventoryBackup = new InventoryBackups();
                //    inventoryBackup._ProductId = int.Parse(item._ProductId);
                //    inventoryBackup._Name = item._Name;
                //    inventoryBackup._Size = item._Size;
                //    inventoryBackup._Color = item._Color;
                //    inventoryBackup._Cost = decimal.Parse(item._Cost);
                //    inventoryBackup._RetailPrice = decimal.Parse(item._RetailPrice);
                //    inventoryBackup._QuantityIn = int.Parse(item._QuantityIn);
                //    inventoryBackup._QuantityOut = int.Parse(item._QuantityOut) + product._QuantityOut;
                //    inventoryBackup._Notes = item._Notes;
                //    inventoryBackup._Store = item._Store;
                //    inventoryBackup._CategoryId = int.Parse(item._CategoryId);
                //    //Saray daal
                //    product._QuantityOut = inventoryBackup._QuantityOut;
                //    ProductServices.Instance.UpdateProduct(product);
                //    InventoryBackupsServices.Instance.CreateInventoryBackup(inventoryBackup);

                //}

            }
            var notification = new Entities.Notification();
            notification._Description = "Warehouse transfer has been done!";
            notification._UserName = user.Name;
            NotificationServices.Instance.CreateNotification(notification);
            //ExportProductInExcelAndEmail();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        //[HttpPost]
        //public ActionResult Action(InventoryActionViewModel model)
        //{         
        //    var data = ProductServices.Instance.GetProductById(model._ProductId);
        //    var inventory = InventoryBackupsServices.Instance.GetInventoryById(model._Id);
        //    inventory._Id = model._Id;
        //    inventory._ProductId = model._ProductId;
        //    inventory._Name = model._Name;
        //    inventory._Size = model._Size;
        //    inventory._Color = model._Color;
        //    inventory._Cost = model._Cost;
        //    inventory._RetailPrice = model._RetailPrice;
        //    inventory._QuantityIn = inventory._QuantityIn + model._QuantityIn;
        //    inventory._QuantityOut = model._QuantityOut;
        //    inventory._Notes = model._Notes;
        //    inventory._ExportDate = model._ExportDate;
        //    inventory._Store = model._Store;
        //    data._QuantityIn = inventory._QuantityIn;
        //    ProductServices.Instance.UpdateProduct(data);
        //    InventoryBackupsServices.Instance.UpdateInventory(inventory);

        //    //else
        //    //{
        //    //    var data = ProductServices.Instance.GetProductById(model._ProductId);
        //    //    var inventory = new InventoryBackups();
        //    //    inventory._ProductId = model._ProductId;
        //    //    inventory._Name = data._Name;
        //    //    inventory._Size = data._Size;
        //    //    inventory._Color = data._Color;
        //    //    inventory._Cost = data._Cost;
        //    //    inventory._RetailPrice = data._RetailPrice;
        //    //    inventory._QuantityIn = data._QuantityIn + model._QuantityIn;
        //    //    inventory._QuantityOut = data._QuantityOut;
        //    //    inventory._Notes = data._Notes;
        //    //    inventory._ExportDate = DateTime.Now;
        //    //    inventory._Store = model._Store;
        //    //    data._QuantityIn = data._QuantityIn + model._QuantityIn;
        //    //    ProductServices.Instance.UpdateProduct(data);
        //    //    InventoryBackupsServices.Instance.CreateInventoryBackup(inventory);
        //    //}
        //    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        //}



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            InventoryActionViewModel model = new InventoryActionViewModel();
            var Inventory = InventoryBackupsServices.Instance.GetInventoryById(ID);
            model._Id = Inventory._Id;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(InventoryActionViewModel model)
        {
            if (model._Id != 0)
            {
                var Inventory = InventoryBackupsServices.Instance.GetInventoryById(model._Id);

                InventoryBackupsServices.Instance.DeleteInventory(Inventory._Id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }


    //private async Task SendEmailCustomer(DeliveryForm formdata, List<DeliveryFormProductsDB> products)
    //{
    //    var userEmail = formdata._Email; // Customer Email
    //    var subject = "Delivery Form";
    //    var body = "Salesperson: " + formdata._SlaesPerson + "\n" +
    //               "Date: " + formdata._Date + "\n" +
    //               "Customer Name: " + formdata._CustomerName + "\n" +
    //               "Address: " + formdata._Address + "\n" +
    //               "Country: " + formdata._Country + "\n" +
    //               "Contact No: " + formdata._ContactNo + "\n" +
    //               "Email: " + formdata._Email + "\n" +
    //               "Note: " + formdata._Note + "\n" +
    //               "Requested Date: " + formdata._RequestedDate + "\n" +
    //               "Total Amount: " + formdata._TotalAmount + "/-\n" +
    //               "Payment Method: " + formdata._PayMethod + "/-\n" +
    //               "Amount Paid: " + formdata._AmountPaid + "/-\n" +
    //               "Amount In Balance: " + formdata._AmountInBalance + "\n";

    //    foreach (var product in products)
    //    {
    //        body += "Product Name: " + product._ProductName + " Quantity: " + product._ProductQuantity + "\n";
    //    }


    //    await _emailSender.SendEmailAsync(userEmail, subject, body);


    //}


    //Task SendEmailWithAttachment(string fileName, object memoryStream)
    //{
    //    var userEmail = "Imperialbamboologistics@outlook.com"; // Replace with the recipient's email address
    //    var subject = "Stock Updated";
    //    var body = "Please find the attached Excel file containing data.";

    //    // Clone the memory stream to a new memory stream to prevent it from being closed
    //    using (var emailStream = new MemoryStream(memoryStream.ToArray()))
    //    {
    //        // Set the position of the emailStream back to the beginning
    //        emailStream.Position = 0;

    //        await _emailSender.SendEmailAsync(userEmail, subject, body, emailStream, fileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    //    }
    //}



    //{
    //    //ProductServices ProductServices = new ProductServices();
    //    InventoryServices InventoryServices = new InventoryServices();
    //    List<Inventory> productListWithQuantities = new List<Inventory>();
    //    InventoryBackupsServices InventoryBackupsServices = new InventoryBackupsServices();
    //    InventoryTemporaryServices InventoryTemporaryServices = new InventoryTemporaryServices();
    //    //locationservices locationservices = new locationservices();
    //    private readonly UserManager<User> _userManager;
    //    public readonly IEmailSender _emailSender;
    //    private readonly DSContext _context;
    //    //private readonly ILogger<InventoryController> _logger;
    //    //public InventoryController(UserManager<User> userManager, DSContext context, IEmailSender emailSender)
    //    //{
    //    //    _userManager = userManager;
    //    //    _context = context;
    //    //    _emailSender = emailSender;
    //    //}
    //    //public InventoryController()
    //    //{

    //    //}
    //    public bool SendEmail(string toEmail, string subject, string emailBody)
    //    {
    //        try
    //        {
    //            string senderEmail = "Imperialbamboologistics@outlook.com";
    //            string senderPassword = "Logistics1234";

    //            SmtpClient client = new SmtpClient();
    //            client.DeliveryMethod = SmtpDeliveryMethod.Network;
    //            client.UseDefaultCredentials = false;
    //            client.Credentials = new NetworkCredential(senderEmail, senderPassword);
    //            client.Host = "smtp.office365.com";
    //            client.EnableSsl = true;
    //            client.Port = 587;

    //            MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, emailBody);
    //            mailMessage.IsBodyHtml = true;
    //            mailMessage.To.Add(toEmail);
    //            mailMessage.BodyEncoding = UTF8Encoding.UTF8;

    //            client.Send(mailMessage);

    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            Session["EmailStatus"] = ex.ToString();
    //            return false;
    //        }

    //    }
    //    public ActionResult Index()
    //    {
    //        var products = ProductServices.GetProducts();
    //        ViewBag.Products = products;
    //        ViewBag.UserId = User.Identity.GetUserId();
    //        var locations = LocationsServices.GetLocations();
    //        ViewBag.Locations = locations;
    //        CheckProductQuantities();
    //        return View();
    //    }

    //    public IEmailSender emailsender;

    //    public JsonResult GetUserById(string id)
    //    {
    //        using (var context = new DSContext())
    //        {
    //            // Assuming your 'User' class represents the user entity in your database
    //            User user = context.Users.FirstOrDefault(u => u.Id == id);
    //            return Json(user?.Name); // Return the Name property if the user is found, or null if not found

    //        }
    //    }
    //    public JsonResult GetUserByIdEmail(string id)
    //    {
    //        using (var context = new DSContext())
    //        {
    //            // Assuming your 'User' class represents the user entity in your database
    //            User user = context.Users.FirstOrDefault(u => u.Id == id);
    //            return  Json(user?.Email); // Return the Name property if the user is found, or null if not found
    //        }
    //    }
    //    static public string locationselected; 
    //    public JsonResult AddStock(List<Inventory> selectedProducts)
    //    {
    //        foreach (var product in selectedProducts)
    //        {
    //            var data = ProductServices.GetProductById(product._ProductId);


    //            productListWithQuantities.Add(new Inventory
    //            {
    //                _ProductId = product._ProductId,
    //                _ProductName = data._Name,
    //                _ToBeChangedQuantity = product._ToBeChangedQuantity
    //            });
    //        }

    //        foreach (var product in productListWithQuantities)
    //        {
    //            InventoryServices.CreateInventory(product);
    //        }

    //        //ExportProductInExcelAndEmail();
    //        return Json("Added");
    //    }

    //    public JsonResult Create( InventoryBackups inventoryBackups)
    //    {           

    //        InventoryBackupsServices.CreateInventoryBackup(inventoryBackups);

    //        return  Json("Inventory Added");
    //    }
    //    public JsonResult CreateTemp(InventoryTemporary inventoryTemporary)
    //    {

    //        InventoryTemporaryServices.CreateInventoryTemporary(inventoryTemporary);
    //        ExportProductInExcelAndEmail();            
    //        return Json("Inventory Added");
    //    }

    //}
}
