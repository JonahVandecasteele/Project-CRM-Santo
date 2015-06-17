namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KlantRelatieTweaks5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KlantRelatie", "Klant_ID2", "dbo.Klant");
            DropIndex("dbo.KlantRelatie", new[] { "Klant_ID2" });
            
            DropColumn("dbo.KlantRelatie", "Klant_ID2");          
        }
        
        public override void Down()
        {
            AddColumn("dbo.KlantRelatie", "Klant_ID2", c => c.Int());
            CreateIndex("dbo.KlantRelatie", new[] { "Klant_ID2" });
            AddForeignKey("dbo.KlantRelatie", "Klant_ID2", "dbo.Klant", "ID");
        }
    }
}
