namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class daily : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarPools", "Daily", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CarPools", "Weekly", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CarPools", "EveryWeekDay", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CarPools", "EveryWeekDay", c => c.Boolean());
            AlterColumn("dbo.CarPools", "Weekly", c => c.Boolean());
            AlterColumn("dbo.CarPools", "Daily", c => c.Boolean());
        }
    }
}
