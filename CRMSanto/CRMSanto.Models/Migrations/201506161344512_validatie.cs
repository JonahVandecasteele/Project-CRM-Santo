namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validatie : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Klant", "Naam", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Klant", "Voornaam", c => c.String(nullable: false, maxLength: 60));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Klant", "Voornaam", c => c.String(nullable: false));
            AlterColumn("dbo.Klant", "Naam", c => c.String(nullable: false));
        }
    }
}
