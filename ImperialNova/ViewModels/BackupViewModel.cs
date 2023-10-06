using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImperialNova.ViewModels
{
    public class BackupViewModel
    {
    }
    public class BackupListingViewModel
    {
        public List<Backup> Backups { get; set; }
    }
    public class BackupActionViewModel
    {
        public int _Id { get; set; }
        public int ComponenetId { get; set; }
        public string Aspect { get; set; }
        public string Type { get; set; }
        public DateTime DeletionDate { get; set; }
    }
}