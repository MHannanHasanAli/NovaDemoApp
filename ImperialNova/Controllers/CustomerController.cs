using ImperialNova.Services;
using ImperialNova.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ImperialNova.ViewModels.CustomerViewModel;

namespace ImperialNova.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        public ActionResult Index(string SearchTerm = "")
        {
            Session["ACTIVER"] = "Customer Index";

            CustomerListingViewModel model = new CustomerListingViewModel();
            model.customers = CustomerServices.Instance.GetCustomers();
            return View("Index", model);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            if (ID != 0)
            {
                Session["ACTIVER"] = "Customer Edit";

            }
            else
            {
                Session["ACTIVER"] = "Customer Action";

            }
            CustomerActionViewModel model = new CustomerActionViewModel();
            if (ID != 0)
            {
                var Customer = CustomerServices.Instance.GetCustomerById(ID);
                model._Id      = Customer._Id;
                model._Name    = Customer._Name;
                model._Email   = Customer._Email;
                model._Phone   = Customer._Phone;
                model._Address = Customer._Address;
                model._Zip     = Customer._Zip;
                model._City    = Customer._City;
                model._Country = Customer._Country;

            }
            return View("Action", model);
        }


        [HttpPost]
        public ActionResult Action(CustomerActionViewModel model)
        {

            if (model._Id != 0)
            {
                var Customer = CustomerServices.Instance.GetCustomerById(model._Id);
                Customer._Id = model._Id;
                Customer._Name = model._Name;
                Customer._Email = model._Email;
                Customer._Phone = model._Phone;
                Customer._Address = model._Address;
                Customer._Zip = model._Zip;
                Customer._City = model._City;
                Customer._Country = model._Country;
              

                CustomerServices.Instance.UpdateCustomer(Customer);

            }
            else
            {
                var Customer = new Entities.Customer();
                Customer._Name = model._Name;
                Customer._Email = model._Email;
                Customer._Phone = model._Phone;
                Customer._Address = model._Address;
                Customer._Zip = model._Zip;
                Customer._City = model._City;
                Customer._Country = model._Country;

                CustomerServices.Instance.CreateCustomer(Customer);
                var notification = new Entities.Notification();
                notification._Description = "New Customer has been Added!";
                NotificationServices.Instance.CreateNotification(notification);
            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            CustomerActionViewModel model = new CustomerActionViewModel();
            var Customer = CustomerServices.Instance.GetCustomerById(ID);
            model._Id = Customer._Id;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CustomerActionViewModel model)
        {
            if (model._Id != 0)
            {
                var Customer = CustomerServices.Instance.GetCustomerById(model._Id);

                CustomerServices.Instance.DeleteCustomer(Customer._Id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}