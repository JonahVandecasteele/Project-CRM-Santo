namespace WebApplication1.Migrations
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
                "dbo.Geslachts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Karaktertreks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                        Klant_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Klants", t => t.Klant_ID)
                .Index(t => t.Klant_ID);
            
            CreateTable(
                "dbo.Klants",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                        Voornaam = c.String(),
                        Telefoon = c.String(),
                        Email = c.String(),
                        Geboortedatum = c.DateTime(nullable: false),
                        SportHobby = c.String(),
                        Extra = c.String(),
                        Allergie = c.String(),
                        Blessures = c.String(),
                        AndereMedische = c.String(),
                        Adres_ID = c.Int(),
                        Geslacht_ID = c.Int(),
                        Mutualiteit_ID = c.Int(),
                        Werksituatie_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Adres", t => t.Adres_ID)
                .ForeignKey("dbo.Geslachts", t => t.Geslacht_ID)
                .ForeignKey("dbo.Mutualiteits", t => t.Mutualiteit_ID)
                .ForeignKey("dbo.Werksituaties", t => t.Werksituatie_ID)
                .Index(t => t.Adres_ID)
                .Index(t => t.Geslacht_ID)
                .Index(t => t.Mutualiteit_ID)
                .Index(t => t.Werksituatie_ID);
            
            CreateTable(
                "dbo.Mutualiteits",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Werksituaties",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Klants", "Werksituatie_ID", "dbo.Werksituaties");
            DropForeignKey("dbo.Klants", "Mutualiteit_ID", "dbo.Mutualiteits");
            DropForeignKey("dbo.Karaktertreks", "Klant_ID", "dbo.Klants");
            DropForeignKey("dbo.Klants", "Geslacht_ID", "dbo.Geslachts");
            DropForeignKey("dbo.Klants", "Adres_ID", "dbo.Adres");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Klants", new[] { "Werksituatie_ID" });
            DropIndex("dbo.Klants", new[] { "Mutualiteit_ID" });
            DropIndex("dbo.Klants", new[] { "Geslacht_ID" });
            DropIndex("dbo.Klants", new[] { "Adres_ID" });
            DropIndex("dbo.Karaktertreks", new[] { "Klant_ID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Werksituaties");
            DropTable("dbo.Mutualiteits");
            DropTable("dbo.Klants");
            DropTable("dbo.Karaktertreks");
            DropTable("dbo.Geslachts");
            DropTable("dbo.Adres");
        }
    }
}
