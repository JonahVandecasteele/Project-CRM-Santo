namespace CRMSanto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adres",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Straat = c.String(),
                        Nummer = c.String(),
                        Postbus = c.String(),
                        Postcode = c.String(),
                        Gemeente = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Afspraak",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DatumTijdstip = c.DateTime(nullable: false),
                        Verplaatsing = c.Boolean(nullable: false),
                        Notitie = c.String(),
                        Duur = c.Int(nullable: false),
                        SoloDuo = c.Boolean(nullable: false),
                        Geannuleerd = c.Boolean(nullable: false),
                        Adres_ID = c.Int(),
                        Klant_ID = c.Int(),
                        SoortAfspraak_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Adres", t => t.Adres_ID)
                .ForeignKey("dbo.Klant", t => t.Klant_ID)
                .ForeignKey("dbo.SoortAfspraak", t => t.SoortAfspraak_ID)
                .Index(t => t.Adres_ID)
                .Index(t => t.Klant_ID)
                .Index(t => t.SoortAfspraak_ID);
            
            CreateTable(
                "dbo.Klant",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                        Voornaam = c.String(),
                        Foto = c.String(),
                        Telefoon = c.String(),
                        Email = c.String(),
                        Geboortedatum = c.DateTime(nullable: false),
                        Hobby = c.String(),
                        Medicatie = c.String(),
                        Spijsvertering = c.String(),
                        Allergie = c.String(),
                        Blessure = c.String(),
                        OperatiesZiektes = c.String(),
                        Voedingssupplementen = c.String(),
                        GezinRelatie = c.String(),
                        Voedingspatroon = c.String(),
                        HuidigeKlachten = c.String(),
                        Adres_ID = c.Int(),
                        Geslacht_ID = c.Int(),
                        Mutualiteit_ID = c.Int(),
                        Werksituatie_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Adres", t => t.Adres_ID)
                .ForeignKey("dbo.Geslacht", t => t.Geslacht_ID)
                .ForeignKey("dbo.Mutualiteit", t => t.Mutualiteit_ID)
                .ForeignKey("dbo.Werksituatie", t => t.Werksituatie_ID)
                .Index(t => t.Adres_ID)
                .Index(t => t.Geslacht_ID)
                .Index(t => t.Mutualiteit_ID)
                .Index(t => t.Werksituatie_ID);
            
            CreateTable(
                "dbo.Geslacht",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Karaktertrek",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                        Klant_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Klant", t => t.Klant_ID)
                .Index(t => t.Klant_ID);
            
            CreateTable(
                "dbo.Mutualiteit",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Werksituatie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SoortAfspraak",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                        Omschrijving = c.String(),
                        Duur = c.Int(nullable: false),
                        Prijs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Verplaatsingmogelijk = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                        Prijs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Inhoud = c.Int(nullable: false),
                        Foto = c.String(),
                        Barcode = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Productregistratie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DatumTijdstip = c.DateTime(nullable: false),
                        Aantal = c.Int(nullable: false),
                        Klant_ID = c.Int(),
                        Product_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Klant", t => t.Klant_ID)
                .ForeignKey("dbo.Product", t => t.Product_ID)
                .Index(t => t.Klant_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Sessie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AantalSessies = c.Int(nullable: false),
                        Klant_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Klant", t => t.Klant_ID)
                .Index(t => t.Klant_ID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Sessie", "Klant_ID", "dbo.Klant");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Productregistratie", "Product_ID", "dbo.Product");
            DropForeignKey("dbo.Productregistratie", "Klant_ID", "dbo.Klant");
            DropForeignKey("dbo.Afspraak", "SoortAfspraak_ID", "dbo.SoortAfspraak");
            DropForeignKey("dbo.Afspraak", "Klant_ID", "dbo.Klant");
            DropForeignKey("dbo.Klant", "Werksituatie_ID", "dbo.Werksituatie");
            DropForeignKey("dbo.Klant", "Mutualiteit_ID", "dbo.Mutualiteit");
            DropForeignKey("dbo.Karaktertrek", "Klant_ID", "dbo.Klant");
            DropForeignKey("dbo.Klant", "Geslacht_ID", "dbo.Geslacht");
            DropForeignKey("dbo.Klant", "Adres_ID", "dbo.Adres");
            DropForeignKey("dbo.Afspraak", "Adres_ID", "dbo.Adres");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Sessie", new[] { "Klant_ID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Productregistratie", new[] { "Product_ID" });
            DropIndex("dbo.Productregistratie", new[] { "Klant_ID" });
            DropIndex("dbo.Karaktertrek", new[] { "Klant_ID" });
            DropIndex("dbo.Klant", new[] { "Werksituatie_ID" });
            DropIndex("dbo.Klant", new[] { "Mutualiteit_ID" });
            DropIndex("dbo.Klant", new[] { "Geslacht_ID" });
            DropIndex("dbo.Klant", new[] { "Adres_ID" });
            DropIndex("dbo.Afspraak", new[] { "SoortAfspraak_ID" });
            DropIndex("dbo.Afspraak", new[] { "Klant_ID" });
            DropIndex("dbo.Afspraak", new[] { "Adres_ID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Sessie");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Productregistratie");
            DropTable("dbo.Product");
            DropTable("dbo.SoortAfspraak");
            DropTable("dbo.Werksituatie");
            DropTable("dbo.Mutualiteit");
            DropTable("dbo.Karaktertrek");
            DropTable("dbo.Geslacht");
            DropTable("dbo.Klant");
            DropTable("dbo.Afspraak");
            DropTable("dbo.Adres");
        }
    }
}
