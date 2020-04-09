namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class latlng : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GeoLocations", "lat", c => c.String());
            AddColumn("dbo.GeoLocations", "lng", c => c.String());
            DropColumn("dbo.GeoLocations", "latlng");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GeoLocations", "latlng", c => c.String());
            DropColumn("dbo.GeoLocations", "lng");
            DropColumn("dbo.GeoLocations", "lat");
        }
    }
}
