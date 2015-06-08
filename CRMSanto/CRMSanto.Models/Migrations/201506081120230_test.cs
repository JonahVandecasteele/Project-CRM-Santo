namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Afspraak", "KlantID", "dbo.Klant");
            DropIndex("dbo.Afspraak", new[] { "KlantID" });
            RenameColumn(table: "dbo.Afspraak", name: "KlantID", newName: "Klant_ID");
            AlterColumn("dbo.Afspraak", "Klant_ID", c => c.Int());
            CreateIndex("dbo.Afspraak", "Klant_ID");
            AddForeignKey("dbo.Afspraak", "Klant_ID", "dbo.Klant", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Afspraak", "Klant_ID", "dbo.Klant");
            DropIndex("dbo.Afspraak", new[] { "Klant_ID" });
            AlterColumn("dbo.Afspraak", "Klant_ID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Afspraak", name: "Klant_ID", newName: "KlantID");
            CreateIndex("dbo.Afspraak", "KlantID");
            AddForeignKey("dbo.Afspraak", "KlantID", "dbo.Klant", "ID", cascadeDelete: true);
        }
    }
}
