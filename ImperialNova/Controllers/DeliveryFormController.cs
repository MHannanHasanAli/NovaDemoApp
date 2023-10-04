using ImperialNova.Database;
using ImperialNova.Entities;
using ImperialNova.Models;
using ImperialNova.Services;
using ImperialNova.ViewModels;
using Microsoft.Build.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;

namespace ImperialNova.Controllers
{
    [Authorize]
    public class DeliveryFormController : Controller
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
       
        public DeliveryFormController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        DeliveryFormServices DeliveryFormServices = new DeliveryFormServices();
        private readonly IEmailSender _emailSender;
        private readonly DSContext _context;
        //private readonly ILogger<DeliveryFormController> _logger;
        public DeliveryFormController()
        {
            // Initialize any properties or perform necessary setup here
        }
        public DeliveryFormController(/*ILogger<DeliveryFormController> logger,*/ DSContext context, IEmailSender emailSender)
        {
            //_logger = logger;
            _context = context;
            _emailSender = emailSender;
        }
        public ActionResult Index()
        {
            Session["ACTIVER"] = "Delivery Action";

            DeliveryFormViewModel model = new DeliveryFormViewModel();
            var DeliveryForm =  DeliveryFormServices.GetDeliveryFormInfo().LastOrDefault();
            string OrderNumber = "";
            if(DeliveryForm != null)
            {
                OrderNumber = (DeliveryForm._id+1).ToString();
            }
            else
            {
                OrderNumber = "1";

            }
            model.OrderNumber = OrderNumber;
            return View(model);
        }
        public ActionResult MassDelete(List<int> ids)
        {
            foreach (var item in ids)
            {
                DeliveryFormServices.Instance.DeleteDeliveryForms(item);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Listing(string SearchTerm = "")
        {
            Session["ACTIVER"] = "Delivery Index";

            DeliveryFormViewModel model = new DeliveryFormViewModel();
            model.forms=DeliveryFormServices.GetDeliveryFormInfo(SearchTerm);
            foreach (var item in model.forms)
            {
                model.Amount = model.Amount + item._TotalAmount;
                model.Balance = model.Balance + item._AmountInBalance;
                model.Paid = model.Paid + item._AmountPaid;
            }
            return View(model);
        }

        public class SignatureConverter
        {
            public static Image ConvertSignatureToImage(byte[] signatureData)
            {
                using (MemoryStream memoryStream = new MemoryStream(signatureData))
                {
                    Image image = Image.FromStream(memoryStream);
                    return image;
                }
            }
        }
        [HttpPost]
        public JsonResult SaveSignature(string signatureData)
        {
            try
            {
                var lastentry = DeliveryFormServices.Instance.GetLastEntryId();

                Signature signature = new Signature();

                
                    signature.DeliveryFormID = lastentry;
                    signature.SignatureValue = signatureData;
                
               

                SignatureServices.Instance.CreateSignature(signature);
                // Return a success response
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an error response
                return Json(new { success = false, error = ex.Message });
            }
        }
        //static DeliveryForm data2;
        List<DeliveryFormProductsDB> _Products = new List<DeliveryFormProductsDB>();
        [HttpPost]
        public ActionResult SubmitDeliveryForm(DeliveryFormModel form, string signatureData, string productData)
        {
            var data = form;
            var productDataString = HttpUtility.UrlDecode(productData);
            var user = UserManager.FindById(User.Identity.GetUserId());
     
            var deliveryForm = new DeliveryForm()
            {

                _SlaesPerson = form._SlaesPerson,
                _Date = form._Date,
                
                _CustomerName = form._CustomerName,
                _Address = form._Address,
                _Country = form._Country,
                _ContactNo = form._ContactNo,
                _Email = form._Email,
                _SignatureData = signatureData,
                _Note = form._Note,
                _RequestedDate = form._RequestedDate,
                _TotalAmount = form._TotalAmount,
                _CashPaid = form._CashPaid,
                _CardPaid = form._CardPaid,
                _FinancePaid = form._FinancePaid,
                _FinanceCompany = form._FinanceCompany,
                _AmountPaid = form._AmountPaid,
                _AmountInBalance = form._AmountInBalance,
                _PostCode = form._PostCode,
                ProductsData = productDataString

            };
            DeliveryFormServices.CreateDeliveryForm(deliveryForm);

            var notification = new Entities.Notification();
            notification._Description = "New Delivery Form has been filled!";
            notification._UserName = user.Name;
            NotificationServices.Instance.CreateNotification(notification);

            //SendEmail(deliveryForm,_Products);
            //SendEmailCustomer(deliveryForm, _Products);
            return Json(new { success = true, _id = deliveryForm._id });

        }

       

        private bool SendEmail(DeliveryForm formdata, List<DeliveryFormProductsDB> products)
        {
            
            var userEmail = "Imperialbamboologistics@outlook.com"; // Replace with the recipient's email address
            var subject = "Delivery Form";
            var body = "Salesperson: " + formdata._SlaesPerson + "\n" +
                       "Date: " + formdata._Date + "\n" +
                       "Customer Name: " + formdata._CustomerName + "\n" +
                       "Address: " + formdata._Address + "\n" +
                       "Country: " + formdata._Country + "\n" +
                       "Contact No: " + formdata._ContactNo + "\n" +
                       "Email: " + formdata._Email + "\n" +
                       "Note: " + formdata._Note + "\n" +
                       "Requested Date: " + formdata._RequestedDate + "\n" +
                       "Total Amount: " + formdata._TotalAmount + "\n" +
                       "Payment Method: " + formdata._PayMethod + "\n" +
                       "Amount Paid: " + formdata._AmountPaid + "\n" +
                       "Amount In Balance: " + formdata._AmountInBalance + "\n";

            foreach (var product in products)
            {
                body += "Product Name: " + product._ProductName + " Quantity: " + product._ProductQuantity + "\n";
            }


            return SendEmail(userEmail, subject, body);

        }
        private bool SendEmailCustomer(DeliveryForm formdata, List<DeliveryFormProductsDB> products)
        {
            var userEmail = formdata._Email; // Customer Email
            var subject = "Delivery Form";
            var body = "Salesperson: " + formdata._SlaesPerson + "\n" +
                       "Date: " + formdata._Date + "\n" +
                       "Customer Name: " + formdata._CustomerName + "\n" +
                       "Address: " + formdata._Address + "\n" +
                       "Country: " + formdata._Country + "\n" +
                       "Contact No: " + formdata._ContactNo + "\n" +
                       "Email: " + formdata._Email + "\n" +
                       "Note: " + formdata._Note + "\n" +
                       "Requested Date: " + formdata._RequestedDate + "\n" +
                       "Total Amount: " + formdata._TotalAmount + "/-\n" +
                       "Payment Method: " + formdata._PayMethod + "/-\n" +
                       "Amount Paid: " + formdata._AmountPaid + "/-\n" +
                       "Amount In Balance: " + formdata._AmountInBalance + "\n";

            foreach (var product in products)
            {
                body += "Product Name: " + product._ProductName + " Quantity: " + product._ProductQuantity + "\n";
            }


            return SendEmail(userEmail, subject, body);


        }


        public bool SendEmail(string toEmail, string subject, string emailBody)
        {
            try
            {
                string senderEmail = "Imperialbamboologistics@outlook.com";
                string senderPassword = "Logistics1234";

                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                client.Host = "smtp.office365.com";
                client.EnableSsl = true;
                client.Port = 587;

                MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, emailBody);
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(toEmail);
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;

                client.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                //Session["EmailStatus"] = ex.ToString();
                return false;
            }

        }

        public ActionResult Invoice(int id)
        {

            var data = DeliveryFormServices.GetFormById(id);
            var model = new DeliveryFormModel();
            //if (data._SignatureData != null)
            //{
            //    var newImage = Base64ToImage();
            //    if (newImage != null)
            //    {object signatureData = data._SignatureData;
            //        string base64Image = Convert.ToBase64String(newImage);
            var signaturedata = SignatureServices.Instance.GetSignatureByDeliveryFormId(id);

           
                //}

            //}


            model._id = id;
                model._SlaesPerson = data._SlaesPerson;
                model._Date = data._Date;
                model._CustomerName = data._CustomerName;
                model._Address = data._Address;
                model._Country = data._Country;
                model._ContactNo = data._ContactNo;
                model._Email = data._Email;
                model._Note = data._Note;
                model._RequestedDate = data._RequestedDate;
                model._TotalAmount = data._TotalAmount;
                model._CashPaid = data._CashPaid;
                model._CardPaid = data._CardPaid;
                model._FinancePaid = data._FinancePaid;
                model._FinanceCompany = data._FinanceCompany;
                model._AmountPaid = data._AmountPaid;
            model._PostCode = data._PostCode;
            model._AmountInBalance = data._AmountInBalance;

            if(data.ProductsData != "[{}]")
            {
                model.Products = JsonConvert.DeserializeObject<List<ProductData>>(data.ProductsData).Where(x => x._ProductName != null).ToList();

            }
            //Yaha Products Ni arhe
            //foreach (var item in data2._Products)
            //{
            //    deliveryForm._Products.Add(new DeliveryFormProducts
            //    {
            //        _ProductName = item._ProductName,

            //        _ProductQuantity = item._ProductQuantity
            //    });
            //}
            //// Ya
            ///
            if(signaturedata.SignatureValue != null)
            {
                ViewBag.SignatureData = signaturedata.SignatureValue;
            }
            return View(model);
         
        }

       


        [HttpGet]
        public ActionResult Delete(int ID)
        {
            DeliveryFormViewModel model = new DeliveryFormViewModel();
            var DeliveryForm = DeliveryFormServices.GetDeliveryFormInfo().Where(x => x._id == ID).FirstOrDefault();
            model.ID = DeliveryForm._id;
            return PartialView("_Delete", model);
        }
        [HttpGet]
        public ActionResult TermsAndConditions()
        {          
            return PartialView("_TermsConditions");
        }
        [HttpPost]
        public ActionResult Delete(DeliveryFormViewModel model)
        {
            if (model.ID != 0)
            {
                var DeliveryForm = DeliveryFormServices.GetDeliveryFormInfo().Where(x=>x._id == model.ID).FirstOrDefault();
                var backup = new Backup();
                backup.DeletionDate = DateTime.Now;
                backup.ComponenetId = DeliveryForm._id;               
                backup.Aspect = DeliveryForm._TotalAmount.ToString();
                backup.Type = "Delivery Form";
                BackupServices.Instance.CreateBackup(backup);
                DeliveryFormServices.DeleteDeliveryForms(DeliveryForm._id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }






    
}
