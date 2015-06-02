namespace CRMSanto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productenaanpassen2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "AankoopPrijs", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Product", "VerkoopPrijs", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Product", "Omschrijving", c => c.String());
            DropColumn("dbo.Product", "Prijs");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "Prijs", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Product", "Omschrijving");
            DropColumn("dbo.Product", "VerkoopPrijs");
            DropColumn("dbo.Product", "AankoopPrijs");
        }
    }
}
