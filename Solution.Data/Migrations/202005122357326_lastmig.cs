namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lastmig : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "HeureD", c => c.String());
            AlterColumn("dbo.Events", "HeureF", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "HeureF", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Events", "HeureD", c => c.DateTime(nullable: false));
        }
    }
}
