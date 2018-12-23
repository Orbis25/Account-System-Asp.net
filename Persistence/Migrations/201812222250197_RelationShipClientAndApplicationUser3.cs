namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationShipClientAndApplicationUser3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Clients", "IdUser");
            DropColumn("dbo.Clients", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Clients", "IdUser", c => c.String());
        }
    }
}
