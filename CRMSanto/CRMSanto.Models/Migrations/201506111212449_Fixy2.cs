namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixy2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Product", "MinimumStock");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "MinimumStock", c => c.Int(nullable: false));
        }
    }
}
