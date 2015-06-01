namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Klants", "SportHobby");
            DropColumn("dbo.Klants", "Allergie");
            DropColumn("dbo.Klants", "Blessures");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Klants", "Blessures", c => c.String());
            AddColumn("dbo.Klants", "Allergie", c => c.String());
            AddColumn("dbo.Klants", "SportHobby", c => c.String());
        }
    }
}
