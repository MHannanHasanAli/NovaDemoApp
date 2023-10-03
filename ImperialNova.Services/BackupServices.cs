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
    public class BackupServices
    {
        #region Singleton
        public static BackupServices Instance
        {
            get
            {
                if (instance == null) instance = new BackupServices();
                return instance;
            }
        }
        private static BackupServices instance { get; set; }
        private BackupServices()
        {
        }
        #endregion
        public List<Backup> GetBackup(string SearchTerm = "")
        {
            using (var context = new DSContext())
            {
                if (SearchTerm != "")
                {
                    return context.backups.Where(p => p.Type != null && p.Type.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x.Type)
                                            .ToList();
                }
                else
                {
                    return context.backups.OrderBy(x => x.Type).ToList();
                }
            }
        }
        public List<string> GetBackupNames()
        {
            using (var context = new DSContext())
            {
                var data = context.backups.Select(c => c.Type).ToList();
                data.Reverse();
                return data;
            }
        }
        public Backup GetBackupInBackups(int Sentid)
        {
            using (var context = new DSContext())
            {
                var category = context.backups.FirstOrDefault(c => c._Id == Sentid);
                return category;

            }
        }
        public List<Backup> GetBackups()
        {
            using (var context = new DSContext())
            {
                var data = context.backups.ToList();
                data.Reverse();
                return data;
            }
        }
        public List<Backup> GetBackups(string SearchTerm)
        {
            using (var context = new DSContext())
            {
                return context.backups.Where(p => p.Type != null && p.Type.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x.Type)
                                            .ToList();
            }
        }



        public Entities.Backup GetBackupById(int id)
        {
            using (var context = new DSContext())
            {
                return context.backups.Find(id);

            }
        }

        public void CreateBackup(Backup Backup)
        {
            using (var context = new DSContext())
            {
                context.backups.Add(Backup);
                context.SaveChanges();
            }
        }

        public void UpdateBackup(Entities.Backup Backup)
        {
            using (var context = new DSContext())
            {
                context.Entry(Backup).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


        public void DeleteBackup(int ID)
        {
            using (var context = new DSContext())
            {

                var Product = context.backups.Find(ID);
                context.backups.Remove(Product);
                context.SaveChanges();
            }
        }
    }
}
