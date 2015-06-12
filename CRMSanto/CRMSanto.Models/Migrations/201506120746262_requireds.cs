namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requireds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Afspraak", "Klant_ID", "dbo.Klant");
            DropForeignKey("dbo.Afspraak", "Masseur_ID", "dbo.Masseur");
            DropForeignKey("dbo.Afspraak", "SoortAfspraak_ID", "dbo.SoortAfspraak");
            DropIndex("dbo.Afspraak", new[] { "Klant_ID" });
            DropIndex("dbo.Afspraak", new[] { "Masseur_ID" });
            DropIndex("dbo.Afspraak", new[] { "SoortAfspraak_ID" });
            AlterColumn("dbo.Afspraak", "Klant_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Afspraak", "Masseur_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Afspraak", "SoortAfspraak_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Klant", "Naam", c => c.String(nullable: false));
            AlterColumn("dbo.Klant", "Voornaam", c => c.String(nullable: false));
            AlterColumn("dbo.Werksituatie", "Naam", c => c.String(nullable: false));
            AlterColumn("dbo.Masseur", "Naam", c => c.String(nullable: false));
            AlterColumn("dbo.SoortAfspraak", "Naam", c => c.String(nullable: false));
            CreateIndex("dbo.Afspraak", "Klant_ID");
            CreateIndex("dbo.Afspraak", "Masseur_ID");
            CreateIndex("dbo.Afspraak", "SoortAfspraak_ID");
            AddForeignKey("dbo.Afspraak", "Klant_ID", "dbo.Klant", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Afspraak", "Masseur_ID", "dbo.Masseur", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Afspraak", "SoortAfspraak_ID", "dbo.SoortAfspraak", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Afspraak", "SoortAfspraak_ID", "dbo.SoortAfspraak");
            DropForeignKey("dbo.Afspraak", "Masseur_ID", "dbo.Masseur");
            DropForeignKey("dbo.Afspraak", "Klant_ID", "dbo.Klant");
            DropIndex("dbo.Afspraak", new[] { "SoortAfspraak_ID" });
            DropIndex("dbo.Afspraak", new[] { "Masseur_ID" });
            DropIndex("dbo.Afspraak", new[] { "Klant_ID" });
            AlterColumn("dbo.SoortAfspraak", "Naam", c => c.String());
            AlterColumn("dbo.Masseur", "Naam", c => c.String());
            AlterColumn("dbo.Werksituatie", "Naam", c => c.String());
            AlterColumn("dbo.Klant", "Voornaam", c => c.String());
            AlterColumn("dbo.Klant", "Naam", c => c.String());
            AlterColumn("dbo.Afspraak", "SoortAfspraak_ID", c => c.Int());
            AlterColumn("dbo.Afspraak", "Masseur_ID", c => c.Int());
            AlterColumn("dbo.Afspraak", "Klant_ID", c => c.Int());
            CreateIndex("dbo.Afspraak", "SoortAfspraak_ID");
            CreateIndex("dbo.Afspraak", "Masseur_ID");
            CreateIndex("dbo.Afspraak", "Klant_ID");
            AddForeignKey("dbo.Afspraak", "SoortAfspraak_ID", "dbo.SoortAfspraak", "ID");
            AddForeignKey("dbo.Afspraak", "Masseur_ID", "dbo.Masseur", "ID");
            AddForeignKey("dbo.Afspraak", "Klant_ID", "dbo.Klant", "ID");
        }
    }
}
