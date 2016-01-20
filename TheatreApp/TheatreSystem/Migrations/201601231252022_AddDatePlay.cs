namespace TheatreSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDatePlay : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "PlayId", "dbo.Plays");
            DropIndex("dbo.Tickets", new[] { "PlayId" });
            CreateTable(
                "dbo.DatePlays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        PlayId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Plays", t => t.PlayId, cascadeDelete: true)
                .Index(t => t.PlayId);
            
            AddColumn("dbo.Tickets", "DatePlayId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "DatePlayId");
            AddForeignKey("dbo.Tickets", "DatePlayId", "dbo.DatePlays", "Id", cascadeDelete: true);
            DropColumn("dbo.Plays", "DatePlay");
            DropColumn("dbo.Tickets", "PlayId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "PlayId", c => c.Int(nullable: false));
            AddColumn("dbo.Plays", "DatePlay", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.DatePlays", "PlayId", "dbo.Plays");
            DropForeignKey("dbo.Tickets", "DatePlayId", "dbo.DatePlays");
            DropIndex("dbo.Tickets", new[] { "DatePlayId" });
            DropIndex("dbo.DatePlays", new[] { "PlayId" });
            DropColumn("dbo.Tickets", "DatePlayId");
            DropTable("dbo.DatePlays");
            CreateIndex("dbo.Tickets", "PlayId");
            AddForeignKey("dbo.Tickets", "PlayId", "dbo.Plays", "Id", cascadeDelete: true);
        }
    }
}
