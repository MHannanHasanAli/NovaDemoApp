namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Aflatoon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "_Category", c => c.String());
            AddColumn("dbo.Products", "_Warehouse", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "_Warehouse");
            DropColumn("dbo.Products", "_Category");
        }
    }
}
