//using ImperialNova.Services;
//using ImperialNova.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace ImperialNova.Controllers
//{
//    public class TodoListController : Controller
//    {
//        // GET: TodoList
//        public ActionResult Index()
//        {
//            TodoListListingViewModel model = new TodoListListingViewModel();
//            model.todolists = TodoListServices.Instance.GetTodoLists();
//            return View("Index", model);
//        }


//        [HttpGet]
//        public ActionResult Action(int ID = 0)
//        {

//            TodoListActionViewModel model = new TodoListActionViewModel();
//            if (ID != 0)
//            {
//                var TodoList = TodoListServices.Instance.GetTodoListById(ID);
//                model._Id = TodoList._Id;
//                model._CName = TodoList._CName;
//                model._Description = TodoList._Description;

//            }
//            return View("Action", model);
//        }


//        [HttpPost]
//        public ActionResult Action(TodoListActionViewModel model)
//        {

//            if (model._Id != 0)
//            {
//                var TodoList = TodoListServices.Instance.GetTodoListById(model._Id);
//                TodoList._Id = model._Id;
//                TodoList._CName = model._CName;
//                TodoList._Description = model._Description;

//                TodoListServices.Instance.UpdateTodoList(TodoList);

//            }
//            else
//            {
//                var TodoList = new TodoList();
//                TodoList._CName = model._CName;
//                TodoList._Description = model._Description;

//                TodoListServices.Instance.CreateTodoList(TodoList);
//            }


//            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
//        }



//        [HttpGet]
//        public ActionResult Delete(int ID)
//        {
//            TodoListActionViewModel model = new TodoListActionViewModel();
//            var TodoList = TodoListServices.Instance.GetTodoListById(ID);
//            model._Id = TodoList._Id;
//            return View("Delete", model);
//        }

//        [HttpPost]
//        public ActionResult Delete(TodoListActionViewModel model)
//        {
//            if (model._Id != 0)
//            {
//                var TodoList = TodoListServices.Instance.GetTodoListById(model._Id);

//                TodoListServices.Instance.DeleteTodoList(TodoList._Id);
//            }

//            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
//        }
//    }
//}