namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class others : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarPools", "Others", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarPools", "Others");
        }
    }
}
