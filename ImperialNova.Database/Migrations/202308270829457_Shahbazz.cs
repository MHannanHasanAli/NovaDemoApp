namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Shahbazz : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryBackups", "_SKU", c => c.String());
            AddColumn("dbo.InventoryBackups", "_Weight", c => c.String());
            AddColumn("dbo.InventoryBackups", "_Thickness", c => c.String());
            AddColumn("dbo.InventoryBackups", "_Variations", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InventoryBackups", "_Variations");
            DropColumn("dbo.InventoryBackups", "_Thickness");
            DropColumn("dbo.InventoryBackups", "_Weight");
            DropColumn("dbo.InventoryBackups", "_SKU");
        }
    }
}
