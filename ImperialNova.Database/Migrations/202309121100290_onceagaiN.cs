namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class onceagaiN : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adjustments",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _Date = c.DateTime(nullable: false),
                        _Type = c.String(),
                        _Remarks = c.String(),
                        _Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Adjustments");
        }
    }
}
