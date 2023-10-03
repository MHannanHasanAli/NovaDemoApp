namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Bulma : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryForms", "_PostCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeliveryForms", "_PostCode");
        }
    }
}
