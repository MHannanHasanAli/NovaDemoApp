namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Goten : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderProducts", "_Cost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderProducts", "_Cost");
        }
    }
}
