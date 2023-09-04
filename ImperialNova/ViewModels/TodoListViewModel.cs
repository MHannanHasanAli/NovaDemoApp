using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImperialNova.ViewModels
{
    public class TodoListListingViewModel
    {
        public List<TodoList> todolists { get; set; }

    }

    public class TodoListActionViewModel
    {
        public int _Id { get; set; }
        public DateTime _DueDate { get; set; }
        public string _Description { get; set; }
        public string _File { get; set; }
        public bool _Ticked { get; set; }
    }
}