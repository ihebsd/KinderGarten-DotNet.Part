namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class geoo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "idGeo");
            DropColumn("dbo.GeoLocations", "IdParent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GeoLocations", "IdParent", c => c.Int());
            AddColumn("dbo.Users", "idGeo", c => c.Int());
        }
    }
}
