namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventmiggg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "HeureD", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "HeureF", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "HeureF");
            DropColumn("dbo.Events", "HeureD");
        }
    }
}
