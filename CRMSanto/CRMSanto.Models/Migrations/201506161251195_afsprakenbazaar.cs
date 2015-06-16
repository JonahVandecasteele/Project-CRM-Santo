namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class afsprakenbazaar : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SoortAfspraak", "Naam", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SoortAfspraak", "Naam", c => c.String(nullable: false));
        }
    }
}
