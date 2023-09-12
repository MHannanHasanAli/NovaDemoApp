using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImperialNova.ViewModels
{
    
    public class ReminderListingViewModel
        {
            public List<Reminder> Reminders { get; set; }
            public string SearchTerm { get; set; }
    }
    public class ReminderActionViewModel
    {
        public int _Id { get; set; }
        public DateTime _CreatedAt { get; set; }
        public string _CreatedBy { get; set; }
        public string _Title { get; set; }
        public string _Description { get; set; }

    }

    
}