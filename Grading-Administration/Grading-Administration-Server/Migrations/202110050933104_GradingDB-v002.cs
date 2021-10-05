namespace GradingAdministration_server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GradingDBv002 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ModuleStudent",
                c => new
                    {
                        ModuleRefID = c.Int(nullable: false),
                        StudentRefID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ModuleRefID, t.StudentRefID })
                .ForeignKey("dbo.Modules", t => t.ModuleRefID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.StudentRefID, cascadeDelete: true)
                .Index(t => t.ModuleRefID)
                .Index(t => t.StudentRefID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ModuleStudent", "StudentRefID", "dbo.Users");
            DropForeignKey("dbo.ModuleStudent", "ModuleRefID", "dbo.Modules");
            DropIndex("dbo.ModuleStudent", new[] { "StudentRefID" });
            DropIndex("dbo.ModuleStudent", new[] { "ModuleRefID" });
            DropTable("dbo.ModuleStudent");
        }
    }
}
