namespace TheatreSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlaces : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "ZoneId", "dbo.Zones");
            DropIndex("dbo.Tickets", new[] { "ZoneId" });
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
            
            AddColumn("dbo.Tickets", "PlaceId", c => c.Int(nullable: false));
            AddColumn("dbo.Zones", "AmountPlaces", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "PlaceId");
            AddForeignKey("dbo.Tickets", "PlaceId", "dbo.Places", "Id", cascadeDelete: true);
            DropColumn("dbo.Tickets", "ZoneId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "ZoneId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Places", "ZoneId", "dbo.Zones");
            DropForeignKey("dbo.Tickets", "PlaceId", "dbo.Places");
            DropIndex("dbo.Places", new[] { "ZoneId" });
            DropIndex("dbo.Tickets", new[] { "PlaceId" });
            DropColumn("dbo.Zones", "AmountPlaces");
            DropColumn("dbo.Tickets", "PlaceId");
            DropTable("dbo.Places");
            CreateIndex("dbo.Tickets", "ZoneId");
            AddForeignKey("dbo.Tickets", "ZoneId", "dbo.Zones", "Id", cascadeDelete: true);
        }
    }
}
