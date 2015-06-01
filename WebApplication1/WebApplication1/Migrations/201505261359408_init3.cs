namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Afspraaks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DatumTijdstip = c.DateTime(nullable: false),
                        Verplaatsing = c.Boolean(nullable: false),
                        Notitie = c.String(),
                        Duur = c.Int(nullable: false),
                        SoloDuo = c.Boolean(nullable: false),
                        Geannuleerd = c.Boolean(nullable: false),
                        Adres_ID = c.Int(),
                        Klant_ID = c.Int(),
                        SoortAfspraak_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Adres", t => t.Adres_ID)
                .ForeignKey("dbo.Klants", t => t.Klant_ID)
                .ForeignKey("dbo.SoortAfspraaks", t => t.SoortAfspraak_ID)
                .Index(t => t.Adres_ID)
                .Index(t => t.Klant_ID)
                .Index(t => t.SoortAfspraak_ID);
            
            CreateTable(
                "dbo.SoortAfspraaks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                        Omschrijving = c.String(),
                        Duur = c.Int(nullable: false),
                        Prijs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Verplaatsingmogelijk = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                        Prijs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Inhoud = c.Int(nullable: false),
                        Foto = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Productregistraties",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DatumTijdstip = c.DateTime(nullable: false),
                        Aantal = c.Int(nullable: false),
                        Klant_ID = c.Int(),
                        Product_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Klants", t => t.Klant_ID)
                .ForeignKey("dbo.Products", t => t.Product_ID)
                .Index(t => t.Klant_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.Sessies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AantalSessies = c.Int(nullable: false),
                        Klant_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Klants", t => t.Klant_ID)
                .Index(t => t.Klant_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sessies", "Klant_ID", "dbo.Klants");
            DropForeignKey("dbo.Productregistraties", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.Productregistraties", "Klant_ID", "dbo.Klants");
            DropForeignKey("dbo.Afspraaks", "SoortAfspraak_ID", "dbo.SoortAfspraaks");
            DropForeignKey("dbo.Afspraaks", "Klant_ID", "dbo.Klants");
            DropForeignKey("dbo.Afspraaks", "Adres_ID", "dbo.Adres");
            DropIndex("dbo.Sessies", new[] { "Klant_ID" });
            DropIndex("dbo.Productregistraties", new[] { "Product_ID" });
            DropIndex("dbo.Productregistraties", new[] { "Klant_ID" });
            DropIndex("dbo.Afspraaks", new[] { "SoortAfspraak_ID" });
            DropIndex("dbo.Afspraaks", new[] { "Klant_ID" });
            DropIndex("dbo.Afspraaks", new[] { "Adres_ID" });
            DropTable("dbo.Sessies");
            DropTable("dbo.Productregistraties");
            DropTable("dbo.Products");
            DropTable("dbo.SoortAfspraaks");
            DropTable("dbo.Afspraaks");
        }
    }
}
