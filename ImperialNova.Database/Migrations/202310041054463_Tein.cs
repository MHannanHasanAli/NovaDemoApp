namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tein : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Signatures",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        DeliveryFormID = c.Int(nullable: false),
                        SignatureValue = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Signatures");
        }
    }
}
