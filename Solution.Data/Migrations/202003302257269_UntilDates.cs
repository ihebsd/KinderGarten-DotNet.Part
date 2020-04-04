namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UntilDates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarPools", "NbPlaceDispo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarPools", "NbPlaceDispo");
        }
    }
}
