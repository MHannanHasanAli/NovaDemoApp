namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBz : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inventories", "_Store", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inventories", "_Store");
        }
    }
}
