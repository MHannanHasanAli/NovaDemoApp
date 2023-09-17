namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class coroorR : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "_Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "_Quantity");
        }
    }
}
