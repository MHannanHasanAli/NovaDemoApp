namespace ImperialNova.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Goku : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdjustmentProducts",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _AdjustmentId = c.Int(nullable: false),
                        _Photo = c.String(),
                        _SKU = c.String(),
                        _Title = c.String(),
                        _Qty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.Adjustments",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _Date = c.DateTime(nullable: false),
                        _Type = c.String(),
                        _Remarks = c.String(),
                        _Product = c.String(),
                        _Quantity = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
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
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _CName = c.String(),
                        _Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.CSVs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        _Date = c.DateTime(nullable: false),
                        _Description = c.String(),
                        _File = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
                        IsDeleted = c.Boolean(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.DeliveryForms",
                c => new
                    {
                        _id = c.Int(nullable: false, identity: true),
                        _SlaesPerson = c.String(),
                        _Date = c.DateTime(nullable: false),
                        _CustomerName = c.String(),
                        _Address = c.String(),
                        _Country = c.String(),
                        _ContactNo = c.String(),
                        _Email = c.String(),
                        _Note = c.String(),
                        _CashPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _CardPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _FinancePaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _FinanceCompany = c.String(),
                        _SignatureData = c.Byte(nullable: false),
                        _RequestedDate = c.String(),
                        _PostCode = c.String(),
                        ProductsData = c.String(),
                        _TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _PayMethod = c.String(),
                        _OrderNumber = c.String(),
                        _AmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _AmountInBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t._id);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _Name = c.String(),
                        _File = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _TotalSales = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _TotalExpenses = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _Title = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Type = c.String(),
                        _Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.Inventories",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _ProductId = c.Int(nullable: false),
                        _ProductName = c.String(),
                        _Store = c.String(),
                        _ToBeChangedQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.InventoryBackups",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _ProductId = c.Int(nullable: false),
                        _Name = c.String(),
                        _Size = c.String(),
                        _Color = c.String(),
                        _SKU = c.String(),
                        _Weight = c.String(),
                        _Thickness = c.String(),
                        _Variations = c.Int(nullable: false),
                        _Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _RetailPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _QuantityIn = c.Int(nullable: false),
                        _QuantityOut = c.Int(nullable: false),
                        _Notes = c.String(),
                        _ExportDate = c.DateTime(nullable: false),
                        _CategoryId = c.Int(nullable: false),
                        _Action = c.String(),
                        JustAdded = c.Boolean(nullable: false),
                        _UserId = c.String(),
                        _UserFullName = c.String(),
                        _UserEmail = c.String(),
                        _Store = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.InventoryInProducts",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _InventoryInId = c.Int(nullable: false),
                        _Photo = c.String(),
                        _SKU = c.String(),
                        _Title = c.String(),
                        _ExpiryDate = c.DateTime(nullable: false),
                        _Location = c.String(),
                        _Qty = c.Int(nullable: false),
                        _Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _CategoryId = c.Int(nullable: false),
                        _WarehouseId = c.Int(nullable: false),
                        _Warehouse = c.String(),
                        _Size = c.String(),
                        _Color = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.InventoryIns",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _Date = c.DateTime(nullable: false),
                        _ShippingCompany = c.String(),
                        _Tracking = c.String(),
                        _Supplier = c.String(),
                        _Status = c.String(),
                        _Quantity = c.Int(nullable: false),
                        _Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsDeleted = c.Boolean(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.InventoryTemporaries",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _ProductId = c.Int(nullable: false),
                        _Name = c.String(),
                        _Size = c.String(),
                        _Color = c.String(),
                        _SKU = c.String(),
                        _Weight = c.String(),
                        _Thickness = c.String(),
                        _Variations = c.Int(nullable: false),
                        _Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _RetailPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _QuantityIn = c.Int(nullable: false),
                        _QuantityOut = c.Int(nullable: false),
                        _Notes = c.String(),
                        _ExportDate = c.DateTime(nullable: false),
                        _CategoryId = c.Int(nullable: false),
                        _Action = c.String(),
                        _UserId = c.String(),
                        _UserFullName = c.String(),
                        _UserEmail = c.String(),
                        _Store = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _LocationName = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _Description = c.String(),
                        _UserName = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _OrderId = c.Int(nullable: false),
                        _ProductId = c.Int(nullable: false),
                        _Photo = c.String(),
                        _SKU = c.String(),
                        _Title = c.String(),
                        _Qty = c.Int(nullable: false),
                        _Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _IsPacked = c.String(),
                        _location = c.String(),
                        _Size = c.String(),
                        _Color = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _Date = c.DateTime(nullable: false),
                        _Record = c.String(),
                        _ShipByDate = c.DateTime(nullable: false),
                        _Tracking = c.String(),
                        _Status = c.String(),
                        _Customer = c.String(),
                        _Priority = c.String(),
                        _Quantity = c.Int(nullable: false),
                        _Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _IsPacked = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Type = c.String(),
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
                        IsDeleted = c.Boolean(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _Name = c.String(),
                        _Size = c.String(),
                        _Color = c.String(),
                        _Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _SKU = c.String(),
                        _Weight = c.String(),
                        _Thickness = c.String(),
                        _Variations = c.Int(nullable: false),
                        _OpeningStock = c.Int(nullable: false),
                        _RetailPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        _Quantity = c.Int(nullable: false),
                        _QuantityIn = c.Int(nullable: false),
                        _QuantityOut = c.Int(nullable: false),
                        _Notes = c.String(),
                        _ExportDate = c.DateTime(nullable: false),
                        _CategoryId = c.Int(nullable: false),
                        _WarehouseId = c.Int(nullable: false),
                        _LowStockAlert = c.Int(nullable: false),
                        _Photo = c.String(),
                        _Category = c.String(),
                        _Warehouse = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.Reminders",
                c => new
                    {
                        _Id = c.Int(nullable: false, identity: true),
                        _CreatedAt = c.DateTime(nullable: false),
                        _CreatedBy = c.String(),
                        _Title = c.String(),
                        _Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                        IsDeleted = c.Boolean(nullable: false),
                        Type = c.String(),
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
                        IsDeleted = c.Boolean(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t._Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Password = c.String(),
                        Role = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TodoLists");
            DropTable("dbo.Suppliers");
            DropTable("dbo.StockMovements");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Reminders");
            DropTable("dbo.Products");
            DropTable("dbo.Payments");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderProducts");
            DropTable("dbo.Notifications");
            DropTable("dbo.Locations");
            DropTable("dbo.InventoryTemporaries");
            DropTable("dbo.InventoryIns");
            DropTable("dbo.InventoryInProducts");
            DropTable("dbo.InventoryBackups");
            DropTable("dbo.Inventories");
            DropTable("dbo.Expenses");
            DropTable("dbo.Documents");
            DropTable("dbo.DeliveryForms");
            DropTable("dbo.Customers");
            DropTable("dbo.CSVs");
            DropTable("dbo.Categories");
            DropTable("dbo.Backups");
            DropTable("dbo.Adjustments");
            DropTable("dbo.AdjustmentProducts");
        }
    }
}
