namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelatieTest2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KlantRelatie", "Relatie_ID", "dbo.Klant");
            AddColumn("dbo.KlantRelatie", "Klant_ID1", c => c.Int());
            CreateIndex("dbo.KlantRelatie", "Klant_ID1");
            AddForeignKey("dbo.KlantRelatie", "Klant_ID1", "dbo.Klant", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KlantRelatie", "Klant_ID1", "dbo.Klant");
            DropIndex("dbo.KlantRelatie", new[] { "Klant_ID1" });
            DropColumn("dbo.KlantRelatie", "Klant_ID1");
            AddForeignKey("dbo.KlantRelatie", "Relatie_ID", "dbo.Klant", "ID");
        }
    }
}
