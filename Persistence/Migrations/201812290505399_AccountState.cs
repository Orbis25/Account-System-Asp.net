namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountState : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "State", c => c.Boolean(nullable: false));
            DropColumn("dbo.Accounts", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "Time", c => c.DateTime(nullable: false));
            DropColumn("dbo.Accounts", "State");
        }
    }
}
