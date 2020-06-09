namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kindergartenazure : DbMigration
    {
        public override void Up()
        {
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
                        Echelon = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.idUser);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Participations", "Parent_idUser", "dbo.Users");
            DropForeignKey("dbo.Participations", "EventId", "dbo.Events");
            DropForeignKey("dbo.ParentEvents", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.ParentEvents", "Parent_idUser", "dbo.Users");
            DropForeignKey("dbo.KinderGartens", "DirecteurId", "dbo.Users");
            DropForeignKey("dbo.Events", "DirecteurFk", "dbo.Users");
            DropIndex("dbo.ParentEvents", new[] { "Event_EventId" });
            DropIndex("dbo.ParentEvents", new[] { "Parent_idUser" });
            DropIndex("dbo.Participations", new[] { "Parent_idUser" });
            DropIndex("dbo.Participations", new[] { "EventId" });
            DropIndex("dbo.KinderGartens", new[] { "DirecteurId" });
            DropIndex("dbo.Events", new[] { "DirecteurFk" });
            DropTable("dbo.ParentEvents");
            DropTable("dbo.Participations");
            DropTable("dbo.KinderGartens");
            DropTable("dbo.Users");
            DropTable("dbo.Events");
        }
    }
}
