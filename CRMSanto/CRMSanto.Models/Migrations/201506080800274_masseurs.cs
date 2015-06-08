namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class masseurs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Masseur",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Afspraak", "Masseur_ID", c => c.Int());
            CreateIndex("dbo.Afspraak", "Masseur_ID");
            AddForeignKey("dbo.Afspraak", "Masseur_ID", "dbo.Masseur", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Afspraak", "Masseur_ID", "dbo.Masseur");
            DropIndex("dbo.Afspraak", new[] { "Masseur_ID" });
            DropColumn("dbo.Afspraak", "Masseur_ID");
            DropTable("dbo.Masseur");
        }
    }
}
