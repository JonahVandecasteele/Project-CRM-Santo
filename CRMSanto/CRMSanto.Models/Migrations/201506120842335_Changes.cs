namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Archief", "Adres_ID", "dbo.Adres");
            DropForeignKey("dbo.Archief", "Afspraak_ID", "dbo.Afspraak");
            DropForeignKey("dbo.Archief", "Arrangement_ID", "dbo.Arrangement");
            DropForeignKey("dbo.Extra", "Archief_ID", "dbo.Archief");
            DropForeignKey("dbo.Archief", "Klant_ID", "dbo.Klant");
            DropForeignKey("dbo.Archief", "Masseur_ID", "dbo.Masseur");
            DropForeignKey("dbo.Archief", "SoortAfspraak_ID", "dbo.SoortAfspraak");
            DropIndex("dbo.Extra", new[] { "Archief_ID" });
            DropIndex("dbo.Archief", new[] { "Adres_ID" });
            DropIndex("dbo.Archief", new[] { "Afspraak_ID" });
            DropIndex("dbo.Archief", new[] { "Arrangement_ID" });
            DropIndex("dbo.Archief", new[] { "Klant_ID" });
            DropIndex("dbo.Archief", new[] { "Masseur_ID" });
            DropIndex("dbo.Archief", new[] { "SoortAfspraak_ID" });
            CreateTable(
                "dbo.KlantRelatie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KlantA_ID = c.Int(),
                        KlantB_ID = c.Int(),
                        Klant_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Klant", t => t.KlantA_ID)
                .ForeignKey("dbo.Klant", t => t.KlantB_ID)
                .ForeignKey("dbo.Klant", t => t.Klant_ID)
                .Index(t => t.KlantA_ID)
                .Index(t => t.KlantB_ID)
                .Index(t => t.Klant_ID);
            
            DropColumn("dbo.Extra", "Archief_ID");
            DropTable("dbo.Archief");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Archief",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DatumTijdstip = c.DateTime(nullable: false),
                        Verplaatsing = c.Boolean(nullable: false),
                        Notitie = c.String(),
                        Duur = c.Int(nullable: false),
                        SoloDuo = c.Boolean(nullable: false),
                        Geannuleerd = c.Boolean(nullable: false),
                        AantalPersonen = c.Int(nullable: false),
                        Adres_ID = c.Int(),
                        Afspraak_ID = c.Int(),
                        Arrangement_ID = c.Int(),
                        Klant_ID = c.Int(),
                        Masseur_ID = c.Int(),
                        SoortAfspraak_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Extra", "Archief_ID", c => c.Int());
            DropForeignKey("dbo.KlantRelatie", "Klant_ID", "dbo.Klant");
            DropForeignKey("dbo.KlantRelatie", "KlantB_ID", "dbo.Klant");
            DropForeignKey("dbo.KlantRelatie", "KlantA_ID", "dbo.Klant");
            DropIndex("dbo.KlantRelatie", new[] { "Klant_ID" });
            DropIndex("dbo.KlantRelatie", new[] { "KlantB_ID" });
            DropIndex("dbo.KlantRelatie", new[] { "KlantA_ID" });
            DropTable("dbo.KlantRelatie");
            CreateIndex("dbo.Archief", "SoortAfspraak_ID");
            CreateIndex("dbo.Archief", "Masseur_ID");
            CreateIndex("dbo.Archief", "Klant_ID");
            CreateIndex("dbo.Archief", "Arrangement_ID");
            CreateIndex("dbo.Archief", "Afspraak_ID");
            CreateIndex("dbo.Archief", "Adres_ID");
            CreateIndex("dbo.Extra", "Archief_ID");
            AddForeignKey("dbo.Archief", "SoortAfspraak_ID", "dbo.SoortAfspraak", "ID");
            AddForeignKey("dbo.Archief", "Masseur_ID", "dbo.Masseur", "ID");
            AddForeignKey("dbo.Archief", "Klant_ID", "dbo.Klant", "ID");
            AddForeignKey("dbo.Extra", "Archief_ID", "dbo.Archief", "ID");
            AddForeignKey("dbo.Archief", "Arrangement_ID", "dbo.Arrangement", "ID");
            AddForeignKey("dbo.Archief", "Afspraak_ID", "dbo.Afspraak", "ID");
            AddForeignKey("dbo.Archief", "Adres_ID", "dbo.Adres", "ID");
        }
    }
}
