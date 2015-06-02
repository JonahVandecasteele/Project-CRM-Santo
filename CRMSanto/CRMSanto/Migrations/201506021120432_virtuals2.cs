namespace CRMSanto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class virtuals2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Karaktertrek", "Klant_ID", "dbo.Klant");
            DropIndex("dbo.Karaktertrek", new[] { "Klant_ID" });
            CreateTable(
                "dbo.KaraktertrekKlant",
                c => new
                    {
                        Karaktertrek_ID = c.Int(nullable: false),
                        Klant_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Karaktertrek_ID, t.Klant_ID })
                .ForeignKey("dbo.Karaktertrek", t => t.Karaktertrek_ID, cascadeDelete: true)
                .ForeignKey("dbo.Klant", t => t.Klant_ID, cascadeDelete: true)
                .Index(t => t.Karaktertrek_ID)
                .Index(t => t.Klant_ID);
            
            DropColumn("dbo.Karaktertrek", "Klant_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Karaktertrek", "Klant_ID", c => c.Int());
            DropForeignKey("dbo.KaraktertrekKlant", "Klant_ID", "dbo.Klant");
            DropForeignKey("dbo.KaraktertrekKlant", "Karaktertrek_ID", "dbo.Karaktertrek");
            DropIndex("dbo.KaraktertrekKlant", new[] { "Klant_ID" });
            DropIndex("dbo.KaraktertrekKlant", new[] { "Karaktertrek_ID" });
            DropTable("dbo.KaraktertrekKlant");
            CreateIndex("dbo.Karaktertrek", "Klant_ID");
            AddForeignKey("dbo.Karaktertrek", "Klant_ID", "dbo.Klant", "ID");
        }
    }
}
