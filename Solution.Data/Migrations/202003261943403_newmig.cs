namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmig : DbMigration
    {
        public override void Up()
        {
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
                        Parent_idUser = c.Int(),
                    })
                .PrimaryKey(t => t.ComplaintId)
                .ForeignKey("dbo.Users", t => t.ParentId)
                .ForeignKey("dbo.Users", t => t.Parent_idUser)
                .Index(t => t.ParentId)
                .Index(t => t.Parent_idUser);
            
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
                "dbo.Reputations",
                c => new
                    {
                        ReputationId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ReputationDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        ParentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReputationId)
                .ForeignKey("dbo.Users", t => t.ParentId, cascadeDelete: true)
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
            DropForeignKey("dbo.Claims", "Parent_idUser", "dbo.Users");
            DropForeignKey("dbo.KinderGartens", "DirecteurId", "dbo.Users");
            DropForeignKey("dbo.Reputations", "ParentId", "dbo.Users");
            DropForeignKey("dbo.Claims", "ParentId", "dbo.Users");
            DropIndex("dbo.KinderGartens", new[] { "DirecteurId" });
            DropIndex("dbo.Reputations", new[] { "ParentId" });
            DropIndex("dbo.Claims", new[] { "Parent_idUser" });
            DropIndex("dbo.Claims", new[] { "ParentId" });
            DropTable("dbo.KinderGartens");
            DropTable("dbo.Reputations");
            DropTable("dbo.Users");
            DropTable("dbo.Claims");
        }
    }
}
