namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Jiren : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Expenses", "_Title", c => c.String());
            AddColumn("dbo.Expenses", "_Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Expenses", "_Date");
            DropColumn("dbo.Expenses", "_Title");
        }
    }
}
