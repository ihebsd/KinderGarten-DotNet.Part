namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyMig : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.Users", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KinderGartens", "DirecteurId", "dbo.Users");
            DropIndex("dbo.KinderGartens", new[] { "DirecteurId" });
            DropColumn("dbo.Users", "Discriminator");
            DropTable("dbo.KinderGartens");
        }
    }
}
