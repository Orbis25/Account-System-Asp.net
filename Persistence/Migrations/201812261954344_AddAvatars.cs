namespace Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAvatars : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Avatar", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Avatar");
        }
    }
}
