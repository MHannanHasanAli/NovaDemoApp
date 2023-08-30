using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImperialNova.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared


        public JsonResult UploadPhotos()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var file = Request.Files[0];

                var fileName = DateTime.Now.ToString("dd-MM-yyyy") + file.FileName;

                var path = Path.Combine(Server.MapPath("~/Photos/"), fileName);
                file.SaveAs(path);
                result.Data = new { Success = true, DocURL = string.Format("/Photos/{0}", fileName) };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
                throw;
            }
            return result;
        }

    }
}