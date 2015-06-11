namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class minstock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "MinStock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "MinimumStock", c => c.Int(nullable: false));
        }
    }
}
