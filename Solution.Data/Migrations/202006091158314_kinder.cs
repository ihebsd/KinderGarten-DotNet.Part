namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kinder : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Dislikes",
                c => new
                    {
                        DislikeId = c.Int(nullable: false, identity: true),
                        ParentDislike = c.Int(nullable: false),
                        PublicationDislike = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DislikeId)
                .ForeignKey("dbo.Users", t => t.ParentDislike)
                .ForeignKey("dbo.Publications", t => t.PublicationDislike)
                .Index(t => t.ParentDislike)
                .Index(t => t.PublicationDislike);
            
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
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.idUser);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        LikeId = c.Int(nullable: false, identity: true),
                        ParentLike = c.Int(nullable: false),
                        PublicationLike = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LikeId)
                .ForeignKey("dbo.Users", t => t.ParentLike)
                .ForeignKey("dbo.Publications", t => t.PublicationLike)
                .Index(t => t.ParentLike)
                .Index(t => t.PublicationLike);
            
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
            DropForeignKey("dbo.Comments", "PublicationFK", "dbo.Publications");
            DropForeignKey("dbo.Publications", "ParentFk", "dbo.Users");
            DropForeignKey("dbo.Likes", "PublicationLike", "dbo.Publications");
            DropForeignKey("dbo.Dislikes", "PublicationDislike", "dbo.Publications");
            DropForeignKey("dbo.Likes", "ParentLike", "dbo.Users");
            DropForeignKey("dbo.Dislikes", "ParentDislike", "dbo.Users");
            DropIndex("dbo.KinderGartens", new[] { "DirecteurId" });
            DropIndex("dbo.Likes", new[] { "PublicationLike" });
            DropIndex("dbo.Likes", new[] { "ParentLike" });
            DropIndex("dbo.Dislikes", new[] { "PublicationDislike" });
            DropIndex("dbo.Dislikes", new[] { "ParentDislike" });
            DropIndex("dbo.Publications", new[] { "ParentFk" });
            DropIndex("dbo.Comments", new[] { "PublicationFK" });
            DropTable("dbo.KinderGartens");
            DropTable("dbo.Conversations");
            DropTable("dbo.Likes");
            DropTable("dbo.Users");
            DropTable("dbo.Dislikes");
            DropTable("dbo.Publications");
            DropTable("dbo.Comments");
        }
    }
}
