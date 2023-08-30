namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Deployment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "_CName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "_CName", c => c.String(nullable: false));
        }
    }
}
