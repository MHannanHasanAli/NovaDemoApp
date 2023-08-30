namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryTemporaries", "_SKU", c => c.String());
            AddColumn("dbo.InventoryTemporaries", "_Weight", c => c.String());
            AddColumn("dbo.InventoryTemporaries", "_Thickness", c => c.String());
            AddColumn("dbo.InventoryTemporaries", "_Variations", c => c.Int(nullable: false));
            AddColumn("dbo.InventoryTemporaries", "_Store", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InventoryTemporaries", "_Store");
            DropColumn("dbo.InventoryTemporaries", "_Variations");
            DropColumn("dbo.InventoryTemporaries", "_Thickness");
            DropColumn("dbo.InventoryTemporaries", "_Weight");
            DropColumn("dbo.InventoryTemporaries", "_SKU");
        }
    }
}
