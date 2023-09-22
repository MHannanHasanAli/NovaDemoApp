using ImperialNova.Services;
using ImperialNova.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImperialNova.Controllers
{
    [Authorize]
    public class NotificationsController : Controller
    {
        public ActionResult Index()
        {
            NotificationsListingViewModel model = new NotificationsListingViewModel();
            model.Notifications = NotificationServices.Instance.GetNotifications();
            return View("Index", model);
        }



        //[HttpGet]
        //public ActionResult Delete(int ID)
        //{
        //    NotificationActionViewModel model = new NotificationActionViewModel();
        //    var Notification = NotificationServices.Instance.GetNotificationById(ID);
        //    model._Id = Notification._Id;
        //    model._Description = Notification._Description;

        //    NotificationServices.Instance.DeleteNotification(ID);
        //    RedirectToAction("Index");
        //}

        //[HttpGet]
        //public ActionResult Delete(int ID)
        //{
        //    if (ID != 0)
        //    {
        //        var Notification = NotificationServices.Instance.GetNotificationById(ID);

        //        NotificationServices.Instance.DeleteNotification(Notification._Id);
        //    }

            
        //}

        [HttpPost]
        public ActionResult DeleteAll()
        { 
            NotificationServices.Instance.DeleteAllNotifications();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}