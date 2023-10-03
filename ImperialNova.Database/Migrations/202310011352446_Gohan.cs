namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Gohan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Backups",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        ComponenetId = c.Int(nullable: false),
                        Aspect = c.String(),
                        Type = c.String(),
                        DeletionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t._Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Backups");
        }
    }
}
