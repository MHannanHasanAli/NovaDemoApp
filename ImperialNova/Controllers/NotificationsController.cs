using ImperialNova.Services;
using ImperialNova.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImperialNova.Controllers
{
    public class NotificationsController : Controller
    {
        public ActionResult Index()
        {
            NotificationsListingViewModel model = new NotificationsListingViewModel();
            model.Notifications = NotificationServices.Instance.GetNotifications();
            return View("Index", model);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            NotificationActionViewModel model = new NotificationActionViewModel();
            var Notification = NotificationServices.Instance.GetNotificationById(ID);
            model._Id = Notification._Id;
            return View("Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(NotificationActionViewModel model)
        {
            if (model._Id != 0)
            {
                var Notification = NotificationServices.Instance.GetNotificationById(model._Id);

                NotificationServices.Instance.DeleteNotification(Notification._Id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteAll()
        { 
            NotificationServices.Instance.DeleteAllNotifications();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}