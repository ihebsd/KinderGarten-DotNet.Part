namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class repmig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reputations",
                c => new
                    {
                        ReputationId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ReputationDate = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ReputationId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Reputations");
        }
    }
}
