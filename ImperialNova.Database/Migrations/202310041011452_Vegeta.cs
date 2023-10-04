namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vegeta : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DeliveryForms", "_SignatureData", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DeliveryForms", "_SignatureData", c => c.Byte(nullable: false));
        }
    }
}
