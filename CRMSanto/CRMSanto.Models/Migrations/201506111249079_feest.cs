namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MedischeFiche", "Operaties", c => c.String());
            AddColumn("dbo.MedischeFiche", "Ziektes", c => c.String());
            AddColumn("dbo.MedischeFiche", "Voedingssupplementen", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MedischeFiche", "Voedingssupplementen");
            DropColumn("dbo.MedischeFiche", "Ziektes");
            DropColumn("dbo.MedischeFiche", "Operaties");
        }
    }
}
