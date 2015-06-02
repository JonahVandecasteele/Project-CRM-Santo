namespace CRMSanto.Migrations
{
    using CRMSanto.Models;
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
            seedGemeentes(context);
            //GESLACHT
            context.Geslacht.AddOrUpdate<Geslacht>(g => g.Naam, new Geslacht() { Naam = "M" });
            context.Geslacht.AddOrUpdate<Geslacht>(g => g.Naam, new Geslacht() { Naam = "V" });
            context.Geslacht.AddOrUpdate<Geslacht>(g => g.Naam, new Geslacht() { Naam = "X" });

            //MUTUALITEIT
            context.Mutualiteit.AddOrUpdate<Mutualiteit>(m => m.Naam, new Mutualiteit() { Naam = "Christelijke Mutualiteit" });
            context.Mutualiteit.AddOrUpdate<Mutualiteit>(m => m.Naam, new Mutualiteit() { Naam = "Socialistische Mutualiteit" });
            context.Mutualiteit.AddOrUpdate<Mutualiteit>(m => m.Naam, new Mutualiteit() { Naam = "Liberale Mutualiteit" });
            context.Mutualiteit.AddOrUpdate<Mutualiteit>(m => m.Naam, new Mutualiteit() { Naam = "OZ Onafhankelijk Ziekenfonds" });
            context.Mutualiteit.AddOrUpdate<Mutualiteit>(m => m.Naam, new Mutualiteit() { Naam = "Onafhankelijk Ziekendfonds Securex" });
            context.Mutualiteit.AddOrUpdate<Mutualiteit>(m => m.Naam, new Mutualiteit() { Naam = "Partena Onafhankelijk Ziekenfonds" });
            context.Mutualiteit.AddOrUpdate<Mutualiteit>(m => m.Naam, new Mutualiteit() { Naam = "Vlaams & Neutraal Ziekenfonds" });
            context.Mutualiteit.AddOrUpdate<Mutualiteit>(m => m.Naam, new Mutualiteit() { Naam = "Symbio" });
            context.Mutualiteit.AddOrUpdate<Mutualiteit>(m => m.Naam, new Mutualiteit() { Naam = "Neutraal Ziekenfonds Vlaanderen" });

            //SOORT AFSPRAAK
            context.SoortAfspraak.AddOrUpdate<SoortAfspraak>(s => s.Naam, new SoortAfspraak() { Naam = "Massage met etherische olie", Omschrijving = "De massage duurt 60 tot 75 minuten, discreet uitgevoerd met olie op de blote huid, op een massagetafel.", Duur = 60, Prijs = 50, Verplaatsingmogelijk = true });
            context.SoortAfspraak.AddOrUpdate<SoortAfspraak>(s => s.Naam, new SoortAfspraak() { Naam = "Kaarsmassage", Omschrijving = "De ontspannende kaarsmassage is een relaxerende gehele lichaamsmassage uitgevoerd met een kaars, gemaakt van een gestolde biologische etherische oli�nolie in een rustige, aangenaam verwarmde ruimte.", Duur = 60, Prijs = 60, Verplaatsingmogelijk = true });
            context.SoortAfspraak.AddOrUpdate<SoortAfspraak>(s => s.Naam, new SoortAfspraak() { Naam = "Japanse gezichtsmassage", Omschrijving = "De massage duurt 60 minuten met gebruik van hoogwaardige producten van biologische kwaliteit van Ahava.", Duur = 60, Prijs = 50, Verplaatsingmogelijk = true });
            context.SoortAfspraak.AddOrUpdate<SoortAfspraak>(s => s.Naam, new SoortAfspraak() { Naam = "Fibromyssage", Omschrijving = "Wij starten altijd met een intake gesprek. Hierna zullen we jou �plaatselijk behandelen� �n/of masseren. Hierbij gaan we samen met jou op zoek naar waar je pijn vandaan komt en/of zich situeert, en tegelijk zoeken we samen naar je pijngrens.", Duur = 30, Prijs = 30, Verplaatsingmogelijk = true });
            context.SoortAfspraak.AddOrUpdate<SoortAfspraak>(s => s.Naam, new SoortAfspraak() { Naam = "Shiatsu", Omschrijving = "Shiatsu is een massagevorm uit Japan, letterlijk vertaald �Vingerdruk� en komt voort uit een lange traditie van de Oosterse Natuurlijke Geneeswijzen. Deze drukpuntmassage werkt in op de stroom van de meridianen of energiebanen in het lichaam, stimuleert de zelfhelende kracht van het lichaam en helpt om terug gezond te worden en te blijven.", Duur = 60, Prijs = 50, Verplaatsingmogelijk = true });
            context.SoortAfspraak.AddOrUpdate<SoortAfspraak>(s => s.Naam, new SoortAfspraak() { Naam = "Lomi-Lomi massage", Omschrijving = "De Lomi-Lomi massage of Hawa�aanse lichaamsmassage heeft tot doel je energiesysteem in balans te brengen. Het is een krachtige, innemende massage, vloeiend en ritmisch tegelijk als de dansende golven op de zwarte zandstranden, soms zacht en soms krachtig.", Duur = 60, Prijs = 60, Verplaatsingmogelijk = true });

            //WERKSITUATIE
            context.Werksituatie.AddOrUpdate<Werksituatie>(w => w.Naam, new Werksituatie() { Naam = "Leerling" });
            context.Werksituatie.AddOrUpdate<Werksituatie>(w => w.Naam, new Werksituatie() { Naam = "Student" });
            context.Werksituatie.AddOrUpdate<Werksituatie>(w => w.Naam, new Werksituatie() { Naam = "Arbeider" });
            context.Werksituatie.AddOrUpdate<Werksituatie>(w => w.Naam, new Werksituatie() { Naam = "Bediende" });
            context.Werksituatie.AddOrUpdate<Werksituatie>(w => w.Naam, new Werksituatie() { Naam = "Gepensioneerd" });
            context.Werksituatie.AddOrUpdate<Werksituatie>(w => w.Naam, new Werksituatie() { Naam = "Werkzoekend" });

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
