using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.ExtendedProperties;
using ImperialNova.Entities;
using ImperialNova.Services;
using ImperialNova.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ImperialNova.Controllers
{

    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        //CategoryServices CategoryServices = new CategoryServices();
        public ActionResult Index(string SearchTerm = "")
        {
            CategoryListingViewModel model = new CategoryListingViewModel();
            model.SearchTerm = SearchTerm;
            model.Categories = CategoryServices.Instance.GetCategory(SearchTerm);
            return View("Index", model);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {

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
            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
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

                CategoryServices.Instance.DeleteCategory(Category._Id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}
