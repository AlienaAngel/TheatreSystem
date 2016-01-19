namespace TheatreSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTicketsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Serial = c.String(),
                        PlayId = c.Int(nullable: false),
                        ZoneId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Zones", t => t.ZoneId, cascadeDelete: true)
                .ForeignKey("dbo.Plays", t => t.PlayId, cascadeDelete: true)
                .Index(t => t.PlayId)
                .Index(t => t.ZoneId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "PlayId", "dbo.Plays");
            DropForeignKey("dbo.Tickets", "ZoneId", "dbo.Zones");
            DropIndex("dbo.Tickets", new[] { "ZoneId" });
            DropIndex("dbo.Tickets", new[] { "PlayId" });
            DropTable("dbo.Tickets");
        }
    }
}
