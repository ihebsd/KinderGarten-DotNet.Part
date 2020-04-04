namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mymig2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reputations", "ParentId", "dbo.Users");
            DropIndex("dbo.Reputations", new[] { "ParentId" });
            CreateTable(
                "dbo.FeedBacks",
                c => new
                    {
                        FeedBackId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        FeedBackDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.FeedBackId)
                .ForeignKey("dbo.Users", t => t.ParentId)
                .Index(t => t.ParentId);
            
            DropTable("dbo.Reputations");
            DropTable("dbo.refunds");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.refunds",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        cost = c.Single(nullable: false),
                        email = c.String(maxLength: 255),
                        fullname = c.String(maxLength: 255),
                        idm = c.Int(nullable: false),
                        rattachesultat = c.String(maxLength: 255),
                        stat = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Reputations",
                c => new
                    {
                        ReputationId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ReputationDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        ParentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReputationId);
            
            DropForeignKey("dbo.FeedBacks", "ParentId", "dbo.Users");
            DropIndex("dbo.FeedBacks", new[] { "ParentId" });
            DropTable("dbo.FeedBacks");
            CreateIndex("dbo.Reputations", "ParentId");
            AddForeignKey("dbo.Reputations", "ParentId", "dbo.Users", "idUser", cascadeDelete: true);
        }
    }
}
