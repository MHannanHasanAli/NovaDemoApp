using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.ExtendedProperties;
using ImperialNova.Entities;
using ImperialNova.Services;
using ImperialNova.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ImperialNova.Controllers
{

    [Authorize(Roles = "Admin")]
    public class LocationController : Controller
    {
        //LocationServices LocationServices = new LocationServices();
        public ActionResult Index(string SearchTerm = "")
        {
            LocationListingViewModel model = new LocationListingViewModel();
            model.SearchTerm = SearchTerm;
            model.Locations = LocationsServices.Instance.GetLocations(SearchTerm);
            return View("Index", model);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {

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

                LocationsServices.Instance.DeleteLocations(Location._Id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}
