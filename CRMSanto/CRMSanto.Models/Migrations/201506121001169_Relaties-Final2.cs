namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelatiesFinal2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KlantRelatie", "Relatie_ID", "dbo.Klant");
            AddColumn("dbo.KlantRelatie", "Klant_ID_ID", c => c.Int());
            AddColumn("dbo.KlantRelatie", "Klant_ID", c => c.Int());
            CreateIndex("dbo.KlantRelatie", "Klant_ID_ID");
            CreateIndex("dbo.KlantRelatie", "Klant_ID");
            AddForeignKey("dbo.KlantRelatie", "Klant_ID_ID", "dbo.Klant", "ID");
            AddForeignKey("dbo.KlantRelatie", "Klant_ID", "dbo.Klant", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KlantRelatie", "Klant_ID", "dbo.Klant");
            DropForeignKey("dbo.KlantRelatie", "Klant_ID_ID", "dbo.Klant");
            DropIndex("dbo.KlantRelatie", new[] { "Klant_ID" });
            DropIndex("dbo.KlantRelatie", new[] { "Klant_ID_ID" });
            DropColumn("dbo.KlantRelatie", "Klant_ID");
            DropColumn("dbo.KlantRelatie", "Klant_ID_ID");
            AddForeignKey("dbo.KlantRelatie", "Relatie_ID", "dbo.Klant", "ID");
        }
    }
}
