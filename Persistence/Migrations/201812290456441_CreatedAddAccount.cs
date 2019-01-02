namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedAddAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "CreatedAt");
        }
    }
}
