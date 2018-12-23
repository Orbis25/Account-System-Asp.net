namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveClient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String(nullable: true, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            DropColumn("dbo.AspNetUsers", "LastName");
        }
    }
}
