namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequestAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "RequestId", c => c.Int(nullable: false));
            CreateIndex("dbo.Accounts", "RequestId");
            AddForeignKey("dbo.Accounts", "RequestId", "dbo.Requests", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "RequestId", "dbo.Requests");
            DropIndex("dbo.Accounts", new[] { "RequestId" });
            DropColumn("dbo.Accounts", "RequestId");
        }
    }
}
