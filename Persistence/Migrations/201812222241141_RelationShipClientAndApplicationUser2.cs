namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationShipClientAndApplicationUser2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "IdUser", c => c.String());
            AddColumn("dbo.Clients", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Discriminator");
            DropColumn("dbo.Clients", "IdUser");
        }
    }
}
