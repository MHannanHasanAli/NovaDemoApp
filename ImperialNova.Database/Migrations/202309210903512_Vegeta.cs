namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vegeta : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _OrderId = c.Int(nullable: false),
                        _ProductId = c.Int(nullable: false),
                        _Photo = c.String(),
                        _SKU = c.String(),
                        _Title = c.String(),
                        _Qty = c.Int(nullable: false),
                        _Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _IsPacked = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
            DropColumn("dbo.Orders", "_ProductId");
            DropColumn("dbo.Orders", "_Photo");
            DropColumn("dbo.Orders", "_SKU");
            DropColumn("dbo.Orders", "_Title");
            DropColumn("dbo.Orders", "_Qty");
            DropColumn("dbo.Orders", "_Price");
            DropColumn("dbo.Orders", "_Amount");
            DropColumn("dbo.Orders", "_IsPacked");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "_IsPacked", c => c.String());
            AddColumn("dbo.Orders", "_Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "_Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "_Qty", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "_Title", c => c.String());
            AddColumn("dbo.Orders", "_SKU", c => c.String());
            AddColumn("dbo.Orders", "_Photo", c => c.String());
            AddColumn("dbo.Orders", "_ProductId", c => c.Int(nullable: false));
            DropTable("dbo.OrderProducts");
        }
    }
}
