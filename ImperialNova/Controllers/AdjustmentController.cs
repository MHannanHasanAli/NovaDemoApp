using ImperialNova.Services;
using ImperialNova.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ImperialNova.ViewModels.AdjustmentViewModel;

namespace ImperialNova.Controllers
{
    public class AdjustmentController : Controller
    {
        public ActionResult Index()
        {
            AdjustmentListingViewModel model = new AdjustmentListingViewModel();
            model.Adjustments = AdjustmentServices.Instance.GetAdjustments();
            return View("Index", model);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {

            AdjustmentActionViewModel model = new AdjustmentActionViewModel();
            if (ID != 0)
            {
                var Adjustment = AdjustmentServices.Instance.GetAdjustmentById(ID);
                model._Id = Adjustment._Id;
                model._Type = Adjustment._Type;
                model._Date = Adjustment._Date;
                model._Quantity = Adjustment._Quantity;
                model._Remarks = Adjustment._Remarks;

            }
            model.Locations = LocationsServices.Instance.GetLocations();
            return View("Action", model);
        }
        [HttpGet]
        public ActionResult GetProductJson(int ID)
        {
            var product = ProductServices.Instance.GetProductById(ID);
            return Json(product, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Action(AdjustmentActionViewModel model)
        {

            if (model._Id != 0)
            {
                var Adjustment = AdjustmentServices.Instance.GetAdjustmentById(model._Id);
                Adjustment._Id = model._Id;
                Adjustment._Type = model._Type;
                Adjustment._Date = model._Date;
                Adjustment._Quantity = model._Quantity;
                Adjustment._Remarks = model._Remarks;

                AdjustmentServices.Instance.UpdateAdjustment(Adjustment);

            }
            else
            {
                var Adjustment = new Entities.Adjustment();
                Adjustment._Type = model._Type;
                Adjustment._Date = model._Date;
                Adjustment._Quantity = model._Quantity;
                Adjustment._Remarks = model._Remarks;

                AdjustmentServices.Instance.CreateAdjustment(Adjustment);
            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AdjustmentActionViewModel model = new AdjustmentActionViewModel();
            var Adjustment = AdjustmentServices.Instance.GetAdjustmentById(ID);
            model._Id = Adjustment._Id;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(AdjustmentActionViewModel model)
        {
            if (model._Id != 0)
            {
                var Adjustment = AdjustmentServices.Instance.GetAdjustmentById(model._Id);

                AdjustmentServices.Instance.DeleteAdjustment(Adjustment._Id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}