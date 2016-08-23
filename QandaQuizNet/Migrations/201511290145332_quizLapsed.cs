namespace QandaQuizNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quizLapsed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuizDetail", "quizLapsed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuizDetail", "quizLapsed");
        }
    }
}
