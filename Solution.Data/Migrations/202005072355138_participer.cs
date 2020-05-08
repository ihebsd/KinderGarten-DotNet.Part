namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class participer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Participation",
                c => new
                    {
                        EventId = c.Int(nullable: false),
                        ParentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EventId, t.ParentId })
                .ForeignKey("dbo.Events", t => t.EventId)
                .ForeignKey("dbo.Users", t => t.ParentId)
                .Index(t => t.EventId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Participation", "ParentId", "dbo.Users");
            DropForeignKey("dbo.Participation", "EventId", "dbo.Events");
            DropIndex("dbo.Participation", new[] { "ParentId" });
            DropIndex("dbo.Participation", new[] { "EventId" });
            DropTable("dbo.Participation");
        }
    }
}
