namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datatpyeschanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DeliveryForms", "_RequestedDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DeliveryForms", "_RequestedDate", c => c.DateTime(nullable: false));
        }
    }
}
