using ImperialNova.Database;
using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialNova.Services
{
    public class ReminderServices
    {
        #region Singleton
        public static ReminderServices Instance
        {
            get
            {
                if (instance == null) instance = new ReminderServices();
                return instance;
            }
        }
        private static ReminderServices instance { get; set; }
        private ReminderServices()
        {
        }
        #endregion
        
        public List<string> GetReminderNames()
        {
            using (var context = new DSContext())
            {
                var data = context.reminders.Select(c => c._Title).ToList();
                data.Reverse();
                return data;
            }
        }
        public Reminder GetReminderInReminders(int Sentid)
        {
            using (var context = new DSContext())
            {
                var category = context.reminders.FirstOrDefault(c => c._Id == Sentid);
                return category;

            }
        }
        public List<Reminder> GetReminders()
        {
            using (var context = new DSContext())
            {
                var data = context.reminders
                    .Where(reminder => !reminder.IsDeleted)
                    .ToList();
                data.Reverse();
                return data;
            }
        }
        //public List<Reminder> GetReminders(string SearchTerm)
        //{
        //    using (var context = new DSContext())
        //    {
        //        return context.reminders.Where(p => p._Title != null && p._Title.ToLower()
        //                                    .Contains(SearchTerm.ToLower()))
        //                                    .OrderBy(x => x._Title)
        //                                    .ToList();
        //    }
        //}



        public Entities.Reminder GetReminderById(int id)
        {
            using (var context = new DSContext())
            {
                return context.reminders.Find(id);

            }
        }

        public void CreateReminder(Reminder Reminder)
        {
            using (var context = new DSContext())
            {
                context.reminders.Add(Reminder);
                context.SaveChanges();
            }
        }

        public void UpdateReminder(Entities.Reminder Reminder)
        {
            using (var context = new DSContext())
            {
                context.Entry(Reminder).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


        public void DeleteReminder(int ID)
        {
            using (var context = new DSContext())
            {

                var Product = context.reminders.Find(ID);
                Product.IsDeleted = true;
                Product.Type = "Reminder";
                context.Entry(Product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
