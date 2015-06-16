namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KlantRelatieTweaks4 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.KlantRelatie", name: "Relatie_ID", newName: "Klant_ID1");
            RenameIndex(table: "dbo.KlantRelatie", name: "IX_Relatie_ID", newName: "IX_Klant_ID1");
            AddColumn("dbo.KlantRelatie", "Klant_ID", c => c.Int());
            CreateIndex("dbo.KlantRelatie", "Klant_ID");
            AddForeignKey("dbo.KlantRelatie", "Klant_ID", "dbo.Klant", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KlantRelatie", "Klant_ID", "dbo.Klant");
            DropIndex("dbo.KlantRelatie", new[] { "Klant_ID" });
            DropColumn("dbo.KlantRelatie", "Klant_ID");
            RenameIndex(table: "dbo.KlantRelatie", name: "IX_Klant_ID1", newName: "IX_Relatie_ID");
            RenameColumn(table: "dbo.KlantRelatie", name: "Klant_ID1", newName: "Relatie_ID");
        }
    }
}
