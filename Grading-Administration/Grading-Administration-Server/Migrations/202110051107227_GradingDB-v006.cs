namespace GradingAdministration_server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GradingDBv006 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        Time = c.DateTime(nullable: false),
                        NumericalGrade = c.Double(nullable: false),
                        LetterGrade = c.String(),
                        Delimiter = c.Double(nullable: false),
                        Contribution_ContributionId = c.Int(),
                    })
                .PrimaryKey(t => t.Time)
                .ForeignKey("dbo.ModuleContributions", t => t.Contribution_ContributionId)
                .Index(t => t.Contribution_ContributionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Grades", "Contribution_ContributionId", "dbo.ModuleContributions");
            DropIndex("dbo.Grades", new[] { "Contribution_ContributionId" });
            DropTable("dbo.Grades");
        }
    }
}
