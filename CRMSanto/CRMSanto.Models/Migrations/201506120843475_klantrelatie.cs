namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class klantrelatie : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KlantRelatie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KlantA_ID = c.Int(),
                        KlantB_ID = c.Int(),
                        Klant_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Klant", t => t.KlantA_ID)
                .ForeignKey("dbo.Klant", t => t.KlantB_ID)
                .ForeignKey("dbo.Klant", t => t.Klant_ID)
                .Index(t => t.KlantA_ID)
                .Index(t => t.KlantB_ID)
                .Index(t => t.Klant_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KlantRelatie", "Klant_ID", "dbo.Klant");
            DropForeignKey("dbo.KlantRelatie", "KlantB_ID", "dbo.Klant");
            DropForeignKey("dbo.KlantRelatie", "KlantA_ID", "dbo.Klant");
            DropIndex("dbo.KlantRelatie", new[] { "Klant_ID" });
            DropIndex("dbo.KlantRelatie", new[] { "KlantB_ID" });
            DropIndex("dbo.KlantRelatie", new[] { "KlantA_ID" });
            DropTable("dbo.KlantRelatie");
        }
    }
}
