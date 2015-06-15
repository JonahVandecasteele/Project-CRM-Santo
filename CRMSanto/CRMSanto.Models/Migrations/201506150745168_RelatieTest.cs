namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelatieTest : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.KlantRelatie", "Klant_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.KlantRelatie", "Klant_ID", c => c.Int(nullable: false));
        }
    }
}
