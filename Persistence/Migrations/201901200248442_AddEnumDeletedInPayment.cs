namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEnumDeletedInPayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "Deleted", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "Deleted");
        }
    }
}
