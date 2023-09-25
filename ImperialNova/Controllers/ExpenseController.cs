using ClosedXML.Excel;
using ImperialNova.Entities;
using ImperialNova.Services;
using ImperialNova.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImperialNova.Controllers
{
    public class ExpenseController : Controller
    {
        // GET: Expense
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DownloadExcelFile()
        {
            var ExpenseData = new List<Expenses> { ExpenseServices.Instance.GetExpense() };
            byte[] excelBytes = GenerateExcel(ExpenseData);

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ExpenseReport.xlsx");

            Response.BinaryWrite(excelBytes);
            Response.End();

            return Content("");
        }

        private byte[] GenerateExcel(IEnumerable<Expenses> Expense)
        {
            try
            {
                DataTable dataTable = new DataTable("Expense");
                dataTable.Columns.AddRange(new DataColumn[]
                {
            new DataColumn("Attribute"),
            new DataColumn("Amount"),
                });

                foreach (var item in Expense)
                {
                    dataTable.Rows.Add("Total Sales", "£" + item._TotalSales);
                    dataTable.Rows.Add("Product Cost", "£" + item._ProductCost);
                    dataTable.Rows.Add("Van Expenses", "£" + item._VanExpenses);
                    dataTable.Rows.Add("Car(Insurance + Petrol)", "£" + item._Car);
                    dataTable.Rows.Add("Logistic", "£" + item._Logistic);
                    dataTable.Rows.Add("Storage", "£" + item._Storage);
                    dataTable.Rows.Add("Rent Shopping Center", "£" + item._Rent);
                    dataTable.Rows.Add("Sales Person", "£" + item._SalesPerson);
                    dataTable.Rows.Add("Vat 20%", "£" + item._Vat);
                    dataTable.Rows.Add("Business Rate", "£" + item._BusinessRate);
                    dataTable.Rows.Add("Utilities(Amex)", "£" + item._Utilities);
                    dataTable.Rows.Add("   ", "   ");
                    dataTable.Rows.Add("Total Expenses", "£" + item._TotalExpenses);
                    dataTable.Rows.Add("   ", "   ");
                    dataTable.Rows.Add("Left", "£" + item._Left);
                    dataTable.Rows.Add("Tax", "£" + item._Tax);
                    dataTable.Rows.Add("Bank", "£" + item._Bank);
                }

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
        [HttpPost]
        public ActionResult Action(ExpenseActionViewModel model)
        {
            if (model._Id != 0)
            {
                var Expense = ExpenseServices.Instance.GetExpenseById(model._Id);
                Expense._Id             = model._Id;
                Expense._TotalSales     = model._TotalSales;
                Expense._ProductCost    = model._ProductCost;
                Expense._VanExpenses    = model._VanExpenses;
                Expense._Car            = model._Car;
                Expense._Logistic       = model._Logistic;
                Expense._Storage        = model._Storage;
                Expense._Rent           = model._Rent;
                Expense._SalesPerson    = model._SalesPerson;
                Expense._Vat            = model._Vat;
                Expense._BusinessRate   = model._BusinessRate;
                Expense._Utilities      = model._Utilities;
                Expense._TotalExpenses  = model._TotalExpenses;
                Expense._Left           = model._Left;
                Expense._Tax            = model._Tax;
                Expense._Bank           = model._Bank;

                ExpenseServices.Instance.UpdateExpense(Expense);
            }
            else
            {
                var Expense = new Expenses();
                Expense._TotalSales = model._TotalSales;
                Expense._ProductCost = model._ProductCost;
                Expense._VanExpenses = model._VanExpenses;
                Expense._Car = model._Car;
                Expense._Logistic = model._Logistic;
                Expense._Storage = model._Storage;
                Expense._Rent = model._Rent;
                Expense._SalesPerson = model._SalesPerson;
                Expense._Vat = model._Vat;
                Expense._BusinessRate = model._BusinessRate;
                Expense._Utilities = model._Utilities;
                Expense._TotalExpenses = model._TotalExpenses;
                Expense._Left = model._Left;
                Expense._Tax = model._Tax;
                Expense._Bank = model._Bank;

                ExpenseServices.Instance.CreateExpense(Expense);

            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {

            ExpenseActionViewModel model = new ExpenseActionViewModel();
            var Expense = ExpenseServices.Instance.GetExpense();
            if (Expense == null)
            {
                decimal price = 0;
                decimal cost = 0;
                decimal profit = 0;
                var ordersproducts = OrderProductServices.Instance.GetOrderProducts();
                foreach (var item in ordersproducts)
                {
                    price = price + (item._Price * item._Qty);
                    cost = cost + (item._Cost * item._Qty);
                    profit = profit + ((item._Price - item._Cost) * item._Qty);
                }
                decimal percentage = 0.2M;
                model._TotalSales = price;
                model._ProductCost = cost;
                model._Vat = percentage * price;
                return View("Action", model);
            }
            else
            {
                decimal percentage = 0.2M;
                decimal price = 0;
                decimal cost = 0;
                //decimal profit = 0;
                var ordersproducts = OrderProductServices.Instance.GetOrderProducts();
                foreach (var item in ordersproducts)
                {
                    price = price + (item._Price * item._Qty);
                    cost = cost + (item._Cost * item._Qty);
                    //profit = profit + ((item._Price - item._Cost) * item._Qty);
                }
                model._Id = Expense._Id;
                model._TotalSales = price;
                model._ProductCost = cost;
                model._VanExpenses = Expense._VanExpenses;
                model._Car = Expense._Car;
                model._Logistic = Expense._Logistic;
                model._Storage = Expense._Storage;
                model._Rent = Expense._Rent;
                model._SalesPerson = Expense._SalesPerson;
                model._Vat = percentage * price;
                model._BusinessRate = Expense._BusinessRate;
                model._Utilities = Expense._Utilities;
                model._TotalExpenses = Expense._TotalExpenses;
                model._Left = Expense._Left;
                model._Tax = Expense._Tax;
                model._Bank = Expense._Bank;

            }


            return View("Action", model);
        }
    }
}