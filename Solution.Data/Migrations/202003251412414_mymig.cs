namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mymig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reputations", "ParentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reputations", "ParentId");
            AddForeignKey("dbo.Reputations", "ParentId", "dbo.Users", "idUser", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reputations", "ParentId", "dbo.Users");
            DropIndex("dbo.Reputations", new[] { "ParentId" });
            DropColumn("dbo.Reputations", "ParentId");
        }
    }
}
