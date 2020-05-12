namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class participer2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Participation", newName: "ParentEvents");
            RenameColumn(table: "dbo.ParentEvents", name: "EventId", newName: "Event_EventId");
            RenameColumn(table: "dbo.ParentEvents", name: "ParentId", newName: "Parent_idUser");
            RenameIndex(table: "dbo.ParentEvents", name: "IX_ParentId", newName: "IX_Parent_idUser");
            RenameIndex(table: "dbo.ParentEvents", name: "IX_EventId", newName: "IX_Event_EventId");
            DropPrimaryKey("dbo.ParentEvents");
            CreateTable(
                "dbo.Participations",
                c => new
                    {
                        EventId = c.Int(nullable: false),
                        ParentId = c.Int(nullable: false),
                        Parent_idUser = c.Int(),
                    })
                .PrimaryKey(t => new { t.EventId, t.ParentId })
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Parent_idUser)
                .Index(t => t.EventId)
                .Index(t => t.Parent_idUser);
            
            AddPrimaryKey("dbo.ParentEvents", new[] { "Parent_idUser", "Event_EventId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Participations", "Parent_idUser", "dbo.Users");
            DropForeignKey("dbo.Participations", "EventId", "dbo.Events");
            DropIndex("dbo.Participations", new[] { "Parent_idUser" });
            DropIndex("dbo.Participations", new[] { "EventId" });
            DropPrimaryKey("dbo.ParentEvents");
            DropTable("dbo.Participations");
            AddPrimaryKey("dbo.ParentEvents", new[] { "EventId", "ParentId" });
            RenameIndex(table: "dbo.ParentEvents", name: "IX_Event_EventId", newName: "IX_EventId");
            RenameIndex(table: "dbo.ParentEvents", name: "IX_Parent_idUser", newName: "IX_ParentId");
            RenameColumn(table: "dbo.ParentEvents", name: "Parent_idUser", newName: "ParentId");
            RenameColumn(table: "dbo.ParentEvents", name: "Event_EventId", newName: "EventId");
            RenameTable(name: "dbo.ParentEvents", newName: "Participation");
        }
    }
}
