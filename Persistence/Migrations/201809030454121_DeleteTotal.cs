namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteTotal : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Accounts", "Total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
