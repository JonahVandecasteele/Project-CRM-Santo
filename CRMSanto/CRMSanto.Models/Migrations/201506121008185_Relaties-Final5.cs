namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelatiesFinal5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KlantRelatie", "Klant_ID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KlantRelatie", "Klant_ID");
        }

    }
}
