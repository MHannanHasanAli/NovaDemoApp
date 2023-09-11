namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Two : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _Name = c.String(),
                        _Email = c.String(),
                        _Phone = c.String(),
                        _Address = c.String(),
                        _Zip = c.String(),
                        _City = c.String(),
                        _Country = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _Name = c.String(),
                        _Email = c.String(),
                        _Phone = c.String(),
                        _Address = c.String(),
                        _Zip = c.String(),
                        _City = c.String(),
                        _Country = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Suppliers");
            DropTable("dbo.Customers");
        }
    }
}
