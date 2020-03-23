namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pooool : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarPools", "Time", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CarPools", "Time", c => c.Time(nullable: false, precision: 7));
        }
    }
}
