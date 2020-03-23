namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dead : DbMigration
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
                        Time = c.Time(nullable: false, precision: 7),
                        Date = c.DateTime(nullable: false),
                        Message = c.String(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KinderGartens", "DirecteurId", "dbo.Users");
            DropForeignKey("dbo.CarPools", "idKid", "dbo.Kids");
            DropForeignKey("dbo.Kids", "idParent", "dbo.Users");
            DropForeignKey("dbo.CarPools", "idParent", "dbo.Users");
            DropIndex("dbo.KinderGartens", new[] { "DirecteurId" });
            DropIndex("dbo.Kids", new[] { "idParent" });
            DropIndex("dbo.CarPools", new[] { "idKid" });
            DropIndex("dbo.CarPools", new[] { "idParent" });
            DropTable("dbo.KinderGartens");
            DropTable("dbo.Users");
            DropTable("dbo.Kids");
            DropTable("dbo.CarPools");
        }
    }
}
