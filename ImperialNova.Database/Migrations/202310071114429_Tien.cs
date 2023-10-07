namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tien : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ModifiedCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ModifiedCost");
        }
    }
}
