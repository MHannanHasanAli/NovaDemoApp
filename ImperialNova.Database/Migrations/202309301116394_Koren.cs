namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Koren : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdjustmentProducts", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Adjustments", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categories", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CSVs", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeliveryForms", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Documents", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Expenses", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.InventoryInProducts", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.InventoryIns", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Locations", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderProducts", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Payments", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Reminders", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Suppliers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.TodoLists", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TodoLists", "IsDeleted");
            DropColumn("dbo.Suppliers", "IsDeleted");
            DropColumn("dbo.Reminders", "IsDeleted");
            DropColumn("dbo.Products", "IsDeleted");
            DropColumn("dbo.Payments", "IsDeleted");
            DropColumn("dbo.Orders", "IsDeleted");
            DropColumn("dbo.OrderProducts", "IsDeleted");
            DropColumn("dbo.Locations", "IsDeleted");
            DropColumn("dbo.InventoryIns", "IsDeleted");
            DropColumn("dbo.InventoryInProducts", "IsDeleted");
            DropColumn("dbo.Expenses", "IsDeleted");
            DropColumn("dbo.Documents", "IsDeleted");
            DropColumn("dbo.DeliveryForms", "IsDeleted");
            DropColumn("dbo.Customers", "IsDeleted");
            DropColumn("dbo.CSVs", "IsDeleted");
            DropColumn("dbo.Categories", "IsDeleted");
            DropColumn("dbo.Adjustments", "IsDeleted");
            DropColumn("dbo.AdjustmentProducts", "IsDeleted");
        }
    }
}
