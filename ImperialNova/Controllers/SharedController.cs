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
                var fileNameWithDate = DateTime.Now.ToString("dd-MM-yyyy") + file.FileName;
                var fileName = Path.GetFileNameWithoutExtension(file.FileName); // Get the file name without extension
                var fileExtension = Path.GetExtension(file.FileName);
                var fileSize = file.ContentLength / 1024; // File size in kilobytes

                var directoryPath = Server.MapPath("~/Photos/");
                var path = Path.Combine(directoryPath, fileNameWithDate);

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath); // Create the directory if it doesn't exist
                }
                file.SaveAs(path);

                result.Data = new { Success = true, FileName = fileName, FileSizeKB = fileSize, DocURL = string.Format("/Images/{0}", fileNameWithDate) };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
                throw;
            }
            return result;
        }

        public FileResult DownloadFile(string path)
        {
            if (System.IO.File.Exists(Server.MapPath(path)))
            {
                // Determine the content type (MIME type) of the file
                string contentType = MimeMapping.GetMimeMapping(Server.MapPath(path));

                // Read the file data into a byte array
                byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(path));

                // Create a FileContentResult to send the file to the client
                return File(fileBytes, contentType, Path.GetFileName(Server.MapPath(path)));
            }
            else
            {
                // Return a not found response if the file doesn't exist
                return null;
            }
        }
    }
}