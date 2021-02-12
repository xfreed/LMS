namespace LMS_Grupp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuizScore : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuizScores",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Score = c.Int(nullable: false),
                        QuizInformation_ID = c.Int(),
                        QuizUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.QuizInformations", t => t.QuizInformation_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.QuizUser_Id)
                .Index(t => t.QuizInformation_ID)
                .Index(t => t.QuizUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuizScores", "QuizUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.QuizScores", "QuizInformation_ID", "dbo.QuizInformations");
            DropIndex("dbo.QuizScores", new[] { "QuizUser_Id" });
            DropIndex("dbo.QuizScores", new[] { "QuizInformation_ID" });
            DropTable("dbo.QuizScores");
        }
    }
}
