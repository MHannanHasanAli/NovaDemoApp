namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class signatureandfinanceadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryForms", "_CashPaid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DeliveryForms", "_CardPaid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DeliveryForms", "_FinancePaid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DeliveryForms", "_FinanceCompany", c => c.String());
            AddColumn("dbo.DeliveryForms", "_SignatureData", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeliveryForms", "_SignatureData");
            DropColumn("dbo.DeliveryForms", "_FinanceCompany");
            DropColumn("dbo.DeliveryForms", "_FinancePaid");
            DropColumn("dbo.DeliveryForms", "_CardPaid");
            DropColumn("dbo.DeliveryForms", "_CashPaid");
        }
    }
}
