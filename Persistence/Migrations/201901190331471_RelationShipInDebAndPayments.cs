namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationShipInDebAndPayments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payments", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Payments", new[] { "AccountId" });
            AddColumn("dbo.Payments", "DebId", c => c.Int(nullable: false));
            CreateIndex("dbo.Payments", "DebId");
            AddForeignKey("dbo.Payments", "DebId", "dbo.Debs", "Id", cascadeDelete: true);
            DropColumn("dbo.Payments", "AccountId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "AccountId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Payments", "DebId", "dbo.Debs");
            DropIndex("dbo.Payments", new[] { "DebId" });
            DropColumn("dbo.Payments", "DebId");
            CreateIndex("dbo.Payments", "AccountId");
            AddForeignKey("dbo.Payments", "AccountId", "dbo.Accounts", "Id", cascadeDelete: true);
        }
    }
}
