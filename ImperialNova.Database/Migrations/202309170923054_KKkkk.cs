namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KKkkk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "_Store", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "_Store");
        }
    }
}
