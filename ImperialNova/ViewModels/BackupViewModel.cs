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
}