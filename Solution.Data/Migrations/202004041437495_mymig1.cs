namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mymig1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.refunds",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        cost = c.Single(nullable: false),
                        email = c.String(maxLength: 255),
                        fullname = c.String(maxLength: 255),
                        idm = c.Int(nullable: false),
                        rattachesultat = c.String(maxLength: 255),
                        stat = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.refunds");
        }
    }
}
