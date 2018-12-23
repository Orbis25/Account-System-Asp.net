namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Amount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Debs", "Total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Debs", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Accounts", "Total");
        }
    }
}
