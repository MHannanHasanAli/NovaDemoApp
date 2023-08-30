using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ImperialNova.ViewModels
{
    public class CategoryListingViewModel
    {
        public List<Category> Categories { get; set; }
        public string SearchTerm { get; set; }
    }
    public class CategoryActionViewModel
    {
        public int _Id { get; set; }
        public string _CName { get; set; }
        public string _Description { get; set; }

    }
}