namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class probeerselarrangementen : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Arrangement",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                        Duur = c.Int(nullable: false),
                        Prijs = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Extra",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                        Prijs = c.Int(nullable: false),
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
            
            AddColumn("dbo.Afspraak", "AantalPersonen", c => c.Int(nullable: false));
            AddColumn("dbo.Afspraak", "Arrangement_ID", c => c.Int());
            CreateIndex("dbo.Afspraak", "Arrangement_ID");
            AddForeignKey("dbo.Afspraak", "Arrangement_ID", "dbo.Arrangement", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExtraAfspraak", "Afspraak_ID", "dbo.Afspraak");
            DropForeignKey("dbo.ExtraAfspraak", "Extra_ID", "dbo.Extra");
            DropForeignKey("dbo.Afspraak", "Arrangement_ID", "dbo.Arrangement");
            DropIndex("dbo.ExtraAfspraak", new[] { "Afspraak_ID" });
            DropIndex("dbo.ExtraAfspraak", new[] { "Extra_ID" });
            DropIndex("dbo.Afspraak", new[] { "Arrangement_ID" });
            DropColumn("dbo.Afspraak", "Arrangement_ID");
            DropColumn("dbo.Afspraak", "AantalPersonen");
            DropTable("dbo.ExtraAfspraak");
            DropTable("dbo.Extra");
            DropTable("dbo.Arrangement");
        }
    }
}
