namespace QandaQuizNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FreePlayChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "NumberOfFreeGamesPlayed", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "UsedAllFreeGames", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UsedAllFreeGames");
            DropColumn("dbo.AspNetUsers", "NumberOfFreeGamesPlayed");
        }
    }
}
