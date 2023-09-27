namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Toppo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderProducts", "_location", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderProducts", "_location");
        }
    }
}
