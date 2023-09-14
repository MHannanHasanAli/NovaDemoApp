namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Checkitout : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InventoryIns",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _Date = c.DateTime(nullable: false),
                        _ShippingCompany = c.String(),
                        _Tracking = c.String(),
                        _Supplier = c.String(),
                        _Status = c.String(),
                        _ProductId = c.Int(nullable: false),
                        _Photo = c.String(),
                        _SKU = c.String(),
                        _Title = c.String(),
                        _Location = c.String(),
                        _Quantity = c.Int(nullable: false),
                        _ExpiryDate = c.DateTime(nullable: false),
                        _Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.InventoryIns");
        }
    }
}
