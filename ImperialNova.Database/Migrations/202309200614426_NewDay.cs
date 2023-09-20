namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDay : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdjustmentProducts",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _AdjustmentId = c.Int(nullable: false),
                        _Photo = c.String(),
                        _SKU = c.String(),
                        _Title = c.String(),
                        _Qty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AdjustmentProducts");
        }
    }
}
