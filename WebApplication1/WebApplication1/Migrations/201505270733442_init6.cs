namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Klant", "Hobby", c => c.String());
            AddColumn("dbo.Klant", "Allergie", c => c.String());
            AddColumn("dbo.Klant", "Blessure", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Klant", "Blessure");
            DropColumn("dbo.Klant", "Allergie");
            DropColumn("dbo.Klant", "Hobby");
        }
    }
}
