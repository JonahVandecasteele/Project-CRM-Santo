namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allegow : DbMigration
    {
        public override void Up()
        {            
            CreateTable(
                "dbo.Extra",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                        Prijs = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Archief",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DatumTijdstip = c.DateTime(nullable: false),
                        KlantID = c.Int(nullable: false),
                        Verplaatsing = c.Boolean(nullable: false),
                        Notitie = c.String(),
                        SoortAfspraakID = c.Int(nullable: false),
                        Duur = c.Int(nullable: false),
                        SoloDuo = c.Boolean(nullable: false),
                        AdresID = c.Int(nullable: false),
                        Geannuleerd = c.Boolean(nullable: false),
                        MasseurID = c.Int(nullable: false),
                        ArrangementID = c.Int(nullable: false),
                        AantalPersonen = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            
            CreateTable(
                "dbo.ExtraAfspraak",
                c => new
                    {
                        Extra_ID = c.Int(nullable: false),
                        Afspraak_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Extra_ID, t.Afspraak_ID })
                .ForeignKey("dbo.Extra", t => t.Extra_ID, cascadeDelete: true)
                .ForeignKey("dbo.Afspraak", t => t.Afspraak_ID, cascadeDelete: true)
                .Index(t => t.Extra_ID)
                .Index(t => t.Afspraak_ID);
        }
        
        public override void Down()
        {           
        }
    }
}
