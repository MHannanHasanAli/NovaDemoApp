using ImperialNova.Services;
using ImperialNova.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImperialNova.Controllers
{
    public class DocumentsController : Controller
    {
        public ActionResult Index()
        {
            DocumentsListingViewModel model = new DocumentsListingViewModel();
            model.documents = DocumentServices.Instance.GetDocuments();
            return View("Index", model);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {

            DocumentsActionViewModel model = new DocumentsActionViewModel();
            if (ID != 0)
            {
                var Document = DocumentServices.Instance.GetDocumentById(ID);
                model._Id = Document._Id;
                model._Name = Document._Name;
                model._File = Document._File;

            }
            return View("Action", model);
        }


        [HttpPost]
        public ActionResult Action(DocumentsActionViewModel model)
        {

            if (model._Id != 0)
            {
                var Document = DocumentServices.Instance.GetDocumentById(model._Id);
                Document._Id = model._Id;
                Document._Name = model._Name;
                Document._File = model._File;

                DocumentServices.Instance.UpdateDocument(Document);

            }
            else
            {
                var Document = new Entities.Document();
                Document._Name = model._Name;
                Document._File = model._File;

                DocumentServices.Instance.CreateDocument(Document);
            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            DocumentsActionViewModel model = new DocumentsActionViewModel();
            var Document = DocumentServices.Instance.GetDocumentById(ID);
            model._Id = Document._Id;
            return View("Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(DocumentsActionViewModel model)
        {
            if (model._Id != 0)
            {
                var Document = DocumentServices.Instance.GetDocumentById(model._Id);

                DocumentServices.Instance.DeleteDocument(Document._Id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}