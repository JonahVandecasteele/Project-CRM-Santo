namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class voedingspatroon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Klant", "Voedingspatroon_ID", c => c.Int());
            CreateIndex("dbo.Klant", "Voedingspatroon_ID");
            AddForeignKey("dbo.Klant", "Voedingspatroon_ID", "dbo.Voedingspatroon", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Klant", "Voedingspatroon_ID", "dbo.Voedingspatroon");
            DropIndex("dbo.Klant", new[] { "Voedingspatroon_ID" });
            DropColumn("dbo.Klant", "Voedingspatroon_ID");
        }
    }
}
