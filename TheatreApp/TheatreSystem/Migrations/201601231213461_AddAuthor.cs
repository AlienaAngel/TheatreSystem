namespace TheatreSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuthor : DbMigration
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
            
            AddColumn("dbo.Plays", "AuthorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Plays", "AuthorId");
            AddForeignKey("dbo.Plays", "AuthorId", "dbo.Authors", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Plays", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Plays", new[] { "AuthorId" });
            DropColumn("dbo.Plays", "AuthorId");
            DropTable("dbo.Authors");
        }
    }
}
