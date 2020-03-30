namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WEBTEST : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarPools", "Daily", c => c.Boolean());
            AlterColumn("dbo.CarPools", "Weekly", c => c.Boolean());
            AlterColumn("dbo.CarPools", "EveryWeekDay", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CarPools", "EveryWeekDay", c => c.String());
            AlterColumn("dbo.CarPools", "Weekly", c => c.String());
            AlterColumn("dbo.CarPools", "Daily", c => c.String());
        }
    }
}
