namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class winkelmand2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Productregistratie", "Klant_ID", "dbo.Klant");
            DropForeignKey("dbo.WinkelmandProductregistratie", "Winkelmand_ID", "dbo.Winkelmand");
            DropForeignKey("dbo.WinkelmandProductregistratie", "Productregistratie_ID", "dbo.Productregistratie");
            DropIndex("dbo.Productregistratie", new[] { "Klant_ID" });
            DropIndex("dbo.WinkelmandProductregistratie", new[] { "Winkelmand_ID" });
            DropIndex("dbo.WinkelmandProductregistratie", new[] { "Productregistratie_ID" });
            AddColumn("dbo.Productregistratie", "Winkelmand_ID", c => c.Int());
            AddColumn("dbo.Winkelmand", "DatumTijdstip", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Productregistratie", "Winkelmand_ID");
            AddForeignKey("dbo.Productregistratie", "Winkelmand_ID", "dbo.Winkelmand", "ID");
            DropColumn("dbo.Productregistratie", "DatumTijdstip");
            DropColumn("dbo.Productregistratie", "Klant_ID");
            DropTable("dbo.WinkelmandProductregistratie");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.WinkelmandProductregistratie",
                c => new
                    {
                        Winkelmand_ID = c.Int(nullable: false),
                        Productregistratie_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Winkelmand_ID, t.Productregistratie_ID });
            
            AddColumn("dbo.Productregistratie", "Klant_ID", c => c.Int());
            AddColumn("dbo.Productregistratie", "DatumTijdstip", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Productregistratie", "Winkelmand_ID", "dbo.Winkelmand");
            DropIndex("dbo.Productregistratie", new[] { "Winkelmand_ID" });
            DropColumn("dbo.Winkelmand", "DatumTijdstip");
            DropColumn("dbo.Productregistratie", "Winkelmand_ID");
            CreateIndex("dbo.WinkelmandProductregistratie", "Productregistratie_ID");
            CreateIndex("dbo.WinkelmandProductregistratie", "Winkelmand_ID");
            CreateIndex("dbo.Productregistratie", "Klant_ID");
            AddForeignKey("dbo.WinkelmandProductregistratie", "Productregistratie_ID", "dbo.Productregistratie", "ID", cascadeDelete: true);
            AddForeignKey("dbo.WinkelmandProductregistratie", "Winkelmand_ID", "dbo.Winkelmand", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Productregistratie", "Klant_ID", "dbo.Klant", "ID");
        }
    }
}
