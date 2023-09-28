namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Uub : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CSVs");
            AddColumn("dbo.CSVs", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CSVs", "ID");
            DropColumn("dbo.CSVs", "_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CSVs", "_Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.CSVs");
            DropColumn("dbo.CSVs", "ID");
            AddPrimaryKey("dbo.CSVs", "_Id");
        }
    }
}
