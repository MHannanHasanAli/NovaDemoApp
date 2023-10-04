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
    public class SignatureServices
    {
        #region Singleton
        public static SignatureServices Instance
        {
            get
            {
                if (instance == null) instance = new SignatureServices();
                return instance;
            }
        }
        private static SignatureServices instance { get; set; }
        private SignatureServices()
        {
        }
        #endregion


  
       
        public List<Signature> GetSignatures()
        {
            using (var context = new DSContext())
            {
                var data = context.signatures
                    .ToList();

                data.Reverse();
                return data;
            }
        }
        public Entities.Signature GetSignatureByDeliveryFormId(int deliveryFormId)
        {
            using (var context = new DSContext())
            {
                return context.signatures.FirstOrDefault(s => s.DeliveryFormID == deliveryFormId);
            }
        }
        public Entities.Signature GetSignatureById(int id)
        {
            using (var context = new DSContext())
            {
                return context.signatures.Find(id);

            }
        }

        public void CreateSignature(Signature Signature)
        {
            using (var context = new DSContext())
            {
                context.signatures.Add(Signature);
                context.SaveChanges();
            }
        }

     
    }
}
