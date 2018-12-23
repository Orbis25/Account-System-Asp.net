namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationShipClientAndApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Clients", "ApplicationUserId");
            AddForeignKey("dbo.Clients", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Clients", new[] { "ApplicationUserId" });
            DropColumn("dbo.Clients", "ApplicationUserId");
        }
    }
}
