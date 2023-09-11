namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sarak : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "_WarehouseId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "_WarehouseId");
        }
    }
}
