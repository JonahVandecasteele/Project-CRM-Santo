namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelatiesFinal4 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.KlantRelatie", name: "Klant_ID", newName: "Relatie_ID");
            RenameIndex(table: "dbo.KlantRelatie", name: "IX_Klant_ID", newName: "IX_Relatie_ID");
            CreateTable(
                "dbo.Relatie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.KlantRelatie", "RelatieType_ID", c => c.Int());
            CreateIndex("dbo.KlantRelatie", "RelatieType_ID");
            AddForeignKey("dbo.KlantRelatie", "RelatieType_ID", "dbo.Relatie", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KlantRelatie", "RelatieType_ID", "dbo.Relatie");
            DropIndex("dbo.KlantRelatie", new[] { "RelatieType_ID" });
            DropColumn("dbo.KlantRelatie", "RelatieType_ID");
            DropTable("dbo.Relatie");
            RenameIndex(table: "dbo.KlantRelatie", name: "IX_Relatie_ID", newName: "IX_Klant_ID");
            RenameColumn(table: "dbo.KlantRelatie", name: "Relatie_ID", newName: "Klant_ID");
        }
    }
}
