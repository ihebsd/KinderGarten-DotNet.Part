namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mymig5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AdminNotifs", "UserId", "dbo.Users");
            DropIndex("dbo.AdminNotifs", new[] { "UserId" });
            AddColumn("dbo.AdminNotifs", "username", c => c.String());
            AlterColumn("dbo.AdminNotifs", "userid", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AdminNotifs", "userid", c => c.Int());
            DropColumn("dbo.AdminNotifs", "username");
            CreateIndex("dbo.AdminNotifs", "UserId");
            AddForeignKey("dbo.AdminNotifs", "UserId", "dbo.Users", "idUser");
        }
    }
}
