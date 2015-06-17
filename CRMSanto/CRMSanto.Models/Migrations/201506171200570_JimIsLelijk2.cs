namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JimIsLelijk2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductWinkelmand",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Aantal = c.Int(nullable: false),
                        VerkochtProduct_ID = c.Int(),
                        Rekening_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Product", t => t.VerkochtProduct_ID)
                .ForeignKey("dbo.Rekening", t => t.Rekening_ID)
                .Index(t => t.VerkochtProduct_ID)
                .Index(t => t.Rekening_ID);
            
            CreateTable(
                "dbo.Rekening",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DatumTijdstip = c.DateTime(nullable: false),
                        KlantID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductWinkelmand", "Rekening_ID", "dbo.Rekening");
            DropForeignKey("dbo.ProductWinkelmand", "VerkochtProduct_ID", "dbo.Product");
            DropIndex("dbo.ProductWinkelmand", new[] { "Rekening_ID" });
            DropIndex("dbo.ProductWinkelmand", new[] { "VerkochtProduct_ID" });
            DropTable("dbo.Rekening");
            DropTable("dbo.ProductWinkelmand");
        }
    }
}
