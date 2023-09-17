namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Corordinate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "_Store");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "_Store", c => c.String());
        }
    }
}
