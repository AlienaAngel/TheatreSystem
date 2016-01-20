namespace TheatreSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenre1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Plays", "GenreId", c => c.Int());
            CreateIndex("dbo.Plays", "GenreId");
            AddForeignKey("dbo.Plays", "GenreId", "dbo.Genres", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Plays", "GenreId", "dbo.Genres");
            DropIndex("dbo.Plays", new[] { "GenreId" });
            DropColumn("dbo.Plays", "GenreId");
            DropTable("dbo.Genres");
        }
    }
}
