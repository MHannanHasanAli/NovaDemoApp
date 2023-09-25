namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MajinBuu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _TotalSales = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _ProductCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _VanExpenses = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _Car = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _Logistic = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _Storage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _Rent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _SalesPerson = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _Vat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _BusinessRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _Utilities = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _TotalExpenses = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _Left = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _Bank = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t._Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Expenses");
        }
    }
}
