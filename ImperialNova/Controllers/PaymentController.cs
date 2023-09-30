using ImperialNova.Services;
using ImperialNova.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ImperialNova.ViewModels.PaymentViewModel;

namespace ImperialNova.Controllers
{
    [Authorize]
    public class PaymentController : Controller
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
        public PaymentController()
        {
        }
        public PaymentController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ActionResult Index()
        {
            Session["ACTIVER"] = "Payment Index";

            PaymentListingViewModel model = new PaymentListingViewModel();
            model.Payments = PaymentServices.Instance.GetPayment();
            return View("Index", model);
        }
        public ActionResult MassDelete(List<int> ids)
        {
            foreach (var item in ids)
            {
                PaymentServices.Instance.DeletePayment(item);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            if (ID != 0)
            {
                Session["ACTIVER"] = "Payment Edit";

            }
            else
            {
                Session["ACTIVER"] = "Payment Action";

            }
            PaymentActionViewModel model = new PaymentActionViewModel();
            if (ID != 0)
            {
                var Payment = PaymentServices.Instance.GetPaymentById(ID);
                model._Id = Payment._Id;
                model._Record = Payment._Record;
                model._Date = Payment._Date;
                model._Individual = Payment._Individual;
                model._Amount = Payment._Amount;
                model._Remarks = Payment._Remarks;

            }
            model.customers = CustomerServices.Instance.GetCustomers();
            return View("Action", model);
        }


        [HttpPost]
        public ActionResult Action(PaymentActionViewModel model)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            if (model._Id != 0)
            {
                var Payment = PaymentServices.Instance.GetPaymentById(model._Id);
                Payment._Id = model._Id;
                Payment._Record = model._Record;
                Payment._Date = model._Date;
                Payment._Individual = model._Individual;
                Payment._Amount = model._Amount;
                Payment._Remarks = model._Remarks;

                PaymentServices.Instance.UpdatePayment(Payment);

            }
            else
            {
                var Payment = new Entities.Payment();
                Payment._Record = model._Record;
                Payment._Date = model._Date;
                Payment._Individual = model._Individual;
                Payment._Amount = model._Amount;
                Payment._Remarks = model._Remarks;

                PaymentServices.Instance.CreatePayment(Payment);

                var notification = new Entities.Notification();
                notification._Description = "New Payment has been Added!";
                notification._UserName = user.Name;
                NotificationServices.Instance.CreateNotification(notification);
            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            PaymentActionViewModel model = new PaymentActionViewModel();
            var Payment = PaymentServices.Instance.GetPaymentById(ID);
            model._Id = Payment._Id;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(PaymentActionViewModel model)
        {
            if (model._Id != 0)
            {
                var Payment = PaymentServices.Instance.GetPaymentById(model._Id);

                PaymentServices.Instance.DeletePayment(Payment._Id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}