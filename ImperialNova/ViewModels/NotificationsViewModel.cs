using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImperialNova.ViewModels
{
    public class NotificationsListingViewModel
    {
        public List<Notification> Notifications { get; set; }
    }

    public class NotificationActionViewModel
    {
        [Key] public int _Id { get; set; }
        public string _Description { get; set; }
    }
}