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
            Session["ACTIVER"] = "CSV Index";
            CSVListingViewModel model = new CSVListingViewModel();
            model.csvs = CSVServices.Instance.GetCSVs();
            return View("Index", model);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            if (ID != 0)
            {
                Session["ACTIVER"] = "CSV Edit";
            }
            else
            {
                Session["ACTIVER"] = "CSV Action";
            }
            CSVActionViewModel model = new CSVActionViewModel();
            if (ID != 0)
            {
                var CSV = CSVServices.Instance.GetCSVById(ID);
                model.ID = CSV.ID;
                model._Date = CSV._Date;
                model._Description = CSV._Description;
                model._File = CSV._File;

            }
            return View("Action", model);
        }


        [HttpPost]
        public ActionResult Action(CSVActionViewModel model)
        {

            if (model.ID != 0)
            {
                var CSV = CSVServices.Instance.GetCSVById(model.ID);
                CSV.ID = model.ID;
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

        public ActionResult MassDelete(List<int> ids)
        {
            foreach(var item in ids)
            {
                CSVServices.Instance.DeleteCSV(item);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            Session["ACTIVER"] = "CSV Edit";
            CSVActionViewModel model = new CSVActionViewModel();
            var CSV = CSVServices.Instance.GetCSVById(ID);
            model.ID = CSV.ID;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CSVActionViewModel model)
        {
            if (model.ID != 0)
            {
                var CSV = CSVServices.Instance.GetCSVById(model.ID);

                CSVServices.Instance.DeleteCSV(CSV.ID);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}