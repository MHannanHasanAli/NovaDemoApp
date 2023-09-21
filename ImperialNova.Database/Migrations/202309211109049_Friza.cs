namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Friza : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "_IsPacked", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "_IsPacked");
        }
    }
}
