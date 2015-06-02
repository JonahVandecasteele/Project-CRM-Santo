using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMSanto.Models
{
    public class Klant
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public Adres Adres { get; set; }
        public string Foto { get; set; }
        public string Telefoon { get; set; }
        public string Email { get; set; }
        public Geslacht Geslacht { get; set; }
        public DateTime Geboortedatum { get; set; }
        public Mutualiteit Mutualiteit { get; set; }
        public List<Karaktertrek> Karaktertrek { get; set; }
        public string Hobby { get; set; }
        public Werksituatie Werksituatie { get; set; }
        public string Medicatie { get; set; }
        public string Spijsvertering { get; set; }
        public string Allergie { get; set; }
        public string Blessure { get; set; }
        public string OperatiesZiektes { get; set; }
        public string Voedingssupplementen { get; set; }
        public string GezinRelatie { get; set; }
        public string Voedingspatroon { get; set; }
        public string HuidigeKlachten { get; set; }
    }
}