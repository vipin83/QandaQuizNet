namespace QandaQuizNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class playQuizDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuizPlayDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        quizSubmittedDateTime = c.DateTime(nullable: false),
                        quizPlayAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        quiz_Id = c.Int(),
                        quizAnswer_Id = c.Int(),
                        user_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuizDetail", t => t.quiz_Id)
                .ForeignKey("dbo.QuizAnswer", t => t.quizAnswer_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.user_Id)
                .Index(t => t.quiz_Id)
                .Index(t => t.quizAnswer_Id)
                .Index(t => t.user_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuizPlayDetail", "user_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.QuizPlayDetail", "quizAnswer_Id", "dbo.QuizAnswer");
            DropForeignKey("dbo.QuizPlayDetail", "quiz_Id", "dbo.QuizDetail");
            DropIndex("dbo.QuizPlayDetail", new[] { "user_Id" });
            DropIndex("dbo.QuizPlayDetail", new[] { "quizAnswer_Id" });
            DropIndex("dbo.QuizPlayDetail", new[] { "quiz_Id" });
            DropTable("dbo.QuizPlayDetail");
        }
    }
}
