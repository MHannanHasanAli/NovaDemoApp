namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CSVs",
                c => new
                    {
                        _Id = c.String(nullable: false, maxLength: 128),
                        _Date = c.DateTime(nullable: false),
                        _Description = c.String(),
                        _File = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _Record = c.Int(nullable: false),
                        _Date = c.DateTime(nullable: false),
                        _Individual = c.String(),
                        _Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _Remarks = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
            AddColumn("dbo.Reminders", "_CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Reminders", "_CreatedBy", c => c.String());
            AddColumn("dbo.Reminders", "_Description", c => c.String());
            DropColumn("dbo.Reminders", "_Text");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reminders", "_Text", c => c.String());
            DropColumn("dbo.Reminders", "_Description");
            DropColumn("dbo.Reminders", "_CreatedBy");
            DropColumn("dbo.Reminders", "_CreatedAt");
            DropTable("dbo.Payments");
            DropTable("dbo.CSVs");
        }
    }
}
