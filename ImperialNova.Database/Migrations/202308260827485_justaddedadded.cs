namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class justaddedadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryBackups", "JustAdded", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InventoryBackups", "JustAdded");
        }
    }
}
