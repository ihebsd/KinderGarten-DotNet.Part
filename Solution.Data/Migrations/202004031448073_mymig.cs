namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mymig : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Claims", name: "ParentId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Claims", name: "Parent_idUser", newName: "ParentId");
            RenameColumn(table: "dbo.Claims", name: "__mig_tmp__0", newName: "Parent_idUser");
            RenameIndex(table: "dbo.Claims", name: "IX_Parent_idUser", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.Claims", name: "IX_ParentId", newName: "IX_Parent_idUser");
            RenameIndex(table: "dbo.Claims", name: "__mig_tmp__0", newName: "IX_ParentId");
            AlterColumn("dbo.AdminNotifs", "UserId", c => c.Int());
            CreateIndex("dbo.AdminNotifs", "UserId");
            AddForeignKey("dbo.AdminNotifs", "UserId", "dbo.Users", "idUser");
            DropColumn("dbo.AdminNotifs", "username");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdminNotifs", "username", c => c.String());
            DropForeignKey("dbo.AdminNotifs", "UserId", "dbo.Users");
            DropIndex("dbo.AdminNotifs", new[] { "UserId" });
            AlterColumn("dbo.AdminNotifs", "UserId", c => c.String());
            RenameIndex(table: "dbo.Claims", name: "IX_ParentId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.Claims", name: "IX_Parent_idUser", newName: "IX_ParentId");
            RenameIndex(table: "dbo.Claims", name: "__mig_tmp__0", newName: "IX_Parent_idUser");
            RenameColumn(table: "dbo.Claims", name: "Parent_idUser", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Claims", name: "ParentId", newName: "Parent_idUser");
            RenameColumn(table: "dbo.Claims", name: "__mig_tmp__0", newName: "ParentId");
        }
    }
}
