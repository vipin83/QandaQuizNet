namespace QandaQuizNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbChagesv6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuizDetails", "quizAvtiveDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuizDetails", "quizAvtiveDateTime");
        }
    }
}
