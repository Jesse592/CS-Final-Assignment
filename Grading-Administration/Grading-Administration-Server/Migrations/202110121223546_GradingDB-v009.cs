namespace GradingAdministration_server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GradingDBv009 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "UserType", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "UserType", c => c.Int(nullable: false));
        }
    }
}
