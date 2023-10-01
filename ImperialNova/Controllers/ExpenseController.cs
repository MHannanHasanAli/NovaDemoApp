using Antlr.Runtime;
using ClosedXML.Excel;
using ImperialNova.Entities;
using ImperialNova.Services;
using ImperialNova.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Finance.Implementations;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImperialNova.Controllers
{
    [Authorize]
    public class ExpenseController : Controller
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
        public ExpenseController()
        {
        }
        public ExpenseController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ActionResult Index(string SearchTerm = "")
        {
            Session["ACTIVER"] = "Expense Index";

            ExpenseListingViewModel model = new ExpenseListingViewModel();
            model.expense = ExpenseServices.Instance.GetExpenses();
            foreach (var item in model.expense)
            {
                model.ExpensesSum = model.ExpensesSum + item._TotalExpenses;
            }
            return View("Index", model);
        }
        //public ActionResult FetchTotalSales(DateTime date)
        //{

        //    var sorted = new List<OrderProduct>();
        //    var sortedorderId = new List<int>();

        //    UpdateExpense model = new UpdateExpense();

        //    var order = OrderServices.Instance.GetOrders();
        //    var orderproducts = OrderProductServices.Instance.GetOrderProducts();

        //    foreach (var item in order)
        //    {
        //        if (item._Date == date)
        //        {
        //            sortedorderId.Add(item._Id);
        //        }
        //    }

        //    decimal percentage = 0.2M;

        //    foreach (var item in sortedorderId)
        //    {
        //        var products = OrderProductServices.Instance.GetOrderProductsByOrderId(item);
        //        foreach (var pro in products)
        //        {
        //            model.price = model.price + (pro._Price * pro._Qty);
        //            model.cost = model.cost + (pro._Cost * pro._Qty);
        //        }
        //    }
        //    //model.vat = percentage * model.price;

        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult MassDelete(List<int> ids)
        {
            foreach (var item in ids)
            {
                ExpenseServices.Instance.DeleteExpense(item);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult Action(int ID = 0)
        {
            if (ID != 0)
            {
                Session["ACTIVER"] = "Expense Edit";

            }
            else
            {
                Session["ACTIVER"] = "Expense Action";

            }
            ExpenseActionViewModel model = new ExpenseActionViewModel();
            if (ID != 0)
            {
                //decimal percentage = 0.2M;
                decimal price = 0;
                //decimal cost = 0;
                //decimal profit = 0;
                var ordersproducts = OrderProductServices.Instance.GetOrderProducts();
                foreach (var item in ordersproducts)
                {
                    price = price + (item._Price * item._Qty);
                    //cost = cost + (item._Cost * item._Qty);
                    //profit = profit + ((item._Price - item._Cost) * item._Qty);
                }
                var Expense = ExpenseServices.Instance.GetExpenseById(ID);
                model._Id = Expense._Id;
                //model._TotalSales = price;
                //model._ProductCost = cost;
                //model._VanExpenses = Expense._VanExpenses;
                //model._Car = Expense._Car;
                //model._Logistic = Expense._Logistic;
                //model._Storage = Expense._Storage;
                //model._Rent = Expense._Rent;
                //model._SalesPerson = Expense._SalesPerson;
                //model._Vat = percentage * price;
                //model._BusinessRate = Expense._BusinessRate;
                //model._Utilities = Expense._Utilities;
                //model._Left = Expense._Left;
                //model._Tax = Expense._Tax;
                //model._Bank = Expense._Bank;
                model._Title = Expense._Title;
                model._Date = Expense._Date;
                model._TotalExpenses = Expense._TotalExpenses;

            }
            return View("Action", model);
        }


        [HttpPost]
        public ActionResult Action(ExpenseActionViewModel model)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (model._Id != 0)
            {
                var Expense = ExpenseServices.Instance.GetExpenseById(model._Id);
                Expense._Id = model._Id;
                //Expense._TotalSales = model._TotalSales;
                //Expense._ProductCost = model._ProductCost;
                //Expense._VanExpenses = model._VanExpenses;
                //Expense._Car = model._Car;
                //Expense._Logistic = model._Logistic;
                //Expense._Storage = model._Storage;
                //Expense._Rent = model._Rent;
                //Expense._SalesPerson = model._SalesPerson;
                //Expense._Vat = model._Vat;
                //Expense._BusinessRate = model._BusinessRate;
                //Expense._Utilities = model._Utilities;
                //Expense._Left = model._Left;
                //Expense._Tax = model._Tax;
                //Expense._Bank = model._Bank;
                Expense._Date = model._Date;
                Expense._Title = model._Title;
                Expense._TotalExpenses = model._TotalExpenses;


                ExpenseServices.Instance.UpdateExpense(Expense);

            }
            else
            {
                var Expense = new Expenses();
                //Expense._TotalSales = model._TotalSales;
                //Expense._ProductCost = model._ProductCost;
                //Expense._VanExpenses = model._VanExpenses;
                //Expense._Car = model._Car;
                //Expense._Logistic = model._Logistic;
                //Expense._Storage = model._Storage;
                //Expense._Rent = model._Rent;
                //Expense._SalesPerson = model._SalesPerson;
                //Expense._Vat = model._Vat;
                //Expense._BusinessRate = model._BusinessRate;
                //Expense._Utilities = model._Utilities;
                //Expense._Left = model._Left;
                //Expense._Tax = model._Tax;
                //Expense._Bank = model._Bank;
                Expense._Date = model._Date;
                Expense._Title = model._Title;
                Expense._TotalExpenses = model._TotalExpenses;


                ExpenseServices.Instance.CreateExpense(Expense);

                var notification = new Entities.Notification();
                notification._Description = "New Expense has been Added!";
                notification._UserName = user.Name;
                NotificationServices.Instance.CreateNotification(notification);
            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GenerateReport()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            ExpenseActionViewModel model = new ExpenseActionViewModel();
            var Expense = ExpenseServices.Instance.GetExpenseById(ID);
            model._Id = Expense._Id;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(ExpenseActionViewModel model)
        {
            if (model._Id != 0)
            {
                var Expense = ExpenseServices.Instance.GetExpenseById(model._Id);

                ExpenseServices.Instance.DeleteExpense(Expense._Id);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        // GET: Expense
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult DownloadExcelFile(DateTime start,DateTime end)
        {
            var orderlist = new List<int>();
            var orders = OrderServices.Instance.GetOrders();
            foreach (var item in orders)
            {
                if(item._Date >= start && item._Date <= end)
                {
                    orderlist.Add(item._Id);
                }
            }

            var products = new List<OrderProduct>();
            foreach (var item in orderlist)
            {
               var productset = OrderProductServices.Instance.GetOrderProductsByOrderId(item);
                foreach (var product in productset)
                {
                    products.Add(product);
                }
            }
            UpdateExpense model = new UpdateExpense();
            foreach (var item in products)
            {
                model.price = model.price + item._Price;
                model.cost = model.cost + item._Cost;
            }

            List<Expenses> Expenses = new List<Expenses>();
            var ExpenseData = ExpenseServices.Instance.GetExpenses().ToList();

            foreach (var item in ExpenseData)
            {
                if(item._Date >= start && item._Date <= end)
                {
                    model.expenses = model.expenses + item._TotalExpenses;
                    Expenses.Add(item);
                }
            }
            //IEnumerable<Expenses> expensesEnumerable = Expenses;
            model.profit = model.price-(model.cost+model.expenses);
            model.startdate = start; 
            model.enddate = end;

            byte[] excelBytes = GenerateExcel(ExpenseData, model);

          Response.Clear();
Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
Response.AddHeader("content-disposition", "attachment;filename=ExpenseReport.xlsx");

Response.BinaryWrite(excelBytes);
Response.End();

            return Content("");
        }

        private byte[] GenerateExcel(IEnumerable<Expenses> Expense, UpdateExpense model)
        {
            try
            {
                DataTable dataTable = new DataTable("Expense");
                dataTable.Columns.AddRange(new DataColumn[]
                {
            new DataColumn("Attribute"),
            new DataColumn("Amount"),
                });
                dataTable.Rows.Add("   ", "   ");
                dataTable.Rows.Add("From", model.startdate);
                dataTable.Rows.Add("To", model.enddate);
                dataTable.Rows.Add("   ", "   ");
                dataTable.Rows.Add("Total Sales", model.price);
                dataTable.Rows.Add("Total Product Cost", model.cost);
                dataTable.Rows.Add("   ", "   ");
                foreach (var item in Expense)
                {
                    dataTable.Rows.Add(item._Title, item._TotalExpenses);
                   
                }
                dataTable.Rows.Add("   ", "   ");
                dataTable.Rows.Add("Profit",model.profit);


                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dataTable);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return stream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //[HttpPost]
        //public ActionResult Action(ExpenseActionViewModel model)
        //{
        //    if (model._Id != 0)
        //    {
        //        var Expense = ExpenseServices.Instance.GetExpenseById(model._Id);
        //        Expense._Id             = model._Id;
        //        Expense._TotalSales     = model._TotalSales;
        //        Expense._ProductCost    = model._ProductCost;
        //        Expense._VanExpenses    = model._VanExpenses;
        //        Expense._Car            = model._Car;
        //        Expense._Logistic       = model._Logistic;
        //        Expense._Storage        = model._Storage;
        //        Expense._Rent           = model._Rent;
        //        Expense._SalesPerson    = model._SalesPerson;
        //        Expense._Vat            = model._Vat;
        //        Expense._BusinessRate   = model._BusinessRate;
        //        Expense._Utilities      = model._Utilities;
        //        Expense._TotalExpenses  = model._TotalExpenses;
        //        Expense._Left           = model._Left;
        //        Expense._Tax            = model._Tax;
        //        Expense._Bank           = model._Bank;

        //        ExpenseServices.Instance.UpdateExpense(Expense);
        //    }
        //    else
        //    {
        //        var Expense = new Expenses();
        //        Expense._TotalSales = model._TotalSales;
        //        Expense._ProductCost = model._ProductCost;
        //        Expense._VanExpenses = model._VanExpenses;
        //        Expense._Car = model._Car;
        //        Expense._Logistic = model._Logistic;
        //        Expense._Storage = model._Storage;
        //        Expense._Rent = model._Rent;
        //        Expense._SalesPerson = model._SalesPerson;
        //        Expense._Vat = model._Vat;
        //        Expense._BusinessRate = model._BusinessRate;
        //        Expense._Utilities = model._Utilities;
        //        Expense._TotalExpenses = model._TotalExpenses;
        //        Expense._Left = model._Left;
        //        Expense._Tax = model._Tax;
        //        Expense._Bank = model._Bank;

        //        ExpenseServices.Instance.CreateExpense(Expense);

        //    }
        //    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        //}


        //[HttpGet]
        //public ActionResult Action(int ID = 0)
        //{

        //    ExpenseActionViewModel model = new ExpenseActionViewModel();
        //    var Expense = ExpenseServices.Instance.GetExpense();
        //    if (Expense == null)
        //    {
        //        decimal price = 0;
        //        decimal cost = 0;
        //        decimal profit = 0;
        //        var ordersproducts = OrderProductServices.Instance.GetOrderProducts();
        //        foreach (var item in ordersproducts)
        //        {
        //            price = price + (item._Price * item._Qty);
        //            cost = cost + (item._Cost * item._Qty);
        //            profit = profit + ((item._Price - item._Cost) * item._Qty);
        //        }
        //        decimal percentage = 0.2M;
        //        model._TotalSales = price;
        //        model._ProductCost = cost;
        //        model._Vat = percentage * price;
        //        return View("Action", model);
        //    }
        //    else
        //    {
        //        decimal percentage = 0.2M;
        //        decimal price = 0;
        //        decimal cost = 0;
        //        //decimal profit = 0;
        //        var ordersproducts = OrderProductServices.Instance.GetOrderProducts();
        //        foreach (var item in ordersproducts)
        //        {
        //            price = price + (item._Price * item._Qty);
        //            cost = cost + (item._Cost * item._Qty);
        //            //profit = profit + ((item._Price - item._Cost) * item._Qty);
        //        }
        //        model._Id = Expense._Id;
        //        model._TotalSales = price;
        //        model._ProductCost = cost;
        //        model._VanExpenses = Expense._VanExpenses;
        //        model._Car = Expense._Car;
        //        model._Logistic = Expense._Logistic;
        //        model._Storage = Expense._Storage;
        //        model._Rent = Expense._Rent;
        //        model._SalesPerson = Expense._SalesPerson;
        //        model._Vat = percentage * price;
        //        model._BusinessRate = Expense._BusinessRate;
        //        model._Utilities = Expense._Utilities;
        //        model._TotalExpenses = Expense._TotalExpenses;
        //        model._Left = Expense._Left;
        //        model._Tax = Expense._Tax;
        //        model._Bank = Expense._Bank;

        //    }


        //    return View("Action", model);
        //}
    }
}