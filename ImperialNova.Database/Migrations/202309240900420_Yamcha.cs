namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Yamcha : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryInProducts", "_CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.InventoryInProducts", "_WarehouseId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InventoryInProducts", "_WarehouseId");
            DropColumn("dbo.InventoryInProducts", "_CategoryId");
        }
    }
}
