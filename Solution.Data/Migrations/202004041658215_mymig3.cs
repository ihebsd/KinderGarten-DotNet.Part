namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mymig3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.FeedBacks", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FeedBacks", "Name", c => c.String(nullable: false));
        }
    }
}
