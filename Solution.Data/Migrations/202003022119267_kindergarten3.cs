namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kindergarten3 : DbMigration
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
                        Description = c.String(),
                        image = c.String(),
                        AdminConfirmtion = c.Boolean(nullable: false),
                        DirecteurFk = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.Users", t => t.DirecteurFk, cascadeDelete: true)
                .Index(t => t.DirecteurFk);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "DirecteurFk", "dbo.Users");
            DropIndex("dbo.Events", new[] { "DirecteurFk" });
            DropTable("dbo.Events");
        }
    }
}
