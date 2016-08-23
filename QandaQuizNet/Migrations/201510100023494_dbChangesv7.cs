namespace QandaQuizNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbChangesv7 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.QuizAnswers", newName: "QuizAnswer");
            RenameTable(name: "dbo.QuizQuestions", newName: "QuizQuestion");
            RenameTable(name: "dbo.QuizDetails", newName: "QuizDetail");
            RenameTable(name: "dbo.userCountries", newName: "userCountry");
            RenameTable(name: "dbo.TopupAccounts", newName: "TopupAccount");
            DropPrimaryKey("dbo.QuizAnswer");
            DropColumn("dbo.QuizAnswer", "quizAnswerId");
            AddColumn("dbo.QuizAnswer", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.QuizAnswer", "Id");            
            DropColumn("dbo.QuizDetail", "quizQuestionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuizDetail", "quizQuestionId", c => c.Int(nullable: false));
            AddColumn("dbo.QuizAnswer", "quizAnswerId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.QuizAnswer");
            DropColumn("dbo.QuizAnswer", "Id");
            AddPrimaryKey("dbo.QuizAnswer", "quizAnswerId");
            RenameTable(name: "dbo.TopupAccount", newName: "TopupAccounts");
            RenameTable(name: "dbo.userCountry", newName: "userCountries");
            RenameTable(name: "dbo.QuizDetail", newName: "QuizDetails");
            RenameTable(name: "dbo.QuizQuestion", newName: "QuizQuestions");
            RenameTable(name: "dbo.QuizAnswer", newName: "QuizAnswers");
        }
    }
}
