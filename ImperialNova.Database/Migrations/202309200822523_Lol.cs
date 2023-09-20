namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Lol : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InventoryInProducts",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _InventoryInId = c.Int(nullable: false),
                        _Photo = c.String(),
                        _SKU = c.String(),
                        _Title = c.String(),
                        _ExpiryDate = c.DateTime(nullable: false),
                        _Location = c.String(),
                        _Qty = c.Int(nullable: false),
                        _Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t._Id);
            
            DropColumn("dbo.InventoryIns", "_ProductId");
            DropColumn("dbo.InventoryIns", "_Photo");
            DropColumn("dbo.InventoryIns", "_SKU");
            DropColumn("dbo.InventoryIns", "_Title");
            DropColumn("dbo.InventoryIns", "_Location");
            DropColumn("dbo.InventoryIns", "_ExpiryDate");
            DropColumn("dbo.InventoryIns", "_Price");
            DropColumn("dbo.InventoryIns", "_SupplierId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InventoryIns", "_SupplierId", c => c.Int(nullable: false));
            AddColumn("dbo.InventoryIns", "_Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.InventoryIns", "_ExpiryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.InventoryIns", "_Location", c => c.String());
            AddColumn("dbo.InventoryIns", "_Title", c => c.String());
            AddColumn("dbo.InventoryIns", "_SKU", c => c.String());
            AddColumn("dbo.InventoryIns", "_Photo", c => c.String());
            AddColumn("dbo.InventoryIns", "_ProductId", c => c.Int(nullable: false));
            DropTable("dbo.InventoryInProducts");
        }
    }
}
