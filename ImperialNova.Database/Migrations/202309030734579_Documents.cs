namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Documents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _Name = c.String(),
                        _File = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.TodoLists",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _DueDate = c.DateTime(nullable: false),
                        _Description = c.String(),
                        _File = c.String(),
                        _Ticked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t._Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TodoLists");
            DropTable("dbo.Documents");
        }
    }
}
