namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seconmig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Echelon", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Echelon");
        }
    }
}
