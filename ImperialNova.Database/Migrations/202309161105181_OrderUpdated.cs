namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderUpdated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _Date = c.DateTime(nullable: false),
                        _Record = c.String(),
                        _ShipByDate = c.DateTime(nullable: false),
                        _Tracking = c.String(),
                        _Status = c.String(),
                        _CustomerId = c.Int(nullable: false),
                        _Priority = c.String(),
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
        }
    }
}
