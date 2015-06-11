namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class producten : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "MinimumStock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "MinimumStock");
        }
    }
}
