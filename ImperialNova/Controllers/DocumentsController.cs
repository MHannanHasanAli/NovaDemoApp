using ImperialNova.Services;
using ImperialNova.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ImperialNova.Controllers
{
    [Authorize]
    public class DocumentsController : Controller
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
        public DocumentsController()
        {
        }
        public DocumentsController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ActionResult Index()
        {
            Session["ACTIVER"] = "Document Index";

            DocumentsListingViewModel model = new DocumentsListingViewModel();
            model.documents = DocumentServices.Instance.GetDocuments();
            return View("Index", model);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            if (ID != 0)
            {
                Session["ACTIVER"] = "Document Edit";

            }
            else
            {
                Session["ACTIVER"] = "Document Action";

            }
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
        public ActionResult MassDelete(List<int> ids)
        {
            foreach (var item in ids)
            {
                DocumentServices.Instance.DeleteDocument(item);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Action(DocumentsActionViewModel model)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
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
                var notification = new Entities.Notification();
                notification._Description = "New Document has been Added!";
                notification._UserName = user.Name;
                NotificationServices.Instance.CreateNotification(notification);
            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            DocumentsActionViewModel model = new DocumentsActionViewModel();
            var Document = DocumentServices.Instance.GetDocumentById(ID);
            model._Id = Document._Id;
            return PartialView("_Delete", model);
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