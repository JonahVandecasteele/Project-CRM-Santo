namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class massage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Afspraak", "SoortAfspraak_ID", "dbo.SoortAfspraak");
            DropIndex("dbo.Afspraak", new[] { "SoortAfspraak_ID" });
            AlterColumn("dbo.Afspraak", "SoortAfspraak_ID", c => c.Int());
            CreateIndex("dbo.Afspraak", "SoortAfspraak_ID");
            AddForeignKey("dbo.Afspraak", "SoortAfspraak_ID", "dbo.SoortAfspraak", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Afspraak", "SoortAfspraak_ID", "dbo.SoortAfspraak");
            DropIndex("dbo.Afspraak", new[] { "SoortAfspraak_ID" });
            AlterColumn("dbo.Afspraak", "SoortAfspraak_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Afspraak", "SoortAfspraak_ID");
            AddForeignKey("dbo.Afspraak", "SoortAfspraak_ID", "dbo.SoortAfspraak", "ID", cascadeDelete: true);
        }
    }
}
