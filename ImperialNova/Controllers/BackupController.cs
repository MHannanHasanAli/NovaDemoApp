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
    [Authorize]
    public class BackupController : Controller
    {
        // GET: Backup
        public ActionResult Index()
        {
            Session["ACTIVER"] = "Backup Index";

            BackupListingViewModel model = new BackupListingViewModel();
            model.Backups = BackupServices.Instance.GetBackups();
            return View(model);
        }
        [HttpGet]
        public ActionResult FetchTotalBackups()
        {

            var Backups = new List<Backup>();
            var BackupData = BackupServices.Instance.GetBackup();

           

            return Json(BackupData, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Restore(int ID, string type, int backupid)
        {
            if(type == "Adjustment")
            {
                var data = AdjustmentServices.Instance.GetAdjustmentById(ID);
                data.IsDeleted = false;
                AdjustmentServices.Instance.UpdateAdjustment(data);
            }else if(type == "Category")
            {
                var data = CategoryServices.Instance.GetCategoryById(ID);
                data.IsDeleted = false;
                CategoryServices.Instance.UpdateCategory(data);
            }else if(type == "CSV")
            {
                var data = CSVServices.Instance.GetCSVById(ID);
                data.IsDeleted = false;
                CSVServices.Instance.UpdateCSV(data);

            }
            else if (type == "Customer")
            {
                var data = CustomerServices.Instance.GetCustomerById(ID);
                data.IsDeleted = false;
                CustomerServices.Instance.UpdateCustomer(data);

            }
            else if (type == "Delivery Form")
            {
                var data = DeliveryFormServices.Instance.GetFormById(ID); 
                data.IsDeleted = false;
                DeliveryFormServices.Instance.UpdateDeliveryForm(data);

            }
            else if (type == "Document")
            {
                var data = DocumentServices.Instance.GetDocumentById(ID);
                data.IsDeleted = false;
                DocumentServices.Instance.UpdateDocument(data);
            }
            else if (type == "Expense")
            {
                var data = ExpenseServices.Instance.GetExpenseById(ID);
                data.IsDeleted = false;
                ExpenseServices.Instance.UpdateExpense(data);
            }
            else if (type == "Inventory In")
            {
                var data = InventoryInServices.Instance.GetInventoryInById(ID);
                data.IsDeleted = false;
                InventoryInServices.Instance.UpdateInventoryIn(data);
            }
            else if (type == "Warehouse")
            {
                var data = LocationsServices.Instance.GetLocationsById(ID);
                data.IsDeleted = false;
                LocationsServices.Instance.UpdateLocations(data);
            }
            else if (type == "Order")
            {
                var data = OrderServices.Instance.GetOrderById(ID);
                data.IsDeleted = false;
                OrderServices.Instance.UpdateOrder(data);
            }
            else if (type == "Payment")
            {
                var data = PaymentServices.Instance.GetPaymentById(ID);
                data.IsDeleted = false;
                PaymentServices.Instance.UpdatePayment(data);
            }
            else if (type == "Product")
            {
                var data = ProductServices.Instance.GetProductById(ID);
                data.IsDeleted = false;
                ProductServices.Instance.UpdateProduct(data);
            }
            else if (type == "Reminder")
            {
                var data = ReminderServices.Instance.GetReminderById(ID);
                data.IsDeleted = false;
                ReminderServices.Instance.UpdateReminder(data);
            }
            else
            {
                var data = SupplierServices.Instance.GetSupplierById(ID);
                data.IsDeleted = false;
                SupplierServices.Instance.UpdateSupplier(data);
            }

            BackupServices.Instance.DeleteBackup(backupid);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            BackupActionViewModel model = new BackupActionViewModel();
            var Backup = BackupServices.Instance.GetBackupById(ID);
            model._Id = Backup._Id;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(BackupActionViewModel model)
        {
            if (model._Id != 0)
            {
                var Backup = BackupServices.Instance.GetBackupById(model._Id);

                BackupServices.Instance.DeleteBackup(Backup._Id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

    }
}