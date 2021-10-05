namespace GradingAdministration_server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GradingDBv001 : DbMigration
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
                "dbo.Modules",
                c => new
                    {
                        ModuleID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ETC = c.Int(nullable: false),
                        IsNumerical = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ModuleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoginDetails", "User_UserId", "dbo.Users");
            DropIndex("dbo.LoginDetails", new[] { "User_UserId" });
            DropTable("dbo.Modules");
            DropTable("dbo.Users");
            DropTable("dbo.LoginDetails");
        }
    }
}
