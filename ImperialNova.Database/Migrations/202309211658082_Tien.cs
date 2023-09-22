namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tien : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StockMovements",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _Fdays = c.Int(nullable: false),
                        _Ffrom = c.Int(nullable: false),
                        _Fto = c.Int(nullable: false),
                        _Sdays = c.Int(nullable: false),
                        _Sfrom = c.Int(nullable: false),
                        _Sto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StockMovements");
        }
    }
}
