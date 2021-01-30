namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuizQuestion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Answer = c.String(nullable: false, maxLength: 100),
                        Fake1 = c.String(nullable: false, maxLength: 100),
                        Fake2 = c.String(nullable: false, maxLength: 100),
                        Fake3 = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.QuizInformations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QuizName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.QuizQuestions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Question_ID = c.Int(),
                        QuizInformation_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Questions", t => t.Question_ID)
                .ForeignKey("dbo.QuizInformations", t => t.QuizInformation_ID)
                .Index(t => t.Question_ID)
                .Index(t => t.QuizInformation_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuizQuestions", "QuizInformation_ID", "dbo.QuizInformations");
            DropForeignKey("dbo.QuizQuestions", "Question_ID", "dbo.Questions");
            DropIndex("dbo.QuizQuestions", new[] { "QuizInformation_ID" });
            DropIndex("dbo.QuizQuestions", new[] { "Question_ID" });
            DropTable("dbo.QuizQuestions");
            DropTable("dbo.QuizInformations");
            DropTable("dbo.Questions");
        }
    }
}
