namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newtemp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Claims", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Claims", "Name");
        }
    }
}
