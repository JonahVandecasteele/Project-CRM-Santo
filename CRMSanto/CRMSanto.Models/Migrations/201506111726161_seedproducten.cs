namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedproducten : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "ProductNr", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "ProductNr");
        }
    }
}
