namespace TheatreSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Plays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Info = c.String(),
                        AuthorId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Dates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayId = c.Int(nullable: false),
                        DatePlay = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Plays", t => t.PlayId, cascadeDelete: true)
                .Index(t => t.PlayId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Serial = c.String(),
                        DateId = c.Int(nullable: false),
                        PlaceId = c.Int(nullable: false),
                        OrderId = c.Int(),
                        Play_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .ForeignKey("dbo.Places", t => t.PlaceId, cascadeDelete: true)
                .ForeignKey("dbo.Dates", t => t.DateId, cascadeDelete: true)
                .ForeignKey("dbo.Plays", t => t.Play_Id)
                .Index(t => t.DateId)
                .Index(t => t.PlaceId)
                .Index(t => t.OrderId)
                .Index(t => t.Play_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsConfirmed = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Phone = c.String(),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        ZoneId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Zones", t => t.ZoneId, cascadeDelete: true)
                .Index(t => t.ZoneId);
            
            CreateTable(
                "dbo.Zones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Plays", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.Tickets", "Play_Id", "dbo.Plays");
            DropForeignKey("dbo.Plays", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Dates", "PlayId", "dbo.Plays");
            DropForeignKey("dbo.Tickets", "DateId", "dbo.Dates");
            DropForeignKey("dbo.Places", "ZoneId", "dbo.Zones");
            DropForeignKey("dbo.Tickets", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Tickets", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.Places", new[] { "ZoneId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.Tickets", new[] { "Play_Id" });
            DropIndex("dbo.Tickets", new[] { "OrderId" });
            DropIndex("dbo.Tickets", new[] { "PlaceId" });
            DropIndex("dbo.Tickets", new[] { "DateId" });
            DropIndex("dbo.Dates", new[] { "PlayId" });
            DropIndex("dbo.Plays", new[] { "GenreId" });
            DropIndex("dbo.Plays", new[] { "AuthorId" });
            DropTable("dbo.UserRole");
            DropTable("dbo.Genres");
            DropTable("dbo.Zones");
            DropTable("dbo.Places");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Orders");
            DropTable("dbo.Tickets");
            DropTable("dbo.Dates");
            DropTable("dbo.Plays");
            DropTable("dbo.Authors");
        }
    }
}
