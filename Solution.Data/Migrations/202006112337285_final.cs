namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final : DbMigration
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
                "dbo.CarPools",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        From = c.String(nullable: false),
                        To = c.String(nullable: false),
                        Time = c.DateTime(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Message = c.String(nullable: false),
                        NbPlaceDispo = c.Int(nullable: false),
                        Daily = c.Boolean(nullable: false),
                        Weekly = c.Boolean(nullable: false),
                        EveryWeekDay = c.Boolean(nullable: false),
                        Others = c.Boolean(nullable: false),
                        UntilDate = c.DateTime(),
                        idParent = c.Int(),
                        idKid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.idParent)
                .ForeignKey("dbo.Kids", t => t.idKid, cascadeDelete: true)
                .Index(t => t.idParent)
                .Index(t => t.idKid);
            
            CreateTable(
                "dbo.Kids",
                c => new
                    {
                        IdKid = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Age = c.Int(nullable: false),
                        IsCheked = c.Boolean(nullable: false),
                        idParent = c.Int(),
                    })
                .PrimaryKey(t => t.IdKid)
                .ForeignKey("dbo.Users", t => t.idParent)
                .Index(t => t.idParent);
            
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
                        idGeo = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.idUser);
            
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
                        nbVue = c.Int(nullable: false),
                        Votes = c.String(),
                        longitude = c.String(),
                        latitude = c.String(),
                        DirecteurId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KinderGartenId)
                .ForeignKey("dbo.Users", t => t.DirecteurId, cascadeDelete: true)
                .Index(t => t.DirecteurId);
            
            CreateTable(
                "dbo.Dislikes",
                c => new
                    {
                        DislikeId = c.Int(nullable: false, identity: true),
                        ParentDislike = c.Int(nullable: false),
                        PublicationDislike = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DislikeId)
                .ForeignKey("dbo.Publications", t => t.PublicationDislike)
                .ForeignKey("dbo.Users", t => t.ParentDislike)
                .Index(t => t.ParentDislike)
                .Index(t => t.PublicationDislike);
            
            CreateTable(
                "dbo.Publications",
                c => new
                    {
                        PublicationId = c.Int(nullable: false, identity: true),
                        titlePub = c.String(),
                        descriptionPub = c.String(),
                        imagePub = c.String(),
                        datePub = c.DateTime(nullable: false),
                        nbLike = c.Int(nullable: false),
                        nbVue = c.Int(nullable: false),
                        nbDislike = c.Int(nullable: false),
                        Like = c.Boolean(nullable: false),
                        Dislike = c.Boolean(nullable: false),
                        file = c.String(),
                        video = c.String(),
                        ParentFk = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PublicationId)
                .ForeignKey("dbo.Users", t => t.ParentFk, cascadeDelete: true)
                .Index(t => t.ParentFk);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        post = c.String(),
                        imageCom = c.String(),
                        dateCom = c.DateTime(nullable: false),
                        nomUser = c.String(),
                        PublicationFK = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Publications", t => t.PublicationFK, cascadeDelete: true)
                .Index(t => t.PublicationFK);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        LikeId = c.Int(nullable: false, identity: true),
                        ParentLike = c.Int(nullable: false),
                        PublicationLike = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LikeId)
                .ForeignKey("dbo.Publications", t => t.PublicationLike)
                .ForeignKey("dbo.Users", t => t.ParentLike)
                .Index(t => t.ParentLike)
                .Index(t => t.PublicationLike);
            
            CreateTable(
                "dbo.GeoLocations",
                c => new
                    {
                        idGeo = c.Int(nullable: false),
                        Address = c.String(),
                        lat = c.String(),
                        lng = c.String(),
                        ParentFK = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idGeo)
                .ForeignKey("dbo.Users", t => t.idGeo)
                .Index(t => t.idGeo);
            
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        sender_id = c.Int(nullable: false),
                        receiver_id = c.Int(nullable: false),
                        message = c.String(),
                        status = c.Int(nullable: false),
                        created_at = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
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
                "dbo.VoteLogs",
                c => new
                    {
                        AutoId = c.Int(nullable: false, identity: true),
                        SectionId = c.Short(nullable: false),
                        VoteForId = c.Int(nullable: false),
                        UserName = c.String(),
                        Vote = c.Short(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AutoId);
            
            CreateTable(
                "dbo.EventParents",
                c => new
                    {
                        Event_EventId = c.Int(nullable: false),
                        Parent_idUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Event_EventId, t.Parent_idUser })
                .ForeignKey("dbo.Events", t => t.Event_EventId)
                .ForeignKey("dbo.Users", t => t.Parent_idUser)
                .Index(t => t.Event_EventId)
                .Index(t => t.Parent_idUser);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Participations", "Parent_idUser", "dbo.Users");
            DropForeignKey("dbo.Participations", "EventId", "dbo.Events");
            DropForeignKey("dbo.CarPools", "idKid", "dbo.Kids");
            DropForeignKey("dbo.Likes", "ParentLike", "dbo.Users");
            DropForeignKey("dbo.Kids", "idParent", "dbo.Users");
            DropForeignKey("dbo.GeoLocations", "idGeo", "dbo.Users");
            DropForeignKey("dbo.Dislikes", "ParentDislike", "dbo.Users");
            DropForeignKey("dbo.Publications", "ParentFk", "dbo.Users");
            DropForeignKey("dbo.Likes", "PublicationLike", "dbo.Publications");
            DropForeignKey("dbo.Dislikes", "PublicationDislike", "dbo.Publications");
            DropForeignKey("dbo.Comments", "PublicationFK", "dbo.Publications");
            DropForeignKey("dbo.KinderGartens", "DirecteurId", "dbo.Users");
            DropForeignKey("dbo.Events", "DirecteurFk", "dbo.Users");
            DropForeignKey("dbo.EventParents", "Parent_idUser", "dbo.Users");
            DropForeignKey("dbo.EventParents", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.FeedBacks", "ParentId", "dbo.Users");
            DropForeignKey("dbo.Claims", "ParentId", "dbo.Users");
            DropForeignKey("dbo.CarPools", "idParent", "dbo.Users");
            DropIndex("dbo.EventParents", new[] { "Parent_idUser" });
            DropIndex("dbo.EventParents", new[] { "Event_EventId" });
            DropIndex("dbo.Participations", new[] { "Parent_idUser" });
            DropIndex("dbo.Participations", new[] { "EventId" });
            DropIndex("dbo.GeoLocations", new[] { "idGeo" });
            DropIndex("dbo.Likes", new[] { "PublicationLike" });
            DropIndex("dbo.Likes", new[] { "ParentLike" });
            DropIndex("dbo.Comments", new[] { "PublicationFK" });
            DropIndex("dbo.Publications", new[] { "ParentFk" });
            DropIndex("dbo.Dislikes", new[] { "PublicationDislike" });
            DropIndex("dbo.Dislikes", new[] { "ParentDislike" });
            DropIndex("dbo.KinderGartens", new[] { "DirecteurId" });
            DropIndex("dbo.Events", new[] { "DirecteurFk" });
            DropIndex("dbo.FeedBacks", new[] { "ParentId" });
            DropIndex("dbo.Claims", new[] { "ParentId" });
            DropIndex("dbo.Kids", new[] { "idParent" });
            DropIndex("dbo.CarPools", new[] { "idKid" });
            DropIndex("dbo.CarPools", new[] { "idParent" });
            DropTable("dbo.EventParents");
            DropTable("dbo.VoteLogs");
            DropTable("dbo.Participations");
            DropTable("dbo.Conversations");
            DropTable("dbo.GeoLocations");
            DropTable("dbo.Likes");
            DropTable("dbo.Comments");
            DropTable("dbo.Publications");
            DropTable("dbo.Dislikes");
            DropTable("dbo.KinderGartens");
            DropTable("dbo.Events");
            DropTable("dbo.FeedBacks");
            DropTable("dbo.Claims");
            DropTable("dbo.Users");
            DropTable("dbo.Kids");
            DropTable("dbo.CarPools");
            DropTable("dbo.AdminNotifs");
        }
    }
}
