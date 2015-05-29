using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Klant
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public Adres Adres { get; set; }
        public string Telefoon { get; set; }
        public string Email { get; set; }
        public Geslacht Geslacht { get; set; }
        public DateTime Geboortedatum { get; set; }
        public Mutualiteit Mutualiteit { get; set; }
        public List<Karaktertrek> Karaktertrek { get; set; }
        public string Hobby { get; set; }
        public Werksituatie Werksituatie { get; set; }
        public string Extra { get; set; }
        public string Allergie { get; set; }
        public string Blessure { get; set; }
        public string AndereMedische { get; set; }
    }
}