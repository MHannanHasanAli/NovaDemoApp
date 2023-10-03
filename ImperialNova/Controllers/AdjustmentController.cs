using ImperialNova.Entities;
using ImperialNova.Services;
using ImperialNova.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ImperialNova.ViewModels.AdjustmentViewModel;

namespace ImperialNova.Controllers
{
    [Authorize]
    public class AdjustmentController : Controller
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
        public AdjustmentController()
        {
        }
        public AdjustmentController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ActionResult Index()
        {
            Session["ACTIVER"] = "Adjustment Index";
            AdjustmentListingViewModel model = new AdjustmentListingViewModel();
            model.Adjustments = AdjustmentServices.Instance.GetAdjustments();
            foreach (var item in model.Adjustments)
            {
                model.Quantity = model.Quantity + item._Quantity;
            }
            return View("Index", model);
        }

        public static int QuantityUpdate;
        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            if (ID != 0)
            {
                Session["ACTIVER"] = "Adjustment Edit";
            }
            else
            {
                Session["ACTIVER"] = "Adjustment Action";
            }

            AdjustmentActionViewModel model = new AdjustmentActionViewModel();
            if (ID != 0)
            {
                var Adjustment = AdjustmentServices.Instance.GetAdjustmentById(ID);
                model._Id = Adjustment._Id;
                model._Date = Adjustment._Date;
                model._Quantity = Adjustment._Quantity;
                model._Remarks = Adjustment._Remarks;

            }
            model.Locations = LocationsServices.Instance.GetLocations();
            model.products = AdjustmentProductServices.Instance.GetAdjustmentProducts(model._Id);
            return View("Action", model);
        }
        [HttpGet]
        public ActionResult GetProductJson(int ID)
        {
            var product = ProductServices.Instance.GetProductById(ID);
            return Json(product, JsonRequestBehavior.AllowGet);

        }
        //[HttpPost]
        //public ActionResult Action(string products)
        //{
        //    var ListOfInventory = JsonConvert.DeserializeObject<List<InventoryModel>>(products);
        //    foreach(var item in ListOfInventory)
        //    {

        //    }
        //    if (model._Id != 0)
        //    {
        //        var Adjustment = AdjustmentServices.Instance.GetAdjustmentById(model._Id);
        //        Adjustment._Id = model._Id;
        //        Adjustment._Type = model._Type;
        //        Adjustment._Date = model._Date;
        //        Adjustment._Quantity = model._Quantity;
        //        Adjustment._Remarks = model._Remarks;

        //        AdjustmentServices.Instance.UpdateAdjustment(Adjustment);

        //    }
        //    else
        //    {
        //        var Adjustment = new Entities.Adjustment();
        //        Adjustment._Type = model._Type;
        //        Adjustment._Date = model._Date;
        //        Adjustment._Quantity = model._Quantity;
        //        Adjustment._Remarks = model._Remarks;

        //        AdjustmentServices.Instance.CreateAdjustment(Adjustment);
        //    }


        //    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public ActionResult ActionProducts(string products)
        {
            

            QuantityUpdate = 0;
            var adjustmentid = AdjustmentServices.Instance.GetLastAdjustmentId();
            var ListOfInventory = JsonConvert.DeserializeObject<List<ProductModel>>(products);
            var adjproduct = new AdjustmentProduct();
            foreach (var item in ListOfInventory)
            {
                if (item._Quantity == null)
                {
                    break;
                }
                var product = ProductServices.Instance.GetProductById(int.Parse(item._ProductId));
                product._Quantity = int.Parse(item._Quantity);
                QuantityUpdate = QuantityUpdate + product._Quantity;
                ProductServices.Instance.UpdateProduct(product);

               
                adjproduct._Qty = product._Quantity;
                adjproduct._SKU = product._SKU;
                adjproduct._Title = product._Name;
                adjproduct._Photo = product._Photo;
                adjproduct._AdjustmentId = adjustmentid+1;
               
                AdjustmentProductServices.Instance.CreateAdjustmentProduct(adjproduct);

            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Action(AdjustmentActionViewModel model)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (model._Id != 0)
            {
                var Adjustment = AdjustmentServices.Instance.GetAdjustmentById(model._Id);
                Adjustment._Id = model._Id;
                Adjustment._Date = model._Date;
                Adjustment._Quantity = QuantityUpdate;
                Adjustment._Remarks = model._Remarks;

                AdjustmentServices.Instance.UpdateAdjustment(Adjustment);

            

            }
            else
            {
                var Adjustment = new Entities.Adjustment();
                Adjustment._Date = model._Date;
                Adjustment._Quantity = QuantityUpdate;
                Adjustment._Remarks = model._Remarks;

                AdjustmentServices.Instance.CreateAdjustment(Adjustment);

                var notification = new Entities.Notification();
                notification._Description = "New Adjustment has been made!";
                notification._UserName = user.Name;
                NotificationServices.Instance.CreateNotification(notification);
            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MassDelete(List<int> ids)
        {
            foreach (var item in ids)
            {
                AdjustmentServices.Instance.DeleteAdjustment(item);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            Session["ACTIVER"] = "Adjustment Edit";
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
                var backup = new Backup();
                backup.DeletionDate = DateTime.Now;
                backup.ComponenetId = Adjustment._Id;
                backup.Aspect = Adjustment._Quantity.ToString();
                backup.Type = "Adjustment";
                BackupServices.Instance.CreateBackup(backup);
                AdjustmentServices.Instance.DeleteAdjustment(Adjustment._Id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}