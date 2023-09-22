using ImperialNova.Services;
using ImperialNova.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ImperialNova.ViewModels.CSVViewModel;

namespace ImperialNova.Controllers
{
    [Authorize]
    public class CSVController : Controller
    {
        public ActionResult Index()
        {
            CSVListingViewModel model = new CSVListingViewModel();
            model.csvs = CSVServices.Instance.GetCSVs();
            return View("Index", model);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {

            CSVActionViewModel model = new CSVActionViewModel();
            if (ID != 0)
            {
                var CSV = CSVServices.Instance.GetCSVById(ID);
                model._Id = CSV._Id;
                model._Date = CSV._Date;
                model._Description = CSV._Description;
                model._File = CSV._File;

            }
            return View("Action", model);
        }


        [HttpPost]
        public ActionResult Action(CSVActionViewModel model)
        {

            if (model._Id != 0)
            {
                var CSV = CSVServices.Instance.GetCSVById(model._Id);
                CSV._Id = model._Id;
                CSV._Date = model._Date;
                CSV._Description = model._Description;
                CSV._File = model._File;

                CSVServices.Instance.UpdateCSV(CSV);

            }
            else
            {
                var CSV = new Entities.CSV();
                CSV._Date = model._Date;
                CSV._Description = model._Description;
                CSV._File = model._File;

                CSVServices.Instance.CreateCSV(CSV);

                var notification = new Entities.Notification();
                notification._Description = "New CSV has been Added!";
                NotificationServices.Instance.CreateNotification(notification);
            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            CSVActionViewModel model = new CSVActionViewModel();
            var CSV = CSVServices.Instance.GetCSVById(ID);
            model._Id = CSV._Id;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CSVActionViewModel model)
        {
            if (model._Id != 0)
            {
                var CSV = CSVServices.Instance.GetCSVById(model._Id);

                CSVServices.Instance.DeleteCSV(CSV._Id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}