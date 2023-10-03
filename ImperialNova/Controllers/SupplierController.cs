using ImperialNova.Entities;
using ImperialNova.Services;
using ImperialNova.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
        public SupplierController()
        {
        }
        public SupplierController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ActionResult Index(string SearchTerm = "")
        {
            Session["ACTIVER"] = "Supplier Index";

            SupplierListingViewModel model = new SupplierListingViewModel();
            model.suppliers = SupplierServices.Instance.GetSuppliers();
            return View("Index", model);
        }

        public ActionResult MassDelete(List<int> ids)
        {
            foreach (var item in ids)
            {
                SupplierServices.Instance.DeleteSupplier(item);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            if (ID != 0)
            {
                Session["ACTIVER"] = "Supplier Edit";

            }
            else
            {
                Session["ACTIVER"] = "Supplier Action";

            }
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
            var user = UserManager.FindById(User.Identity.GetUserId());
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
                notification._UserName = user.Name;
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
                var backup = new Backup();
                backup.DeletionDate = DateTime.Now;
                backup.ComponenetId = Supplier._Id;
                backup.Aspect = Supplier._Name;
                backup.Type = "Supplier";
                BackupServices.Instance.CreateBackup(backup);
                SupplierServices.Instance.DeleteSupplier(Supplier._Id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}