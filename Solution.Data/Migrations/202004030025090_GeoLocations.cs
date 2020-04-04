namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GeoLocations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GeoLocations",
                c => new
                    {
                        idGeo = c.Int(nullable: false),
                        Address = c.String(),
                        latlng = c.String(),
                        IdParent = c.Int(),
                    })
                .PrimaryKey(t => t.idGeo)
                .ForeignKey("dbo.Users", t => t.idGeo)
                .Index(t => t.idGeo);
            
            AddColumn("dbo.Users", "idGeo", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GeoLocations", "idGeo", "dbo.Users");
            DropIndex("dbo.GeoLocations", new[] { "idGeo" });
            DropColumn("dbo.Users", "idGeo");
            DropTable("dbo.GeoLocations");
        }
    }
}
