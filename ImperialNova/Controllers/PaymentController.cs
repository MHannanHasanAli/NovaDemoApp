using ImperialNova.Services;
using ImperialNova.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ImperialNova.ViewModels.PaymentViewModel;

namespace ImperialNova.Controllers
{
    public class PaymentController : Controller
    {
        public ActionResult Index()
        {
            PaymentListingViewModel model = new PaymentListingViewModel();
            model.Payments = PaymentServices.Instance.GetPayment();
            return View("Index", model);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {

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