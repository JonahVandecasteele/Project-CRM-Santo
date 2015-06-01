namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbtest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Klant", "Foto", c => c.String());
            AddColumn("dbo.Product", "Barcode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Barcode");
            DropColumn("dbo.Klant", "Foto");
        }
    }
}
