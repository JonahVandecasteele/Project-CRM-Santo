namespace CRMSanto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rebuilddatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Klant", "Mutualiteit_ID", "dbo.Mutualiteit");
            DropForeignKey("dbo.Klant", "Werksituatie_ID", "dbo.Werksituatie");
            DropIndex("dbo.Klant", new[] { "Mutualiteit_ID" });
            DropIndex("dbo.Klant", new[] { "Werksituatie_ID" });
            AddColumn("dbo.Adres", "Gemeente_ID", c => c.Int());
            CreateIndex("dbo.Adres", "Gemeente_ID");
            AddForeignKey("dbo.Adres", "Gemeente_ID", "dbo.Gemeente", "ID");
            DropColumn("dbo.Adres", "Gemeente");
            DropColumn("dbo.Klant", "Hobby");
            DropColumn("dbo.Klant", "Medicatie");
            DropColumn("dbo.Klant", "Spijsvertering");
            DropColumn("dbo.Klant", "Allergie");
            DropColumn("dbo.Klant", "Blessure");
            DropColumn("dbo.Klant", "OperatiesZiektes");
            DropColumn("dbo.Klant", "Voedingssupplementen");
            DropColumn("dbo.Klant", "GezinRelatie");
            DropColumn("dbo.Klant", "Voedingspatroon");
            DropColumn("dbo.Klant", "HuidigeKlachten");
            DropColumn("dbo.Klant", "Mutualiteit_ID");
            DropColumn("dbo.Klant", "Werksituatie_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Klant", "Werksituatie_ID", c => c.Int());
            AddColumn("dbo.Klant", "Mutualiteit_ID", c => c.Int());
            AddColumn("dbo.Klant", "HuidigeKlachten", c => c.String());
            AddColumn("dbo.Klant", "Voedingspatroon", c => c.String());
            AddColumn("dbo.Klant", "GezinRelatie", c => c.String());
            AddColumn("dbo.Klant", "Voedingssupplementen", c => c.String());
            AddColumn("dbo.Klant", "OperatiesZiektes", c => c.String());
            AddColumn("dbo.Klant", "Blessure", c => c.String());
            AddColumn("dbo.Klant", "Allergie", c => c.String());
            AddColumn("dbo.Klant", "Spijsvertering", c => c.String());
            AddColumn("dbo.Klant", "Medicatie", c => c.String());
            AddColumn("dbo.Klant", "Hobby", c => c.String());
            AddColumn("dbo.Adres", "Gemeente", c => c.String());
            DropForeignKey("dbo.Adres", "Gemeente_ID", "dbo.Gemeente");
            DropIndex("dbo.Adres", new[] { "Gemeente_ID" });
            DropColumn("dbo.Adres", "Gemeente_ID");
            CreateIndex("dbo.Klant", "Werksituatie_ID");
            CreateIndex("dbo.Klant", "Mutualiteit_ID");
            AddForeignKey("dbo.Klant", "Werksituatie_ID", "dbo.Werksituatie", "ID");
            AddForeignKey("dbo.Klant", "Mutualiteit_ID", "dbo.Mutualiteit", "ID");
        }
    }
}
