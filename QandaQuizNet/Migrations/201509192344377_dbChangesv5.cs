namespace QandaQuizNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbChangesv5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuizAnswers",
                c => new
                    {
                        quizAnswerId = c.Int(nullable: false, identity: true),
                        quizAnswerText = c.String(),
                        quizAnswerCorrect = c.Boolean(nullable: false),
                        quizQuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.quizAnswerId)
                .ForeignKey("dbo.QuizQuestions", t => t.quizQuestionId, cascadeDelete: true)
                .Index(t => t.quizQuestionId);
            
            CreateTable(
                "dbo.QuizQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        quizQuestionText = c.String(),
                        quizQuestionHasImage = c.Boolean(nullable: false),
                        quizQuestionImagePath = c.String(),
                        quizQuestionHasVideo = c.Boolean(nullable: false),
                        quizQuestionVideoPath = c.String(),
                        quizQuestionNumberOfAnswers = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuizDetails", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.QuizDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        quizTitle = c.String(),
                        quizDescription = c.String(),
                        quizPrizeMoney = c.Decimal(nullable: false, precision: 18, scale: 2),
                        quizTimesNumberOfEntriesAllowed = c.Int(nullable: false),
                        quizWinnerNumber = c.Int(nullable: false),
                        quizWinnerId = c.String(maxLength: 128),
                        quizQuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.quizWinnerId)
                .Index(t => t.quizWinnerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuizQuestions", "Id", "dbo.QuizDetails");
            DropForeignKey("dbo.QuizDetails", "quizWinnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.QuizAnswers", "quizQuestionId", "dbo.QuizQuestions");
            DropIndex("dbo.QuizDetails", new[] { "quizWinnerId" });
            DropIndex("dbo.QuizQuestions", new[] { "Id" });
            DropIndex("dbo.QuizAnswers", new[] { "quizQuestionId" });
            DropTable("dbo.QuizDetails");
            DropTable("dbo.QuizQuestions");
            DropTable("dbo.QuizAnswers");
        }
    }
}
