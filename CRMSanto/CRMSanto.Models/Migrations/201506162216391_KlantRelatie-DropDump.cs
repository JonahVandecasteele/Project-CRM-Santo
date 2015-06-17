namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KlantRelatieDropDump : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KlantRelatie", "Klant_ID", "dbo.Klant");
            DropIndex("dbo.KlantRelatie", new[] { "Klant_ID" });
            DropIndex("dbo.KlantRelatie", new[] { "Klant_ID1" });
            DropIndex("dbo.KlantRelatie", new[] { "Relatie_ID" });
            DropColumn("dbo.KlantRelatie", "Relatie_ID");
         //   RenameColumn(table: "dbo.KlantRelatie", name: "Klant_ID1", newName: "Relatie_ID");
            DropColumn("dbo.KlantRelatie", "Klant_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.KlantRelatie", "Klant_ID", c => c.Int());
         //   RenameColumn(table: "dbo.KlantRelatie", name: "Relatie_ID", newName: "Klant_ID1");
            AddColumn("dbo.KlantRelatie", "Relatie_ID", c => c.Int());
            CreateIndex("dbo.KlantRelatie", "Klant_ID1");
            CreateIndex("dbo.KlantRelatie", "Relatie_ID");
            CreateIndex("dbo.KlantRelatie", "Klant_ID");
            AddForeignKey("dbo.KlantRelatie", "Klant_ID", "dbo.Klant", "ID");
        }
    }
}
