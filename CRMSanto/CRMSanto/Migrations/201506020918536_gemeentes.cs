namespace CRMSanto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gemeentes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Gemeente",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Postcode = c.String(),
                        Plaatsnaam = c.String(),
                        Provincie = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Gemeente");
        }
    }
}
