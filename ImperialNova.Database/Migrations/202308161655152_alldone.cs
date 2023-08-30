namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alldone : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _CName = c.String(nullable: false),
                        _Description = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.DeliveryForms",
                c => new
                    {
                        _id = c.Int(nullable: false, identity: true),
                        _SlaesPerson = c.String(),
                        _Date = c.DateTime(nullable: false),
                        _CustomerName = c.String(),
                        _Address = c.String(),
                        _Country = c.String(),
                        _ContactNo = c.String(),
                        _Email = c.String(),
                        _Note = c.String(),
                        _RequestedDate = c.DateTime(nullable: false),
                        ProductsData = c.String(),
                        _TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _PayMethod = c.String(),
                        _AmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _AmountInBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t._id);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _ProductId = c.Int(nullable: false),
                        _ProductName = c.String(),
                        _ToBeChangedQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.InventoryBackups",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _ProductId = c.Int(nullable: false),
                        _Name = c.String(),
                        _Size = c.String(),
                        _Color = c.String(),
                        _Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _RetailPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _QuantityIn = c.Int(nullable: false),
                        _QuantityOut = c.Int(nullable: false),
                        _Notes = c.String(),
                        _ExportDate = c.DateTime(nullable: false),
                        _CategoryId = c.Int(nullable: false),
                        _Action = c.String(),
                        _UserId = c.String(),
                        _UserFullName = c.String(),
                        _UserEmail = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.InventoryTemporaries",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _ProductId = c.Int(nullable: false),
                        _Name = c.String(),
                        _Size = c.String(),
                        _Color = c.String(),
                        _Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _RetailPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _QuantityIn = c.Int(nullable: false),
                        _QuantityOut = c.Int(nullable: false),
                        _Notes = c.String(),
                        _ExportDate = c.DateTime(nullable: false),
                        _CategoryId = c.Int(nullable: false),
                        _Action = c.String(),
                        _UserId = c.String(),
                        _UserFullName = c.String(),
                        _UserEmail = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        _id = c.Int(nullable: false, identity: true),
                        _LocationName = c.String(),
                    })
                .PrimaryKey(t => t._id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _Name = c.String(nullable: false),
                        _Size = c.String(),
                        _Color = c.String(),
                        _Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _RetailPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _QuantityIn = c.Int(nullable: false),
                        _QuantityOut = c.Int(nullable: false),
                        _Notes = c.String(),
                        _ExportDate = c.DateTime(nullable: false),
                        _CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
            DropTable("dbo.Locations");
            DropTable("dbo.InventoryTemporaries");
            DropTable("dbo.InventoryBackups");
            DropTable("dbo.Inventories");
            DropTable("dbo.DeliveryForms");
            DropTable("dbo.Categories");
        }
    }
}
