namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vegeta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Adjustments", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Adjustments", "Type", c => c.String());
            AddColumn("dbo.Categories", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categories", "Type", c => c.String());
            AddColumn("dbo.Customers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "Type", c => c.String());
            AddColumn("dbo.DeliveryForms", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeliveryForms", "Type", c => c.String());
            AddColumn("dbo.Documents", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Documents", "Type", c => c.String());
            AddColumn("dbo.Expenses", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Expenses", "Type", c => c.String());
            AddColumn("dbo.InventoryIns", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.InventoryIns", "Type", c => c.String());
            AddColumn("dbo.Locations", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Locations", "Type", c => c.String());
            AddColumn("dbo.Orders", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "Type", c => c.String());
            AddColumn("dbo.Payments", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Payments", "Type", c => c.String());
            AddColumn("dbo.Products", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "Type", c => c.String());
            AddColumn("dbo.Reminders", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Reminders", "Type", c => c.String());
            AddColumn("dbo.Suppliers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Suppliers", "Type", c => c.String());
            AddColumn("dbo.TodoLists", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.TodoLists", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TodoLists", "Type");
            DropColumn("dbo.TodoLists", "IsDeleted");
            DropColumn("dbo.Suppliers", "Type");
            DropColumn("dbo.Suppliers", "IsDeleted");
            DropColumn("dbo.Reminders", "Type");
            DropColumn("dbo.Reminders", "IsDeleted");
            DropColumn("dbo.Products", "Type");
            DropColumn("dbo.Products", "IsDeleted");
            DropColumn("dbo.Payments", "Type");
            DropColumn("dbo.Payments", "IsDeleted");
            DropColumn("dbo.Orders", "Type");
            DropColumn("dbo.Orders", "IsDeleted");
            DropColumn("dbo.Locations", "Type");
            DropColumn("dbo.Locations", "IsDeleted");
            DropColumn("dbo.InventoryIns", "Type");
            DropColumn("dbo.InventoryIns", "IsDeleted");
            DropColumn("dbo.Expenses", "Type");
            DropColumn("dbo.Expenses", "IsDeleted");
            DropColumn("dbo.Documents", "Type");
            DropColumn("dbo.Documents", "IsDeleted");
            DropColumn("dbo.DeliveryForms", "Type");
            DropColumn("dbo.DeliveryForms", "IsDeleted");
            DropColumn("dbo.Customers", "Type");
            DropColumn("dbo.Customers", "IsDeleted");
            DropColumn("dbo.Categories", "Type");
            DropColumn("dbo.Categories", "IsDeleted");
            DropColumn("dbo.Adjustments", "Type");
            DropColumn("dbo.Adjustments", "IsDeleted");
        }
    }
}
