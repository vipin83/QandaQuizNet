namespace QandaQuizNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class town : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TownCity", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TownCity");
        }
    }
}
