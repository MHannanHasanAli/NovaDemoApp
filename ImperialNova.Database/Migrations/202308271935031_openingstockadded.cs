namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class openingstockadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "_OpeningStock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "_OpeningStock");
        }
    }
}
