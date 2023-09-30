namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cooler : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "_UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "_UserName");
        }
    }
}
