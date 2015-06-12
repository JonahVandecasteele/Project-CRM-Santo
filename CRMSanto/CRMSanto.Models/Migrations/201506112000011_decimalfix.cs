namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class decimalfix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Arrangement", "Prijs", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Extra", "Prijs", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Extra", "Prijs", c => c.Int(nullable: false));
            AlterColumn("dbo.Arrangement", "Prijs", c => c.Int(nullable: false));
        }
    }
}
