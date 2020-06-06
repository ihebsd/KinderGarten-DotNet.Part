namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kinder : DbMigration
    {
        public override void Up()
        {
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
                        idKid = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.idParent)
                .ForeignKey("dbo.Kids", t => t.idKid)
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
                        idGeo = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.idUser);
            
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
            DropForeignKey("dbo.CarPools", "idKid", "dbo.Kids");
            DropForeignKey("dbo.Kids", "idParent", "dbo.Users");
            DropForeignKey("dbo.GeoLocations", "idGeo", "dbo.Users");
            DropForeignKey("dbo.CarPools", "idParent", "dbo.Users");
            DropIndex("dbo.KinderGartens", new[] { "DirecteurId" });
            DropIndex("dbo.GeoLocations", new[] { "idGeo" });
            DropIndex("dbo.Kids", new[] { "idParent" });
            DropIndex("dbo.CarPools", new[] { "idKid" });
            DropIndex("dbo.CarPools", new[] { "idParent" });
            DropTable("dbo.KinderGartens");
            DropTable("dbo.GeoLocations");
            DropTable("dbo.Users");
            DropTable("dbo.Kids");
            DropTable("dbo.CarPools");
        }
    }
}
