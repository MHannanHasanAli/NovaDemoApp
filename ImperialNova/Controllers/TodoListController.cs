using ImperialNova.Services;
using ImperialNova.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImperialNova.Controllers
{
    public class TodoListController : Controller
    {
        public ActionResult Index()
        {
            TodoListListingViewModel model = new TodoListListingViewModel();
            model.todolists = TodoListServices.Instance.GetTodoLists();
            return View("Index", model);
        }

       
        [HttpGet]
        public ActionResult Action(int ID = 0)
        {

            TodoListActionViewModel model = new TodoListActionViewModel();
            if (ID != 0)
            {
                var TodoList = TodoListServices.Instance.GetTodoListById(ID);
                model._Id = TodoList._Id;
                model._DueDate = TodoList._DueDate.Date;
                model._Description = TodoList._Description;
                model._Ticked = TodoList._Ticked;
                model._File = TodoList._File;

            }
            return View("Action", model);
        }


        [HttpPost]
        public ActionResult Action(TodoListActionViewModel model)
        {

            if (model._Id != 0)
            {
                var TodoList = TodoListServices.Instance.GetTodoListById(model._Id);
                TodoList._Id = model._Id;
                TodoList._DueDate = model._DueDate.Date;
                TodoList._File = model._File;
                TodoList._Description = model._Description;
                TodoList._Ticked = model._Ticked;
                TodoListServices.Instance.UpdateTodoList(TodoList);

            }
            else
            {
                var TodoList = new Entities.TodoList();
                TodoList._DueDate = model._DueDate.Date;
                TodoList._File = model._File;
                TodoList._Description = model._Description;
                TodoList._Ticked = model._Ticked;

                TodoListServices.Instance.CreateTodoList(TodoList);
            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            TodoListActionViewModel model = new TodoListActionViewModel();
            var TodoList = TodoListServices.Instance.GetTodoListById(ID);
            model._Id = TodoList._Id;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(TodoListActionViewModel model)
        {
            if (model._Id != 0)
            {
                var TodoList = TodoListServices.Instance.GetTodoListById(model._Id);

                TodoListServices.Instance.DeleteTodoList(TodoList._Id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateData(int id, bool checkboxState)
        {
            // Find the individual with the specified Id
            var individual = TodoListServices.Instance.GetTodoListById(id);

            if (individual != null)
            {
                // Update the checkbox state for the individual
                individual._Ticked = checkboxState;
                TodoListServices.Instance.UpdateTodoList(individual);
                // Handle the data as needed (for demonstration purposes, just return the updated state)
                return Json(new { Success = true, Message = $"Checkbox state updated for {individual._Description}" });
            }

            return Json(new { Success = false, Message = "Individual not found" });
        }
    }
}