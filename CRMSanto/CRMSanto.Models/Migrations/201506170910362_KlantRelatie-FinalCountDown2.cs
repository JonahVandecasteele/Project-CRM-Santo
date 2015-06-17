namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KlantRelatieFinalCountDown2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KlantRelatie", "Klant_ID", c => c.Int());
            CreateIndex("dbo.KlantRelatie", "Klant_ID");
            AddForeignKey("dbo.KlantRelatie", "Klant_ID", "dbo.Klant", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KlantRelatie", "Klant_ID", "dbo.Klant");
            DropIndex("dbo.KlantRelatie", new[] { "Klant_ID" });
            DropColumn("dbo.KlantRelatie", "Klant_ID");

        }
    }
}
