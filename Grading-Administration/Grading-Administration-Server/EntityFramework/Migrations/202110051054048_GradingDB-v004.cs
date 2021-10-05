namespace GradingAdministration_server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GradingDBv004 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Module_ModuleID", "dbo.Modules");
            DropForeignKey("dbo.ModuleTeacher", "ModuleRefID", "dbo.Modules");
            DropForeignKey("dbo.ModuleTeacher", "TeacherRefID", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Module_ModuleID" });
            DropIndex("dbo.ModuleTeacher", new[] { "ModuleRefID" });
            DropIndex("dbo.ModuleTeacher", new[] { "TeacherRefID" });
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
            
            DropColumn("dbo.Users", "Module_ModuleID");
            DropTable("dbo.ModuleTeacher");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ModuleTeacher",
                c => new
                    {
                        ModuleRefID = c.Int(nullable: false),
                        TeacherRefID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ModuleRefID, t.TeacherRefID });
            
            AddColumn("dbo.Users", "Module_ModuleID", c => c.Int());
            DropForeignKey("dbo.ModuleContributions", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.ModuleContributions", "Module_ModuleId", "dbo.Modules");
            DropIndex("dbo.ModuleContributions", new[] { "User_UserId" });
            DropIndex("dbo.ModuleContributions", new[] { "Module_ModuleId" });
            DropTable("dbo.ModuleContributions");
            CreateIndex("dbo.ModuleTeacher", "TeacherRefID");
            CreateIndex("dbo.ModuleTeacher", "ModuleRefID");
            CreateIndex("dbo.Users", "Module_ModuleID");
            AddForeignKey("dbo.ModuleTeacher", "TeacherRefID", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.ModuleTeacher", "ModuleRefID", "dbo.Modules", "ModuleID", cascadeDelete: true);
            AddForeignKey("dbo.Users", "Module_ModuleID", "dbo.Modules", "ModuleID");
        }
    }
}
