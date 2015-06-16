namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KlantRelatieTweaks6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KlantRelatie", "Relatie_ID_ID", c => c.Int());
            CreateIndex("dbo.KlantRelatie", "Relatie_ID_ID");
            AddForeignKey("dbo.KlantRelatie", "Relatie_ID_ID", "dbo.Klant", "ID");
            DropForeignKey("dbo.KlantRelatie", "Klant_ID1", "dbo.Klant");
            DropIndex("dbo.KlantRelatie", new[] { "Klant_ID1" });
            DropColumn("dbo.KlantRelatie", "Klant_ID1");  
        }
        
        public override void Down()
        {
            AddColumn("dbo.KlantRelatie", "Klant_ID1", c => c.Int());
            CreateIndex("dbo.KlantRelatie", new[] { "Klant_ID1" });
            AddForeignKey("dbo.KlantRelatie", "Klant_ID1", "dbo.Klant", "ID");
            DropForeignKey("dbo.KlantRelatie", "Relatie_ID_ID", "dbo.Klant");
            DropIndex("dbo.KlantRelatie", new[] { "Relatie_ID_ID" });
            DropColumn("dbo.KlantRelatie", "Relatie_ID_ID");
        }
    }
}
