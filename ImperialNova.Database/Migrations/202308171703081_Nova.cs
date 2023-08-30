namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nova : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryBackups", "_Store", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InventoryBackups", "_Store");
        }
    }
}
