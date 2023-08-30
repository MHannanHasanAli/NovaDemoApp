namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "_Photo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "_Photo");
        }
    }
}
