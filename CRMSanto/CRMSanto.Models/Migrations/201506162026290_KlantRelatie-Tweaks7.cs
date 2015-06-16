namespace CRMSanto.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KlantRelatieTweaks7 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.KlantRelatie", name: "Relatie_ID_ID", newName: "Relatie_ID");
            RenameIndex(table: "dbo.KlantRelatie", name: "IX_Relatie_ID_ID", newName: "IX_Relatie_ID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.KlantRelatie", name: "IX_Relatie_ID", newName: "IX_Relatie_ID_ID");
            RenameColumn(table: "dbo.KlantRelatie", name: "Relatie_ID", newName: "Relatie_ID_ID");
        }
    }
}
