namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Goku : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "_Customer", c => c.String());
            DropColumn("dbo.Orders", "_CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "_CustomerId", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "_Customer");
        }
    }
}
