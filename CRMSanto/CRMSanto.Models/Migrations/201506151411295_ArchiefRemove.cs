namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArchiefRemove : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Afspraak", "Archief", c => c.Boolean(nullable: false));
            DropColumn("dbo.Afspraak", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Afspraak", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Afspraak", "Archief");
        }
    }
}
