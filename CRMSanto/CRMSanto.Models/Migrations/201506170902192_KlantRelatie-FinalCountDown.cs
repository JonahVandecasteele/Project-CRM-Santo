namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KlantRelatieFinalCountDown : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KlantRelatie", "Klant_ID1", "dbo.Klant");
            DropIndex("dbo.KlantRelatie", new[] { "Klant_ID" });
            DropIndex("dbo.KlantRelatie", new[] { "Klant_ID1" });
            DropColumn("dbo.KlantRelatie", "Relatie_ID");
            RenameColumn(table: "dbo.KlantRelatie", name: "Klant_ID", newName: "Relatie_ID");
            DropColumn("dbo.KlantRelatie", "Klant_ID1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.KlantRelatie", "Klant_ID1", c => c.Int());
            RenameColumn(table: "dbo.KlantRelatie", name: "Relatie_ID", newName: "Klant_ID");
            AddColumn("dbo.KlantRelatie", "Relatie_ID", c => c.Int());
            CreateIndex("dbo.KlantRelatie", "Klant_ID1");
            CreateIndex("dbo.KlantRelatie", "Klant_ID");
            AddForeignKey("dbo.KlantRelatie", "Klant_ID1", "dbo.Klant", "ID");
        }
    }
}
