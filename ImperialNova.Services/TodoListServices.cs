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
    public class TodoListServices
    {
        #region Singleton
        public static TodoListServices Instance
        {
            get
            {
                if (instance == null) instance = new TodoListServices();
                return instance;
            }
        }
        private static TodoListServices instance { get; set; }
        private TodoListServices()
        {
        }
        #endregion


        
        public List<TodoList> GetTodoLists()
        {
            using (var context = new DSContext())
            {
                var data = context.todolists
                    .Where(todoList => !todoList.IsDeleted)
                    .OrderBy(todoList => todoList._Id)
                    .ToList();

                return data;
            }
        }
        public List<TodoList> GetFilteredTodoLists()
        {
            using (var context = new DSContext())
            {
                return context.todolists
                    .Where(x => x._Ticked == false)
                    .OrderBy(x => x._DueDate) // Order by the Date property
                    .ToList();
            }
        }
        public TodoList GetTodoListById(int id)
        {
            using (var context = new DSContext())
            {
                return context.todolists.Find(id);

            }
        }

        public void CreateTodoList(TodoList TodoList)
        {
            using (var context = new DSContext())
            {
                context.todolists.Add(TodoList);
                context.SaveChanges();
            }
        }

        public void UpdateTodoList(TodoList TodoList)
        {
            using (var context = new DSContext())
            {
                context.Entry(TodoList).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteTodoList(int ID)
        {
            using (var context = new DSContext())
            {

                var TodoList = context.todolists.Find(ID);
                TodoList.IsDeleted = true;
                TodoList.Type = "Todo List";
                context.Entry(TodoList).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
