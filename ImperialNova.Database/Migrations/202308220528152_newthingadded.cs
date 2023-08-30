namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newthingadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryForms", "_OrderNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeliveryForms", "_OrderNumber");
        }
    }
}
