namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KlantRelatieTweaks2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Relatie", "Klant_ID", "dbo.Klant");
            DropForeignKey("dbo.KlantRelatie", "Klant_ID1", "dbo.Klant");
            DropIndex("dbo.Relatie", new[] { "Klant_ID" });
            AddColumn("dbo.KlantRelatie", "Klant_ID2", c => c.Int());
            CreateIndex("dbo.KlantRelatie", "Klant_ID2");
            AddForeignKey("dbo.KlantRelatie", "Klant_ID1", "dbo.Klant", "ID");
            AddForeignKey("dbo.KlantRelatie", "Klant_ID2", "dbo.Klant", "ID");
            DropColumn("dbo.Relatie", "Klant_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Relatie", "Klant_ID", c => c.Int());
            DropForeignKey("dbo.KlantRelatie", "Klant_ID2", "dbo.Klant");
            DropForeignKey("dbo.KlantRelatie", "Klant_ID1", "dbo.Klant");
            DropIndex("dbo.KlantRelatie", new[] { "Klant_ID2" });
            DropColumn("dbo.KlantRelatie", "Klant_ID2");
            CreateIndex("dbo.Relatie", "Klant_ID");
            AddForeignKey("dbo.KlantRelatie", "Klant_ID1", "dbo.Klant", "ID");
            AddForeignKey("dbo.Relatie", "Klant_ID", "dbo.Klant", "ID");
        }
    }
}
