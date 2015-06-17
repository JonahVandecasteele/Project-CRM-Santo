namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JimIsLelijk4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rekening", "Klant_ID", c => c.Int());
            CreateIndex("dbo.Rekening", "Klant_ID");
            AddForeignKey("dbo.Rekening", "Klant_ID", "dbo.Klant", "ID");
            DropColumn("dbo.Rekening", "KlantID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rekening", "KlantID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Rekening", "Klant_ID", "dbo.Klant");
            DropIndex("dbo.Rekening", new[] { "Klant_ID" });
            DropColumn("dbo.Rekening", "Klant_ID");
        }
    }
}
