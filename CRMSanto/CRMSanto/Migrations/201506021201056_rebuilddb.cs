namespace CRMSanto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rebuilddb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MedischeFiche",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Voedingspatroon = c.String(),
                        Spijsvertering = c.String(),
                        HuidigeKlachten = c.String(),
                        Mutualiteit_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Mutualiteit", t => t.Mutualiteit_ID)
                .Index(t => t.Mutualiteit_ID);
            
            CreateTable(
                "dbo.PersoonlijkeFiche",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Hobby = c.String(),
                        Gezinssituatie = c.String(),
                        Werksituatie_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Werksituatie", t => t.Werksituatie_ID)
                .Index(t => t.Werksituatie_ID);
            
            AddColumn("dbo.Klant", "MedischeFiche_ID", c => c.Int());
            AddColumn("dbo.Klant", "PersoonlijkeFiche_ID", c => c.Int());
            CreateIndex("dbo.Klant", "MedischeFiche_ID");
            CreateIndex("dbo.Klant", "PersoonlijkeFiche_ID");
            AddForeignKey("dbo.Klant", "MedischeFiche_ID", "dbo.MedischeFiche", "ID");
            AddForeignKey("dbo.Klant", "PersoonlijkeFiche_ID", "dbo.PersoonlijkeFiche", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Klant", "PersoonlijkeFiche_ID", "dbo.PersoonlijkeFiche");
            DropForeignKey("dbo.PersoonlijkeFiche", "Werksituatie_ID", "dbo.Werksituatie");
            DropForeignKey("dbo.Klant", "MedischeFiche_ID", "dbo.MedischeFiche");
            DropForeignKey("dbo.MedischeFiche", "Mutualiteit_ID", "dbo.Mutualiteit");
            DropIndex("dbo.PersoonlijkeFiche", new[] { "Werksituatie_ID" });
            DropIndex("dbo.MedischeFiche", new[] { "Mutualiteit_ID" });
            DropIndex("dbo.Klant", new[] { "PersoonlijkeFiche_ID" });
            DropIndex("dbo.Klant", new[] { "MedischeFiche_ID" });
            DropColumn("dbo.Klant", "PersoonlijkeFiche_ID");
            DropColumn("dbo.Klant", "MedischeFiche_ID");
            DropTable("dbo.PersoonlijkeFiche");
            DropTable("dbo.MedischeFiche");
        }
    }
}
