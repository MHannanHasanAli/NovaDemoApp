using ImperialNova.Database;
using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperialNova.Services
{
    public class PaymentServices
    {

        #region Singleton
        public static PaymentServices Instance
        {
            get
            {
                if (instance == null) instance = new PaymentServices();
                return instance;
            }
        }
        private static PaymentServices instance { get; set; }
        private PaymentServices()
        {
        }
        #endregion


        public List<Payment> GetPayment(string SearchTerm = "")
        {
            using (var context = new DSContext())
            {
                if (SearchTerm != "")
                {
                    return context.payments.Where(p => p._Individual != null && p._Individual.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x._Individual)
                                            .ToList();
                }
                else
                {
                    return context.payments.OrderBy(x => x._Individual).ToList();
                }
            }
        }
        public List<Payment> GetPayments()
        {
            using (var context = new DSContext())
            {
                var data = context.payments
                    .Where(payment => !payment.IsDeleted)
                    .OrderBy(payment => payment._Individual)
                    .ToList();

                return data;
            }
        }

        public Payment GetPaymentById(int id)
        {
            using (var context = new DSContext())
            {
                return context.payments.Find(id);

            }
        }

        public void CreatePayment(Payment Payment)
        {
            using (var context = new DSContext())
            {
                context.payments.Add(Payment);
                context.SaveChanges();
            }
        }

        public void UpdatePayment(Payment Payment)
        {
            using (var context = new DSContext())
            {
                context.Entry(Payment).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeletePayment(int ID)
        {
            using (var context = new DSContext())
            {

                var Payment = context.payments.Find(ID);
                Payment.IsDeleted = true;
                Payment.Type = "Payment";
                context.Entry(Payment).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
