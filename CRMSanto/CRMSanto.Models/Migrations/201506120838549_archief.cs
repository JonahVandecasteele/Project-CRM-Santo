namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class archief : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KlantRelatie", "KlantA_ID", "dbo.Klant");
            DropForeignKey("dbo.KlantRelatie", "KlantB_ID", "dbo.Klant");
            DropForeignKey("dbo.KlantRelatie", "Klant_ID", "dbo.Klant");
            DropIndex("dbo.KlantRelatie", new[] { "KlantA_ID" });
            DropIndex("dbo.KlantRelatie", new[] { "KlantB_ID" });
            DropIndex("dbo.KlantRelatie", new[] { "Klant_ID" });
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Adres", t => t.Adres_ID)
                .ForeignKey("dbo.Afspraak", t => t.Afspraak_ID)
                .ForeignKey("dbo.Arrangement", t => t.Arrangement_ID)
                .ForeignKey("dbo.Klant", t => t.Klant_ID)
                .ForeignKey("dbo.Masseur", t => t.Masseur_ID)
                .ForeignKey("dbo.SoortAfspraak", t => t.SoortAfspraak_ID)
                .Index(t => t.Adres_ID)
                .Index(t => t.Afspraak_ID)
                .Index(t => t.Arrangement_ID)
                .Index(t => t.Klant_ID)
                .Index(t => t.Masseur_ID)
                .Index(t => t.SoortAfspraak_ID);
            
            AddColumn("dbo.Extra", "Archief_ID", c => c.Int());
            CreateIndex("dbo.Extra", "Archief_ID");
            AddForeignKey("dbo.Extra", "Archief_ID", "dbo.Archief", "ID");
            DropTable("dbo.KlantRelatie");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.KlantRelatie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KlantA_ID = c.Int(),
                        KlantB_ID = c.Int(),
                        Klant_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
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
            DropColumn("dbo.Extra", "Archief_ID");
            DropTable("dbo.Archief");
            CreateIndex("dbo.KlantRelatie", "Klant_ID");
            CreateIndex("dbo.KlantRelatie", "KlantB_ID");
            CreateIndex("dbo.KlantRelatie", "KlantA_ID");
            AddForeignKey("dbo.KlantRelatie", "Klant_ID", "dbo.Klant", "ID");
            AddForeignKey("dbo.KlantRelatie", "KlantB_ID", "dbo.Klant", "ID");
            AddForeignKey("dbo.KlantRelatie", "KlantA_ID", "dbo.Klant", "ID");
        }
    }
}
