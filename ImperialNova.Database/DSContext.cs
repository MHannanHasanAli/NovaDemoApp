using Microsoft.AspNet.Identity.EntityFramework;
using ImperialNova.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using System.Reflection.Emit;

namespace ImperialNova.Database
{
    public class DSContext :IdentityDbContext<User>,IDisposable
    {
        public DSContext() : base("ImperialNovaConnectionStrings")
        {

        }

        public static DSContext Create()
        {
            return new DSContext();
        }

        
        public DbSet<DeliveryForm> deliveryform { get; set; }


        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Inventory> inventories { get; set; }
        public DbSet<InventoryBackups> inventorybackups { get; set; }
        public DbSet<InventoryTemporary> inventorytemporary { get; set; }
        public DbSet<Locations> locations { get; set; }
        public DbSet<Notification> notifications { get; set; }
        public DbSet<TodoList> todolists { get; set; }
       public DbSet<Document> documents { get; set; }
        public DbSet<Reminder> reminders { get; set; }
        public DbSet<Customer> customers{ get; set; }
        public DbSet<Supplier> suppliers { get; set; }
        public DbSet<CSV> csvs { get; set; }
        public DbSet<Payment> payments { get; set; }

        public DbSet<Adjustment> adjustments { get; set; }
        public DbSet<InventoryIn> inventoryins { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<AdjustmentProduct> adjustmentproducts { get; set; }
        public DbSet<InventoryInProduct> inventoryinproducts { get; set; }


    }
}
