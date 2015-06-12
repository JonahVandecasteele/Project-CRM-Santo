namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class winkelmand : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Winkelmand",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Klant_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Klant", t => t.Klant_ID)
                .Index(t => t.Klant_ID);
            
            CreateTable(
                "dbo.WinkelmandProductregistratie",
                c => new
                    {
                        Winkelmand_ID = c.Int(nullable: false),
                        Productregistratie_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Winkelmand_ID, t.Productregistratie_ID })
                .ForeignKey("dbo.Winkelmand", t => t.Winkelmand_ID, cascadeDelete: true)
                .ForeignKey("dbo.Productregistratie", t => t.Productregistratie_ID, cascadeDelete: true)
                .Index(t => t.Winkelmand_ID)
                .Index(t => t.Productregistratie_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WinkelmandProductregistratie", "Productregistratie_ID", "dbo.Productregistratie");
            DropForeignKey("dbo.WinkelmandProductregistratie", "Winkelmand_ID", "dbo.Winkelmand");
            DropForeignKey("dbo.Winkelmand", "Klant_ID", "dbo.Klant");
            DropIndex("dbo.WinkelmandProductregistratie", new[] { "Productregistratie_ID" });
            DropIndex("dbo.WinkelmandProductregistratie", new[] { "Winkelmand_ID" });
            DropIndex("dbo.Winkelmand", new[] { "Klant_ID" });
            DropTable("dbo.WinkelmandProductregistratie");
            DropTable("dbo.Winkelmand");
        }
    }
}
