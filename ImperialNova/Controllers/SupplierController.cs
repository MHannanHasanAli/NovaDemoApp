using ImperialNova.Services;
using ImperialNova.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ImperialNova.ViewModels.SupplierViewModel;

namespace ImperialNova.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        public ActionResult Index(string SearchTerm = "")
        {
            SupplierListingViewModel model = new SupplierListingViewModel();
            model.suppliers = SupplierServices.Instance.GetSuppliers();
            return View("Index", model);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {

            SupplierActionViewModel model = new SupplierActionViewModel();
            if (ID != 0)
            {
                var Supplier = SupplierServices.Instance.GetSupplierById(ID);
                model._Id      = Supplier._Id;
                model._Name    = Supplier._Name;
                model._Email   = Supplier._Email;
                model._Phone   = Supplier._Phone;
                model._Address = Supplier._Address;
                model._Zip     = Supplier._Zip;
                model._City    = Supplier._City;
                model._Country = Supplier._Country;

            }
            return View("Action", model);
        }


        [HttpPost]
        public ActionResult Action(SupplierActionViewModel model)
        {

            if (model._Id != 0)
            {
                var Supplier = SupplierServices.Instance.GetSupplierById(model._Id);
                Supplier._Id = model._Id;
                Supplier._Name = model._Name;
                Supplier._Email = model._Email;
                Supplier._Phone = model._Phone;
                Supplier._Address = model._Address;
                Supplier._Zip = model._Zip;
                Supplier._City = model._City;
                Supplier._Country = model._Country;
              

                SupplierServices.Instance.UpdateSupplier(Supplier);

            }
            else
            {
                var Supplier = new Entities.Supplier();
                Supplier._Name = model._Name;
                Supplier._Email = model._Email;
                Supplier._Phone = model._Phone;
                Supplier._Address = model._Address;
                Supplier._Zip = model._Zip;
                Supplier._City = model._City;
                Supplier._Country = model._Country;

                SupplierServices.Instance.CreateSupplier(Supplier);

                var notification = new Entities.Notification();
                notification._Description = "New Supplier has been Added!";
                NotificationServices.Instance.CreateNotification(notification);
            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            SupplierActionViewModel model = new SupplierActionViewModel();
            var Supplier = SupplierServices.Instance.GetSupplierById(ID);
            model._Id = Supplier._Id;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(SupplierActionViewModel model)
        {
            if (model._Id != 0)
            {
                var Supplier = SupplierServices.Instance.GetSupplierById(model._Id);

                SupplierServices.Instance.DeleteSupplier(Supplier._Id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}