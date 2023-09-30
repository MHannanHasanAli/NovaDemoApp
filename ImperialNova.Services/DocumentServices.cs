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
    public class DocumentServices
    {
        #region Singleton
        public static DocumentServices Instance
        {
            get
            {
                if (instance == null) instance = new DocumentServices();
                return instance;
            }
        }
        private static DocumentServices instance { get; set; }
        private DocumentServices()
        {
        }
        #endregion
       
 
        public List<Document> GetDocuments()
        {
            using (var context = new DSContext())
            {
                var data = context.documents
                    .Where(document => !document.IsDeleted)
                    .ToList();

                data.Reverse();
                return data;
            }
        }

        public Entities.Document GetDocumentById(int id)
        {
            using (var context = new DSContext())
            {
                return context.documents.Find(id);

            }
        }

        public void CreateDocument(Document Document)
        {
            using (var context = new DSContext())
            {
                context.documents.Add(Document);
                context.SaveChanges();
            }
        }

        public void UpdateDocument(Entities.Document Document)
        {
            using (var context = new DSContext())
            {
                context.Entry(Document).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


        public void DeleteDocument(int ID)
        {
            using (var context = new DSContext())
            {

                var Product = context.documents.Find(ID);
                Product.IsDeleted = true;
                Product.Type = "Document";
                context.Entry(Product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
