namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mymig4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Ban", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Ban");
        }
    }
}
