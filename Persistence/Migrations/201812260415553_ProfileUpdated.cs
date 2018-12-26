namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfileUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "ProfileUpdated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "ProfileUpdated");
        }
    }
}
