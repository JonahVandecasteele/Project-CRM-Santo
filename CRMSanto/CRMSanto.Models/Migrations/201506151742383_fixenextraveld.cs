namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixenextraveld : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExtraAfspraak", "Extra_ID", "dbo.Extra");
            DropForeignKey("dbo.ExtraAfspraak", "Afspraak_ID", "dbo.Afspraak");
            DropIndex("dbo.ExtraAfspraak", new[] { "Extra_ID" });
            DropIndex("dbo.ExtraAfspraak", new[] { "Afspraak_ID" });
            AddColumn("dbo.Afspraak", "Extra_ID", c => c.Int());
            CreateIndex("dbo.Afspraak", "Extra_ID");
            AddForeignKey("dbo.Afspraak", "Extra_ID", "dbo.Extra", "ID");
            DropTable("dbo.ExtraAfspraak");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ExtraAfspraak",
                c => new
                    {
                        Extra_ID = c.Int(nullable: false),
                        Afspraak_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Extra_ID, t.Afspraak_ID });
            
            DropForeignKey("dbo.Afspraak", "Extra_ID", "dbo.Extra");
            DropIndex("dbo.Afspraak", new[] { "Extra_ID" });
            DropColumn("dbo.Afspraak", "Extra_ID");
            CreateIndex("dbo.ExtraAfspraak", "Afspraak_ID");
            CreateIndex("dbo.ExtraAfspraak", "Extra_ID");
            AddForeignKey("dbo.ExtraAfspraak", "Afspraak_ID", "dbo.Afspraak", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ExtraAfspraak", "Extra_ID", "dbo.Extra", "ID", cascadeDelete: true);
        }
    }
}
