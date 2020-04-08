namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kinder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KinderGartens", "nbVue", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KinderGartens", "nbVue");
        }
    }
}
