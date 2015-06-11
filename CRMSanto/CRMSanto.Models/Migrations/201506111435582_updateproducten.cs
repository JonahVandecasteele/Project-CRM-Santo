namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateproducten : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MedischeFiche", "Voedingspatroon_ID", "dbo.Voedingspatroon");
            DropIndex("dbo.MedischeFiche", new[] { "Voedingspatroon_ID" });
            AddColumn("dbo.MedischeFiche", "Voedingspatroon", c => c.String());
            DropColumn("dbo.MedischeFiche", "Voedingspatroon_ID");
            DropTable("dbo.Voedingspatroon");
        }
        
        public override void Down()
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
            DropColumn("dbo.MedischeFiche", "Voedingspatroon");
            CreateIndex("dbo.MedischeFiche", "Voedingspatroon_ID");
            AddForeignKey("dbo.MedischeFiche", "Voedingspatroon_ID", "dbo.Voedingspatroon", "ID");
        }
    }
}
