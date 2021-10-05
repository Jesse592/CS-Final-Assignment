namespace GradingAdministration_server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GradingDBv003 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ModuleStudent", newName: "ModuleTeacher");
            RenameColumn(table: "dbo.ModuleTeacher", name: "StudentRefID", newName: "TeacherRefID");
            RenameIndex(table: "dbo.ModuleTeacher", name: "IX_StudentRefID", newName: "IX_TeacherRefID");
            AddColumn("dbo.Users", "Module_ModuleID", c => c.Int());
            CreateIndex("dbo.Users", "Module_ModuleID");
            AddForeignKey("dbo.Users", "Module_ModuleID", "dbo.Modules", "ModuleID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Module_ModuleID", "dbo.Modules");
            DropIndex("dbo.Users", new[] { "Module_ModuleID" });
            DropColumn("dbo.Users", "Module_ModuleID");
            RenameIndex(table: "dbo.ModuleTeacher", name: "IX_TeacherRefID", newName: "IX_StudentRefID");
            RenameColumn(table: "dbo.ModuleTeacher", name: "TeacherRefID", newName: "StudentRefID");
            RenameTable(name: "dbo.ModuleTeacher", newName: "ModuleStudent");
        }
    }
}
