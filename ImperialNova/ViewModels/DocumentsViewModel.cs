using ImperialNova.Entities;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImperialNova.ViewModels
{
    public class DocumentsListingViewModel
    {
        public List<Entities.Document> documents {  get; set; }
    }

    public class DocumentsActionViewModel
    {
        public int _Id { get; set; }
        public string _Name { get; set; }
        public string _File { get; set; }
    }
}