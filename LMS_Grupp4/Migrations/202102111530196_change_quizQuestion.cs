namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_quizQuestion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuizQuestions", "Question_ID", c => c.Int());
            CreateIndex("dbo.QuizQuestions", "Question_ID");
            AddForeignKey("dbo.QuizQuestions", "Question_ID", "dbo.Questions", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuizQuestions", "Question_ID", "dbo.Questions");
            DropIndex("dbo.QuizQuestions", new[] { "Question_ID" });
            DropColumn("dbo.QuizQuestions", "Question_ID");
        }
    }
}
