namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Turles : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Expenses", "_ProductCost");
            DropColumn("dbo.Expenses", "_VanExpenses");
            DropColumn("dbo.Expenses", "_Car");
            DropColumn("dbo.Expenses", "_Logistic");
            DropColumn("dbo.Expenses", "_Storage");
            DropColumn("dbo.Expenses", "_Rent");
            DropColumn("dbo.Expenses", "_SalesPerson");
            DropColumn("dbo.Expenses", "_Vat");
            DropColumn("dbo.Expenses", "_BusinessRate");
            DropColumn("dbo.Expenses", "_Utilities");
            DropColumn("dbo.Expenses", "_Left");
            DropColumn("dbo.Expenses", "_Tax");
            DropColumn("dbo.Expenses", "_Bank");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Expenses", "_Bank", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Expenses", "_Tax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Expenses", "_Left", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Expenses", "_Utilities", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Expenses", "_BusinessRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Expenses", "_Vat", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Expenses", "_SalesPerson", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Expenses", "_Rent", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Expenses", "_Storage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Expenses", "_Logistic", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Expenses", "_Car", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Expenses", "_VanExpenses", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Expenses", "_ProductCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
