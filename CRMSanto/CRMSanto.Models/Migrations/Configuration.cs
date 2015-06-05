namespace CRMSanto.Models.Migrations
{
    using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CRMSanto.Models.ApplicationDbContext>
    {
        private string pathGemeentes = AppDomain.CurrentDomain.BaseDirectory + "..\\Data\\zipcodes.txt";
        List<Gemeente> gemeentes = new List<Gemeente>();

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CRMSanto.Models.ApplicationDbContext context)
        {
            SeedGeslacht(context);
            SeedMutualiteiten(context);
            SeedSoortAfspraak(context);
            SeedWerksituatie(context);
            SeedKaraktertrek(context);
            seedProducten(context);
            //SeedAccounts(context);
        }

        public void SeedAccounts(ApplicationDbContext context)
        {            
            string roleAdmin = "Admin";
            string roleUser = "User";

            IdentityResult roleResult;

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if(!roleManager.RoleExists(roleAdmin))
            {
                roleResult = roleManager.Create(new IdentityRole(roleAdmin));
            }

            if(!roleManager.RoleExists(roleUser))
            {
                roleResult = roleManager.Create(new IdentityRole(roleUser));
            }

            if(!context.Users.Any(u=>u.Email.Equals("sandrine@massage-santo.be")))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Name = "Christiaens",
                    FirstName = "Sandrine",
                    Email = "sandrine@massage-santo.be",
                    UserName = "sandrine@massage-santo.be",
                    Address = "Burgemeester Hector Isebaertstraat 17",
                    City = "Deerlijk",
                    ZipCode = "8540"
                };
                manager.Create(user, "-Password1");
                manager.AddToRole(user.Id, roleAdmin);
            }
        }
            public void SeedGeslacht(ApplicationDbContext context)
            {
                //seedGemeentes(context);
                //GESLACHT
                context.Geslacht.AddOrUpdate<Geslacht>(g => g.Naam, new Geslacht() { Naam = "M" });
                context.Geslacht.AddOrUpdate<Geslacht>(g => g.Naam, new Geslacht() { Naam = "V" });
                context.Geslacht.AddOrUpdate<Geslacht>(g => g.Naam, new Geslacht() { Naam = "X" });
            }
                //MUTUALITEIT
            public void SeedMutualiteiten(CRMSanto.Models.ApplicationDbContext context)
            {
                context.Mutualiteit.AddOrUpdate<Mutualiteit>(m => m.Naam, new Mutualiteit() { Naam = "Christelijke Mutualiteit" });
                context.Mutualiteit.AddOrUpdate<Mutualiteit>(m => m.Naam, new Mutualiteit() { Naam = "Socialistische Mutualiteit" });
                context.Mutualiteit.AddOrUpdate<Mutualiteit>(m => m.Naam, new Mutualiteit() { Naam = "Liberale Mutualiteit" });
                context.Mutualiteit.AddOrUpdate<Mutualiteit>(m => m.Naam, new Mutualiteit() { Naam = "OZ Onafhankelijk Ziekenfonds" });
                context.Mutualiteit.AddOrUpdate<Mutualiteit>(m => m.Naam, new Mutualiteit() { Naam = "Onafhankelijk Ziekendfonds Securex" });
                context.Mutualiteit.AddOrUpdate<Mutualiteit>(m => m.Naam, new Mutualiteit() { Naam = "Partena Onafhankelijk Ziekenfonds" });
                context.Mutualiteit.AddOrUpdate<Mutualiteit>(m => m.Naam, new Mutualiteit() { Naam = "Vlaams & Neutraal Ziekenfonds" });
                context.Mutualiteit.AddOrUpdate<Mutualiteit>(m => m.Naam, new Mutualiteit() { Naam = "Symbio" });
                context.Mutualiteit.AddOrUpdate<Mutualiteit>(m => m.Naam, new Mutualiteit() { Naam = "Neutraal Ziekenfonds Vlaanderen" });
            }
            public void SeedSoortAfspraak(CRMSanto.Models.ApplicationDbContext context)
            {

                //SOORT AFSPRAAK
                context.SoortAfspraak.AddOrUpdate<SoortAfspraak>(s => s.Naam, new SoortAfspraak() { Naam = "Massage met etherische olie", Omschrijving = "De massage duurt 60 tot 75 minuten, discreet uitgevoerd met olie op de blote huid, op een massagetafel.", Duur = 60, Prijs = 50, Verplaatsingmogelijk = true });
                context.SoortAfspraak.AddOrUpdate<SoortAfspraak>(s => s.Naam, new SoortAfspraak() { Naam = "Kaarsmassage", Omschrijving = "De ontspannende kaarsmassage is een relaxerende gehele lichaamsmassage uitgevoerd met een kaars, gemaakt van een gestolde biologische etherische oliënolie in een rustige, aangenaam verwarmde ruimte.", Duur = 60, Prijs = 60, Verplaatsingmogelijk = true });
                context.SoortAfspraak.AddOrUpdate<SoortAfspraak>(s => s.Naam, new SoortAfspraak() { Naam = "Japanse gezichtsmassage", Omschrijving = "De massage duurt 60 minuten met gebruik van hoogwaardige producten van biologische kwaliteit van Ahava.", Duur = 60, Prijs = 50, Verplaatsingmogelijk = true });
                context.SoortAfspraak.AddOrUpdate<SoortAfspraak>(s => s.Naam, new SoortAfspraak() { Naam = "Fibromyssage", Omschrijving = "Wij starten altijd met een intake gesprek. Hierna zullen we jou ’plaatselijk behandelen’ én/of masseren. Hierbij gaan we samen met jou op zoek naar waar je pijn vandaan komt en/of zich situeert, en tegelijk zoeken we samen naar je pijngrens.", Duur = 30, Prijs = 30, Verplaatsingmogelijk = true });
                context.SoortAfspraak.AddOrUpdate<SoortAfspraak>(s => s.Naam, new SoortAfspraak() { Naam = "Shiatsu", Omschrijving = "Shiatsu is een massagevorm uit Japan, letterlijk vertaald ‘Vingerdruk’ en komt voort uit een lange traditie van de Oosterse Natuurlijke Geneeswijzen. Deze drukpuntmassage werkt in op de stroom van de meridianen of energiebanen in het lichaam, stimuleert de zelfhelende kracht van het lichaam en helpt om terug gezond te worden en te blijven.", Duur = 60, Prijs = 50, Verplaatsingmogelijk = true });
                context.SoortAfspraak.AddOrUpdate<SoortAfspraak>(s => s.Naam, new SoortAfspraak() { Naam = "Lomi-Lomi massage", Omschrijving = "De Lomi-Lomi massage of Hawaïaanse lichaamsmassage heeft tot doel je energiesysteem in balans te brengen. Het is een krachtige, innemende massage, vloeiend en ritmisch tegelijk als de dansende golven op de zwarte zandstranden, soms zacht en soms krachtig.", Duur = 60, Prijs = 60, Verplaatsingmogelijk = true });
            }
            public void SeedWerksituatie(CRMSanto.Models.ApplicationDbContext context)
            {
                //WERKSITUATIE
                context.Werksituatie.AddOrUpdate<Werksituatie>(w => w.Naam, new Werksituatie() { Naam = "Leerling" });
                context.Werksituatie.AddOrUpdate<Werksituatie>(w => w.Naam, new Werksituatie() { Naam = "Student" });
                context.Werksituatie.AddOrUpdate<Werksituatie>(w => w.Naam, new Werksituatie() { Naam = "Arbeider" });
                context.Werksituatie.AddOrUpdate<Werksituatie>(w => w.Naam, new Werksituatie() { Naam = "Bediende" });
                context.Werksituatie.AddOrUpdate<Werksituatie>(w => w.Naam, new Werksituatie() { Naam = "Gepensioneerd" });
                context.Werksituatie.AddOrUpdate<Werksituatie>(w => w.Naam, new Werksituatie() { Naam = "Werkzoekend" });
            }
            public void SeedKaraktertrek(CRMSanto.Models.ApplicationDbContext context)
            {
                //KARAKTERTREK
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Attent" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Bescheiden" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Betrouwbaar" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Creatief" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Consequent" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Dapper" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Dominant" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Eerlijk" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Eigenwijs" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Enthousiast" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Extrovert" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Geduldig" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "IJverig" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Introvert" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Lui" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Medelijdend" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Nadenkend" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Samenwerkend" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Slim" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Tactvol" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Trouw" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Volhardend" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Zorgzaam" });
                context.Karaktertrek.AddOrUpdate<Karaktertrek>(k => k.Naam, new Karaktertrek() { Naam = "Zuinig" });
            }
            public void seedProducten(ApplicationDbContext context)
            {
                context.Product.AddOrUpdate<Product>(p => p.Naam, new Product() { Naam = "Buikzalfje", AankoopPrijs = 100,VerkoopPrijs=200, Inhoud = 200, Foto = "http://www.zwitsal.nl/Images/1380/1380-366083-zwitsal%20mama%20buikbalsem.png", Barcode = "12548456",Omschrijving="Ideaal voor buikje groot of klein."});
                context.Product.AddOrUpdate<Product>(p => p.Naam, new Product() { Naam = "Uierzalfje", AankoopPrijs = 3000,VerkoopPrijs=5000, Inhoud = 10, Foto = "http://www.medigros.nl/media/catalog/product/u/i/uierzalf_700_gr.jpg", Barcode = "65959595", Omschrijving="Heerlijke uierzalf"});
            }
            public void seedGemeentes(ApplicationDbContext context)
            {
                using (StreamReader sr = new StreamReader(pathGemeentes))
                {
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        Gemeente g = new Gemeente();
                        g.Postcode = Convert.ToString(line.Split(',')[0]);
                        g.Plaatsnaam = Convert.ToString(line.Split(',')[1]);
                        g.Provincie = Convert.ToString(line.Split(',')[2]);

                        gemeentes.Add(g);

                        line = sr.ReadLine();
                    }
                    sr.Close();
                }
                foreach(Gemeente g in gemeentes)
                {
                    context.Gemeente.AddOrUpdate(g);
                }
                context.SaveChanges();
            }
    }
        }

