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
    [Authorize(Roles = "Admin")]    public class ReminderController : Controller
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
        public ReminderController()
        {
        }



        public ReminderController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ActionResult Index()
        {
            Session["ACTIVER"] = "Reminder Index";

            ReminderListingViewModel model = new ReminderListingViewModel();
            model.Reminders = ReminderServices.Instance.GetReminders();
            return View("Index", model);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            if (ID != 0)
            {
                Session["ACTIVER"] = "Reminder Edit";

            }
            else
            {
                Session["ACTIVER"] = "Reminder Action";

            }
            ReminderActionViewModel model = new ReminderActionViewModel();
            if (ID != 0)
            {
                var Reminder = ReminderServices.Instance.GetReminderById(ID);
                model._Id = Reminder._Id;
                model._CreatedAt = Reminder._CreatedAt;
                model._CreatedBy = Reminder._CreatedBy;
                model._Title = Reminder._Title;
                model._Description = Reminder._Description;

            }
            return View("Action", model);
        }


        [HttpPost]
        public ActionResult Action(string _Id, string _Title, string _Description)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            string decodedDescription = HttpUtility.UrlDecode(_Description);

            if (_Id != "0")
            {
                var Reminder = ReminderServices.Instance.GetReminderById(int.Parse(_Id));
                Reminder._Id = int.Parse(_Id);
                Reminder._CreatedAt = DateTime.Now;
                Reminder._CreatedBy = user.Name;
                Reminder._Title = _Title;
                Reminder._Description = decodedDescription;

                ReminderServices.Instance.UpdateReminder(Reminder);

            }
            else
            {
                var Reminder = new Entities.Reminder();
                Reminder._CreatedAt = DateTime.Now;
                Reminder._CreatedBy = user.Name;
                Reminder._Title = _Title;
                Reminder._Description = decodedDescription;

                ReminderServices.Instance.CreateReminder(Reminder);

                var notification = new Entities.Notification();
                notification._Description = "New Reminder has been Added!";
                NotificationServices.Instance.CreateNotification(notification);
            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            ReminderActionViewModel model = new ReminderActionViewModel();
            var Reminder = ReminderServices.Instance.GetReminderById(ID);
            model._Id = Reminder._Id;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(ReminderActionViewModel model)
        {
            if (model._Id != 0)
            {
                var Reminder = ReminderServices.Instance.GetReminderById(model._Id);

                ReminderServices.Instance.DeleteReminder(Reminder._Id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}