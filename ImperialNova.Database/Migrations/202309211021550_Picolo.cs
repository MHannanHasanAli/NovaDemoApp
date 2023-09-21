namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Picolo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "_Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "_Quantity");
        }
    }
}
