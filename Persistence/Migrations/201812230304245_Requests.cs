namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Requests : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requests", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Requests", new[] { "ApplicationUserId" });
            AddColumn("dbo.Requests", "Descripcion", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Requests", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Requests", "ApplicationUserId");
            AddForeignKey("dbo.Requests", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Requests", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Requests", "ApplicationUserId", c => c.String(maxLength: 128));
            DropColumn("dbo.Requests", "Descripcion");
            CreateIndex("dbo.Requests", "ApplicationUserId");
            AddForeignKey("dbo.Requests", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
