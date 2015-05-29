namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Afspraaks", newName: "Afspraak");
            RenameTable(name: "dbo.Klants", newName: "Klant");
            RenameTable(name: "dbo.Geslachts", newName: "Geslacht");
            RenameTable(name: "dbo.Karaktertreks", newName: "Karaktertrek");
            RenameTable(name: "dbo.Mutualiteits", newName: "Mutualiteit");
            RenameTable(name: "dbo.Werksituaties", newName: "Werksituatie");
            RenameTable(name: "dbo.SoortAfspraaks", newName: "SoortAfspraak");
            RenameTable(name: "dbo.Products", newName: "Product");
            RenameTable(name: "dbo.Productregistraties", newName: "Productregistratie");
            RenameTable(name: "dbo.Sessies", newName: "Sessie");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Sessie", newName: "Sessies");
            RenameTable(name: "dbo.Productregistratie", newName: "Productregistraties");
            RenameTable(name: "dbo.Product", newName: "Products");
            RenameTable(name: "dbo.SoortAfspraak", newName: "SoortAfspraaks");
            RenameTable(name: "dbo.Werksituatie", newName: "Werksituaties");
            RenameTable(name: "dbo.Mutualiteit", newName: "Mutualiteits");
            RenameTable(name: "dbo.Karaktertrek", newName: "Karaktertreks");
            RenameTable(name: "dbo.Geslacht", newName: "Geslachts");
            RenameTable(name: "dbo.Klant", newName: "Klants");
            RenameTable(name: "dbo.Afspraak", newName: "Afspraaks");
        }
    }
}
