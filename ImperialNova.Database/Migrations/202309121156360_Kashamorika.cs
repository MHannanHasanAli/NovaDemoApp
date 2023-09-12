namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Kashamorika : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Adjustments", "_Product", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Adjustments", "_Product");
        }
    }
}
