namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hhh : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.KinderGartens", "Votes");
            DropTable("dbo.VoteLogs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VoteLogs",
                c => new
                    {
                        VoteLogId = c.Int(nullable: false, identity: true),
                        SectionId = c.Int(nullable: false),
                        VoteForId = c.Int(nullable: false),
                        Username = c.String(),
                        Vote = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.VoteLogId);
            
            AddColumn("dbo.KinderGartens", "Votes", c => c.String());
        }
    }
}
