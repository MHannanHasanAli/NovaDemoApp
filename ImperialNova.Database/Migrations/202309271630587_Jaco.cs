namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Jaco : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryInProducts", "_Warehouse", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.InventoryInProducts", "_Warehouse");
        }
    }
}
