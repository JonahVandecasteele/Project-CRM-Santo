namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validatie2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Werksituatie", "Naam", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Werksituatie", "Naam", c => c.String(nullable: false));
        }
    }
}
