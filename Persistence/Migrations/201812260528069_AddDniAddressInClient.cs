namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDniAddressInClient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Dni", c => c.String(nullable: false, maxLength: 12));
            AddColumn("dbo.Clients", "Address", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Address");
            DropColumn("dbo.Clients", "Dni");
        }
    }
}
