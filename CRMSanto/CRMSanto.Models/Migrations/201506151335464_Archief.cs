namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Archief : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Afspraak", "Discriminator", c => c.String(nullable: false, maxLength: 128));
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
            
            DropColumn("dbo.Afspraak", "Discriminator");
        }
    }
}
