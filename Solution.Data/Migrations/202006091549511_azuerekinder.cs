namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class azuerekinder : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.ComplaintId)
                .ForeignKey("dbo.Users", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        idUser = c.Int(nullable: false, identity: true),
                        nom = c.String(nullable: false, maxLength: 30),
                        prenom = c.String(nullable: false, maxLength: 30),
                        login = c.String(nullable: false, maxLength: 30),
                        email = c.String(nullable: false),
                        password = c.String(nullable: false),
                        Confirmpassword = c.String(nullable: false),
                        IsEmailVerified = c.Boolean(nullable: false),
                        ActivationCode = c.Guid(nullable: false),
                        ResetPasswordCode = c.String(),
                        role = c.Int(nullable: false),
                        Ban = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.idUser);
            
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
                .PrimaryKey(t => t.FeedBackId)
                .ForeignKey("dbo.Users", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.KinderGartens",
                c => new
                    {
                        KinderGartenId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Description = c.String(nullable: false, maxLength: 30),
                        Address = c.String(nullable: false, maxLength: 30),
                        Cost = c.Single(nullable: false),
                        Phone = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        Image = c.String(nullable: false, maxLength: 30),
                        NbrEmp = c.Int(nullable: false),
                        DirecteurId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KinderGartenId)
                .ForeignKey("dbo.Users", t => t.DirecteurId, cascadeDelete: true)
                .Index(t => t.DirecteurId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KinderGartens", "DirecteurId", "dbo.Users");
            DropForeignKey("dbo.FeedBacks", "ParentId", "dbo.Users");
            DropForeignKey("dbo.Claims", "ParentId", "dbo.Users");
            DropIndex("dbo.KinderGartens", new[] { "DirecteurId" });
            DropIndex("dbo.FeedBacks", new[] { "ParentId" });
            DropIndex("dbo.Claims", new[] { "ParentId" });
            DropTable("dbo.KinderGartens");
            DropTable("dbo.FeedBacks");
            DropTable("dbo.Users");
            DropTable("dbo.Claims");
            DropTable("dbo.AdminNotifs");
        }
    }
}
