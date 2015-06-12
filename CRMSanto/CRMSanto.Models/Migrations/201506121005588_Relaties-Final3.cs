namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelatiesFinal3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KlantRelatie", "Klant_ID_ID", "dbo.Klant");
            DropForeignKey("dbo.KlantRelatie", "Relatie_ID", "dbo.Klant");
            DropForeignKey("dbo.KlantRelatie", "RelatieType_ID", "dbo.Relatie");
            DropIndex("dbo.KlantRelatie", new[] { "Klant_ID_ID" });
            DropIndex("dbo.KlantRelatie", new[] { "Relatie_ID" });
            DropIndex("dbo.KlantRelatie", new[] { "RelatieType_ID" });
            DropColumn("dbo.KlantRelatie", "Klant_ID_ID");
            DropColumn("dbo.KlantRelatie", "Relatie_ID");
            DropColumn("dbo.KlantRelatie", "RelatieType_ID");
            DropTable("dbo.Relatie");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Relatie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.KlantRelatie", "RelatieType_ID", c => c.Int());
            AddColumn("dbo.KlantRelatie", "Relatie_ID", c => c.Int());
            AddColumn("dbo.KlantRelatie", "Klant_ID_ID", c => c.Int());
            CreateIndex("dbo.KlantRelatie", "RelatieType_ID");
            CreateIndex("dbo.KlantRelatie", "Relatie_ID");
            CreateIndex("dbo.KlantRelatie", "Klant_ID_ID");
            AddForeignKey("dbo.KlantRelatie", "RelatieType_ID", "dbo.Relatie", "ID");
            AddForeignKey("dbo.KlantRelatie", "Relatie_ID", "dbo.Klant", "ID");
            AddForeignKey("dbo.KlantRelatie", "Klant_ID_ID", "dbo.Klant", "ID");
        }
    }
}
