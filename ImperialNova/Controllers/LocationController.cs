using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.ExtendedProperties;
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

namespace ImperialNova.Controllers
{

    [Authorize(Roles = "Admin")]
    public class LocationController : Controller
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
        public LocationController()
        {
        }
        public LocationController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        //LocationServices LocationServices = new LocationServices();
        public ActionResult Index(string SearchTerm = "")
        {
            Session["ACTIVER"] = "Warehouse Index";

            LocationListingViewModel model = new LocationListingViewModel();
            model.SearchTerm = SearchTerm;
            model.Locations = LocationsServices.Instance.GetLocations(SearchTerm);
            return View("Index", model);
        }

        public ActionResult MassDelete(List<int> ids)
        {
            foreach (var item in ids)
            {
                LocationsServices.Instance.DeleteLocations(item);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            if (ID != 0)
            {
                Session["ACTIVER"] = "Warehouse Edit";

            }
            else
            {
                Session["ACTIVER"] = "Warehouse Action";

            }

            LocationActionViewModel model = new LocationActionViewModel();
            if (ID != 0)
            {
                var Location = LocationsServices.Instance.GetLocationsById(ID);
                model._Id = Location._Id;
                model._LocationName = Location._LocationName;

            }
            return View("Action", model);
        }


        [HttpPost]
        public ActionResult Action(LocationActionViewModel model)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (model._Id != 0)
            {
                var Location = LocationsServices.Instance.GetLocationsById(model._Id);
                Location._Id = model._Id;
                Location._LocationName = model._LocationName;

                LocationsServices.Instance.UpdateLocations(Location);

            }
            else
            {
                var Location = new Locations();
                Location._LocationName = model._LocationName;

                LocationsServices.Instance.CreateLocations(Location);

                var notification = new Entities.Notification();
                notification._Description = "New Warehouse location has been Added!";
                notification._UserName = user.Name;
                NotificationServices.Instance.CreateNotification(notification);
            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            LocationActionViewModel model = new LocationActionViewModel();
            var Location = LocationsServices.Instance.GetLocationsById(ID);
            model._Id = Location._Id;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(LocationActionViewModel model)
        {
            if (model._Id != 0)
            {
                var Location = LocationsServices.Instance.GetLocationsById(model._Id);
                var backup = new Backup();
                backup.DeletionDate = DateTime.Now;
                backup.ComponenetId = Location._Id;
                backup.Aspect = Location._LocationName;
                backup.Type = "Warehouse";
                BackupServices.Instance.CreateBackup(backup);
                LocationsServices.Instance.DeleteLocations(Location._Id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}
