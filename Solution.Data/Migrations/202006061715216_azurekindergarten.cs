namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class azurekindergarten : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Claims", "ParentId", "dbo.Users");
            DropForeignKey("dbo.FeedBacks", "ParentId", "dbo.Users");
            DropIndex("dbo.Claims", new[] { "ParentId" });
            DropIndex("dbo.FeedBacks", new[] { "ParentId" });
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Category = c.Int(nullable: false),
                        number_P = c.Int(nullable: false),
                        DateEvent = c.DateTime(nullable: false),
                        HeureD = c.String(),
                        HeureF = c.String(),
                        Description = c.String(),
                        image = c.String(),
                        qrCode = c.String(),
                        AdminConfirmtion = c.Boolean(nullable: false),
                        DirecteurFk = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.Users", t => t.DirecteurFk, cascadeDelete: true)
                .Index(t => t.DirecteurFk);
            
            CreateTable(
                "dbo.Participations",
                c => new
                    {
                        EventId = c.Int(nullable: false),
                        ParentId = c.Int(nullable: false),
                        Parent_idUser = c.Int(),
                    })
                .PrimaryKey(t => new { t.EventId, t.ParentId })
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Parent_idUser)
                .Index(t => t.EventId)
                .Index(t => t.Parent_idUser);
            
            CreateTable(
                "dbo.ParentEvents",
                c => new
                    {
                        Parent_idUser = c.Int(nullable: false),
                        Event_EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Parent_idUser, t.Event_EventId })
                .ForeignKey("dbo.Users", t => t.Parent_idUser)
                .ForeignKey("dbo.Events", t => t.Event_EventId)
                .Index(t => t.Parent_idUser)
                .Index(t => t.Event_EventId);
            
            AddColumn("dbo.Users", "Echelon", c => c.Int());
            DropColumn("dbo.Users", "Ban");
            DropTable("dbo.AdminNotifs");
            DropTable("dbo.Claims");
            DropTable("dbo.FeedBacks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FeedBacks",
                c => new
                    {
                        FeedBackId = c.Int(nullable: false, identity: true),
                        FeedBackDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        sentiment = c.String(),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.FeedBackId);
            
            CreateTable(
                "dbo.Claims",
                c => new
                    {
                        ComplaintId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(nullable: false),
                        ClaimDate = c.DateTime(nullable: false),
                        ParentId = c.Int(),
                        ClaimType = c.String(),
                        status = c.String(),
                    })
                .PrimaryKey(t => t.ComplaintId);
            
            CreateTable(
                "dbo.AdminNotifs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        msg = c.String(),
                        Datenotif = c.DateTime(nullable: false),
                        username = c.String(),
                        userid = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "Ban", c => c.Int(nullable: false));
            DropForeignKey("dbo.Participations", "Parent_idUser", "dbo.Users");
            DropForeignKey("dbo.Participations", "EventId", "dbo.Events");
            DropForeignKey("dbo.ParentEvents", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.ParentEvents", "Parent_idUser", "dbo.Users");
            DropForeignKey("dbo.Events", "DirecteurFk", "dbo.Users");
            DropIndex("dbo.ParentEvents", new[] { "Event_EventId" });
            DropIndex("dbo.ParentEvents", new[] { "Parent_idUser" });
            DropIndex("dbo.Participations", new[] { "Parent_idUser" });
            DropIndex("dbo.Participations", new[] { "EventId" });
            DropIndex("dbo.Events", new[] { "DirecteurFk" });
            DropColumn("dbo.Users", "Echelon");
            DropTable("dbo.ParentEvents");
            DropTable("dbo.Participations");
            DropTable("dbo.Events");
            CreateIndex("dbo.FeedBacks", "ParentId");
            CreateIndex("dbo.Claims", "ParentId");
            AddForeignKey("dbo.FeedBacks", "ParentId", "dbo.Users", "idUser");
            AddForeignKey("dbo.Claims", "ParentId", "dbo.Users", "idUser");
        }
    }
}
