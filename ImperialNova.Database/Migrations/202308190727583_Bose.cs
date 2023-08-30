namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Bose : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "_SKU", c => c.String());
            AddColumn("dbo.Products", "_Weight", c => c.String());
            AddColumn("dbo.Products", "_Thickness", c => c.String());
            AddColumn("dbo.Products", "_Variations", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "_Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "_Name", c => c.String(nullable: false));
            DropColumn("dbo.Products", "_Variations");
            DropColumn("dbo.Products", "_Thickness");
            DropColumn("dbo.Products", "_Weight");
            DropColumn("dbo.Products", "_SKU");
        }
    }
}
