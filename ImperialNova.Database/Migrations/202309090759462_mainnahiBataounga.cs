namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mainnahiBataounga : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CSVs");
            AlterColumn("dbo.CSVs", "_Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CSVs", "_Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.CSVs");
            AlterColumn("dbo.CSVs", "_Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.CSVs", "_Id");
        }
    }
}
