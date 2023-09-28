namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Babadi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryInProducts", "_Size", c => c.String());
            AddColumn("dbo.InventoryInProducts", "_Color", c => c.String());
            AddColumn("dbo.OrderProducts", "_Size", c => c.String());
            AddColumn("dbo.OrderProducts", "_Color", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderProducts", "_Color");
            DropColumn("dbo.OrderProducts", "_Size");
            DropColumn("dbo.InventoryInProducts", "_Color");
            DropColumn("dbo.InventoryInProducts", "_Size");
        }
    }
}
