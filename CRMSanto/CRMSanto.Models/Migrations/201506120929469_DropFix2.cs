namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropFix2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.KlantRelatie", newName: "RelatieKlant");
            DropForeignKey("dbo.KlantRelatie", "KlantA_ID", "dbo.Klant");
            DropForeignKey("dbo.KlantRelatie", "KlantB_ID", "dbo.Klant");
            DropIndex("dbo.RelatieKlant", new[] { "KlantA_ID" });
            DropIndex("dbo.RelatieKlant", new[] { "KlantB_ID" });
            RenameColumn(table: "dbo.RelatieKlant", name: "Klant_ID", newName: "Relatie_ID");
            RenameIndex(table: "dbo.RelatieKlant", name: "IX_Klant_ID", newName: "IX_Relatie_ID");
            CreateTable(
                "dbo.Relatie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.RelatieKlant", "RelatieType_ID", c => c.Int());
            CreateIndex("dbo.RelatieKlant", "RelatieType_ID");
            AddForeignKey("dbo.RelatieKlant", "RelatieType_ID", "dbo.Relatie", "ID");
            DropColumn("dbo.RelatieKlant", "KlantA_ID");
            DropColumn("dbo.RelatieKlant", "KlantB_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RelatieKlant", "KlantB_ID", c => c.Int());
            AddColumn("dbo.RelatieKlant", "KlantA_ID", c => c.Int());
            DropForeignKey("dbo.RelatieKlant", "RelatieType_ID", "dbo.Relatie");
            DropIndex("dbo.RelatieKlant", new[] { "RelatieType_ID" });
            DropColumn("dbo.RelatieKlant", "RelatieType_ID");
            DropTable("dbo.Relatie");
            RenameIndex(table: "dbo.RelatieKlant", name: "IX_Relatie_ID", newName: "IX_Klant_ID");
            RenameColumn(table: "dbo.RelatieKlant", name: "Relatie_ID", newName: "Klant_ID");
            CreateIndex("dbo.RelatieKlant", "KlantB_ID");
            CreateIndex("dbo.RelatieKlant", "KlantA_ID");
            AddForeignKey("dbo.KlantRelatie", "KlantB_ID", "dbo.Klant", "ID");
            AddForeignKey("dbo.KlantRelatie", "KlantA_ID", "dbo.Klant", "ID");
            RenameTable(name: "dbo.RelatieKlant", newName: "KlantRelatie");
        }
    }
}
