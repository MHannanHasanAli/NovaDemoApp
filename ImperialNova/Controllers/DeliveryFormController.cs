﻿using ImperialNova.Database;
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

namespace ImperialNova.Controllers
{
    [Authorize]
    public class DeliveryFormController : Controller
    {
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
        public ActionResult Listing(string SearchTerm = "")
        {
            Session["ACTIVER"] = "Delivery Index";

            DeliveryFormViewModel model = new DeliveryFormViewModel();
            model.forms=DeliveryFormServices.GetDeliveryFormInfo(SearchTerm);
            return View(model);
        }

        
        static DeliveryForm data2;
        List<DeliveryFormProductsDB> _Products = new List<DeliveryFormProductsDB>();
        [HttpPost]
        public ActionResult SubmitDeliveryForm(DeliveryFormModel form, string signatureData, string productData)
        {
            var data = form;
            var productDataString = HttpUtility.UrlDecode(productData);

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
                ProductsData = productDataString

            };
            DeliveryFormServices.CreateDeliveryForm(deliveryForm);

            var notification = new Entities.Notification();
            notification._Description = "New Delivery Form has been filled!";
            NotificationServices.Instance.CreateNotification(notification);

            //SendEmail(deliveryForm,_Products);
            //SendEmailCustomer(deliveryForm, _Products);
            return Json(new { success = true, _id = deliveryForm._id });

        }

        [HttpPost]
        public byte[] Base64ToImage(string source)
        {
            string base64 = source.Substring(source.IndexOf(',') + 1);
            int mod4 = base64.Length % 4;
            if (mod4 > 0)
            {
                // Add padding characters to make it a valid length
                base64 += new string('=', 4 - mod4);
                byte[] chartData = Convert.FromBase64String(base64);
                return chartData;

            }
            return null;

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
            if (data._SignatureData != null)
            {
                var newImage = Base64ToImage(data._SignatureData);
                if (newImage != null)
                {
                    string base64Image = Convert.ToBase64String(newImage);
                    model._SignatureData = base64Image;
                }

            }


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
            model._AmountInBalance = data._AmountInBalance;

            
            model.Products = JsonConvert.DeserializeObject<List<ProductData>>(data.ProductsData).Where(x=>x._ProductName != null).ToList();
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

        [HttpPost]
        public ActionResult Delete(DeliveryFormViewModel model)
        {
            if (model.ID != 0)
            {
                var DeliveryForm = DeliveryFormServices.GetDeliveryFormInfo().Where(x=>x._id == model.ID).FirstOrDefault();

                DeliveryFormServices.DeleteDeliveryForms(DeliveryForm._id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }






    
}
