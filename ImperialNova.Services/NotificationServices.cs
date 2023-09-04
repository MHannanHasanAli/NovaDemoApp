using ImperialNova.Database;
using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialNova.Services
{
   
    public class NotificationServices
    {
        #region Singleton
        public static NotificationServices Instance
        {
            get
            {
                if (instance == null) instance = new NotificationServices();
                return instance;
            }
        }
        private static NotificationServices instance { get; set; }
        private NotificationServices()
        {
        }
        #endregion

        public List<Notification> GetNotifications()
        {
            using (var context = new DSContext())
            {

                return context.notifications.OrderByDescending(x => x._Id).ToList();

            }
        }
        public Notification GetNotificationById(int id)
        {
            using (var context = new DSContext())
            {
                return context.notifications.Find(id);

            }
        }
        public void CreateNotification(Notification Notification)
        {
            using (var context = new DSContext())
            {
                context.notifications.Add(Notification);
                context.SaveChanges();
            }
        }
        public void DeleteNotification(int ID)
        {
            using (var context = new DSContext())
            {

                var Notification = context.notifications.Find(ID);
                context.notifications.Remove(Notification);
                context.SaveChanges();
            }
        }
        public void DeleteAllNotifications()
        {
            using (var context = new DSContext())
            {
                var allNotifications = context.notifications.ToList();
                context.notifications.RemoveRange(allNotifications);
                context.SaveChanges();
            }
        }
    }
}
