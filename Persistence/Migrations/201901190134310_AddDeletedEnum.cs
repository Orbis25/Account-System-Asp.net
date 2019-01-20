namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeletedEnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Debs", "Deleted", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Debs", "Deleted");
        }
    }
}
