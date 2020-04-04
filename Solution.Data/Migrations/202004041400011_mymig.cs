namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mymig : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Claims", "Parent_idUser", "dbo.Users");
            DropIndex("dbo.Claims", new[] { "Parent_idUser" });
            DropColumn("dbo.Claims", "Parent_idUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Claims", "Parent_idUser", c => c.Int());
            CreateIndex("dbo.Claims", "Parent_idUser");
            AddForeignKey("dbo.Claims", "Parent_idUser", "dbo.Users", "idUser");
        }
    }
}
