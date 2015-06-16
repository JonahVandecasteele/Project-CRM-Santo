namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KlantRelatieTweaks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Relatie", "Klant_ID", c => c.Int());
            CreateIndex("dbo.Relatie", "Klant_ID");
            AddForeignKey("dbo.Relatie", "Klant_ID", "dbo.Klant", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Relatie", "Klant_ID", "dbo.Klant");
            DropIndex("dbo.Relatie", new[] { "Klant_ID" });
            DropColumn("dbo.Relatie", "Klant_ID");
        }
    }
}
