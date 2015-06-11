namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kustze : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Voedingspatroon",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.MedischeFiche", "Voedingspatroon_ID", c => c.Int());
            CreateIndex("dbo.MedischeFiche", "Voedingspatroon_ID");
            AddForeignKey("dbo.MedischeFiche", "Voedingspatroon_ID", "dbo.Voedingspatroon", "ID");
            DropColumn("dbo.MedischeFiche", "Voedingspatroon");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MedischeFiche", "Voedingspatroon", c => c.String());
            DropForeignKey("dbo.MedischeFiche", "Voedingspatroon_ID", "dbo.Voedingspatroon");
            DropIndex("dbo.MedischeFiche", new[] { "Voedingspatroon_ID" });
            DropColumn("dbo.MedischeFiche", "Voedingspatroon_ID");
            DropTable("dbo.Voedingspatroon");
        }
    }
}
