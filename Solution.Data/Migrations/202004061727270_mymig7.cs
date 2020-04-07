namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mymig7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FeedBacks", "sentiment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FeedBacks", "sentiment");
        }
    }
}
