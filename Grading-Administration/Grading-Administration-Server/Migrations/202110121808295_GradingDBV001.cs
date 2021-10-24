namespace Grading_Administration_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GradingDBV001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoginDetails",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        Password = c.String(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.UserName)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Email = c.String(),
                        UserType = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.ModuleContributions",
                c => new
                    {
                        ContributionId = c.Int(nullable: false, identity: true),
                        Module_ModuleId = c.Int(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ContributionId)
                .ForeignKey("dbo.Modules", t => t.Module_ModuleId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.Module_ModuleId)
                .Index(t => t.User_UserId);
            
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
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        ModuleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ETC = c.Int(nullable: false),
                        IsNumerical = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ModuleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoginDetails", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.ModuleContributions", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.ModuleContributions", "Module_ModuleId", "dbo.Modules");
            DropForeignKey("dbo.Grades", "Contribution_ContributionId", "dbo.ModuleContributions");
            DropIndex("dbo.Grades", new[] { "Contribution_ContributionId" });
            DropIndex("dbo.ModuleContributions", new[] { "User_UserId" });
            DropIndex("dbo.ModuleContributions", new[] { "Module_ModuleId" });
            DropIndex("dbo.LoginDetails", new[] { "User_UserId" });
            DropTable("dbo.Modules");
            DropTable("dbo.Grades");
            DropTable("dbo.ModuleContributions");
            DropTable("dbo.Users");
            DropTable("dbo.LoginDetails");
        }
    }
}
