namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelatiesFinal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KlantRelatie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Relatie_ID = c.Int(),
                        RelatieType_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Klant", t => t.Relatie_ID)
                .ForeignKey("dbo.Relatie", t => t.RelatieType_ID)
                .Index(t => t.Relatie_ID)
                .Index(t => t.RelatieType_ID);
            
            CreateTable(
                "dbo.Relatie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KlantRelatie", "RelatieType_ID", "dbo.Relatie");
            DropForeignKey("dbo.KlantRelatie", "Relatie_ID", "dbo.Klant");
            DropIndex("dbo.KlantRelatie", new[] { "RelatieType_ID" });
            DropIndex("dbo.KlantRelatie", new[] { "Relatie_ID" });
            DropTable("dbo.Relatie");
            DropTable("dbo.KlantRelatie");
        }
    }
}
