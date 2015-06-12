namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Extra", "Archief_ID", c => c.Int());
            AddColumn("dbo.Archief", "Adres_ID", c => c.Int());
            AddColumn("dbo.Archief", "Afspraak_ID", c => c.Int());
            AddColumn("dbo.Archief", "Arrangement_ID", c => c.Int());
            AddColumn("dbo.Archief", "Klant_ID", c => c.Int());
            AddColumn("dbo.Archief", "Masseur_ID", c => c.Int());
            AddColumn("dbo.Archief", "SoortAfspraak_ID", c => c.Int());
            CreateIndex("dbo.Extra", "Archief_ID");
            CreateIndex("dbo.Archief", "Adres_ID");
            CreateIndex("dbo.Archief", "Afspraak_ID");
            CreateIndex("dbo.Archief", "Arrangement_ID");
            CreateIndex("dbo.Archief", "Klant_ID");
            CreateIndex("dbo.Archief", "Masseur_ID");
            CreateIndex("dbo.Archief", "SoortAfspraak_ID");
            AddForeignKey("dbo.Archief", "Adres_ID", "dbo.Adres", "ID");
            AddForeignKey("dbo.Archief", "Afspraak_ID", "dbo.Afspraak", "ID");
            AddForeignKey("dbo.Archief", "Arrangement_ID", "dbo.Arrangement", "ID");
            AddForeignKey("dbo.Extra", "Archief_ID", "dbo.Archief", "ID");
            AddForeignKey("dbo.Archief", "Klant_ID", "dbo.Klant", "ID");
            AddForeignKey("dbo.Archief", "Masseur_ID", "dbo.Masseur", "ID");
            AddForeignKey("dbo.Archief", "SoortAfspraak_ID", "dbo.SoortAfspraak", "ID");
            DropColumn("dbo.Archief", "KlantID");
            DropColumn("dbo.Archief", "SoortAfspraakID");
            DropColumn("dbo.Archief", "AdresID");
            DropColumn("dbo.Archief", "MasseurID");
            DropColumn("dbo.Archief", "ArrangementID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Archief", "ArrangementID", c => c.Int(nullable: false));
            AddColumn("dbo.Archief", "MasseurID", c => c.Int(nullable: false));
            AddColumn("dbo.Archief", "AdresID", c => c.Int(nullable: false));
            AddColumn("dbo.Archief", "SoortAfspraakID", c => c.Int(nullable: false));
            AddColumn("dbo.Archief", "KlantID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Archief", "SoortAfspraak_ID", "dbo.SoortAfspraak");
            DropForeignKey("dbo.Archief", "Masseur_ID", "dbo.Masseur");
            DropForeignKey("dbo.Archief", "Klant_ID", "dbo.Klant");
            DropForeignKey("dbo.Extra", "Archief_ID", "dbo.Archief");
            DropForeignKey("dbo.Archief", "Arrangement_ID", "dbo.Arrangement");
            DropForeignKey("dbo.Archief", "Afspraak_ID", "dbo.Afspraak");
            DropForeignKey("dbo.Archief", "Adres_ID", "dbo.Adres");
            DropIndex("dbo.Archief", new[] { "SoortAfspraak_ID" });
            DropIndex("dbo.Archief", new[] { "Masseur_ID" });
            DropIndex("dbo.Archief", new[] { "Klant_ID" });
            DropIndex("dbo.Archief", new[] { "Arrangement_ID" });
            DropIndex("dbo.Archief", new[] { "Afspraak_ID" });
            DropIndex("dbo.Archief", new[] { "Adres_ID" });
            DropIndex("dbo.Extra", new[] { "Archief_ID" });
            DropColumn("dbo.Archief", "SoortAfspraak_ID");
            DropColumn("dbo.Archief", "Masseur_ID");
            DropColumn("dbo.Archief", "Klant_ID");
            DropColumn("dbo.Archief", "Arrangement_ID");
            DropColumn("dbo.Archief", "Afspraak_ID");
            DropColumn("dbo.Archief", "Adres_ID");
            DropColumn("dbo.Extra", "Archief_ID");
        }
    }
}
