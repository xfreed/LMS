namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_quiz : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuizQuestions", "Question_ID", "dbo.Questions");
            DropIndex("dbo.QuizQuestions", new[] { "Question_ID" });
            AddColumn("dbo.Questions", "QuestionName", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.QuizQuestions", "Question_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuizQuestions", "Question_ID", c => c.Int());
            DropColumn("dbo.Questions", "QuestionName");
            CreateIndex("dbo.QuizQuestions", "Question_ID");
            AddForeignKey("dbo.QuizQuestions", "Question_ID", "dbo.Questions", "ID");
        }
    }
}
