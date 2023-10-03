using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.ExtendedProperties;
using ImperialNova.Entities;
using ImperialNova.Services;
using ImperialNova.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImperialNova.Controllers
{

    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private AMSignInManager _signInManager;
        private AMUserManager _userManager;
        public AMSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<AMSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public AMUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AMUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private AMRolesManager _rolesManager;
        public AMRolesManager RolesManager
        {
            get
            {
                return _rolesManager ?? HttpContext.GetOwinContext().GetUserManager<AMRolesManager>();
            }
            private set
            {
                _rolesManager = value;
            }
        }
        public CategoryController()
        {
        }
        public CategoryController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        //CategoryServices CategoryServices = new CategoryServices();
        public ActionResult Index(string SearchTerm = "")
        {
            Session["ACTIVER"] = "Category Index";
            CategoryListingViewModel model = new CategoryListingViewModel();
            model.SearchTerm = SearchTerm;
            model.Categories = CategoryServices.Instance.GetCategory(SearchTerm);
            return View("Index", model);
        }
        public ActionResult MassDelete(List<int> ids)
        {
            foreach (var item in ids)
            {
                CategoryServices.Instance.DeleteCategory(item);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            if (ID != 0)
            {
                Session["ACTIVER"] = "Category Edit";

            }
            else
            {
                Session["ACTIVER"] = "Category Action";

            }

            CategoryActionViewModel model = new CategoryActionViewModel();
            if (ID != 0)
            {
                var Category = CategoryServices.Instance.GetCategoryById(ID);
                model._Id = Category._Id;
                model._CName = Category._CName;
                model._Description = Category._Description;
                
            }
            return View("Action", model);
        }


        [HttpPost]
        public ActionResult Action(CategoryActionViewModel model)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            if (model._Id != 0)
            {
                var Category = CategoryServices.Instance.GetCategoryById(model._Id);
                Category._Id = model._Id;
                Category._CName = model._CName;
                Category._Description = model._Description;
               
                CategoryServices.Instance.UpdateCategory(Category);

            }
            else
            {
                var Category = new Category();
                Category._CName = model._CName;
                Category._Description = model._Description;
               
                CategoryServices.Instance.CreateCategory(Category);

                var notification = new Entities.Notification();
                notification._Description = "New Category has been made!";
                notification._UserName = user.Name;
                NotificationServices.Instance.CreateNotification(notification);
            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            Session["ACTIVER"] = "Category Edit";
            CategoryActionViewModel model = new CategoryActionViewModel();
            var Category = CategoryServices.Instance.GetCategoryById(ID);
            model._Id = Category._Id;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CategoryActionViewModel model)
        {
            if (model._Id != 0)
            {
                var Category = CategoryServices.Instance.GetCategoryById(model._Id);
                var backup = new Backup();
                backup.DeletionDate = DateTime.Now;
                backup.ComponenetId = Category._Id;
                backup.Aspect = Category._CName;
                backup.Type = "Category";
                BackupServices.Instance.CreateBackup(backup);
                CategoryServices.Instance.DeleteCategory(Category._Id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}
