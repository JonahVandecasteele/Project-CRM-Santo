namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KlantRelatieTweaks3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KlantRelatie", "Klant_ID", "dbo.Klant");
            DropForeignKey("dbo.KlantRelatie", "Klant_ID1", "dbo.Klant");
            DropIndex("dbo.KlantRelatie", new[] { "Klant_ID" });
            DropIndex("dbo.KlantRelatie", new[] { "Klant_ID1" });
            DropIndex("dbo.KlantRelatie", new[] { "Klant_ID2" });
       //     DropColumn("dbo.KlantRelatie", "Klant_ID2");
           // RenameColumn(table: "dbo.KlantRelatie", name: "Klant_ID2", newName: "Relatie_ID");
      //      DropColumn("dbo.KlantRelatie", "Klant_ID");
            DropColumn("dbo.KlantRelatie", "Klant_ID1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.KlantRelatie", "Klant_ID1", c => c.Int());
            AddColumn("dbo.KlantRelatie", "Klant_ID", c => c.Int());
       //     RenameColumn(table: "dbo.KlantRelatie", name: "Relatie_ID", newName: "Klant_ID2");
          //  AddColumn("dbo.KlantRelatie", "Klant_ID2", c => c.Int());
            CreateIndex("dbo.KlantRelatie", "Klant_ID2");
            CreateIndex("dbo.KlantRelatie", "Klant_ID1");
            CreateIndex("dbo.KlantRelatie", "Klant_ID");
            AddForeignKey("dbo.KlantRelatie", "Klant_ID1", "dbo.Klant", "ID");
            AddForeignKey("dbo.KlantRelatie", "Klant_ID", "dbo.Klant", "ID");
        }
    }
}
