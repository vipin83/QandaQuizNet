namespace QandaQuizNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FreePlayOptionForPlayDetailsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuizPlayDetail", "IsThisFreePlay", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuizPlayDetail", "IsThisFreePlay");
        }
    }
}
