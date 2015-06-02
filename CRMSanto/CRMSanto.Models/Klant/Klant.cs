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
        public virtual Adres Adres { get; set; }
        public string Foto { get; set; }
        public string Telefoon { get; set; }
        public string Email { get; set; }
        public virtual Geslacht Geslacht { get; set; }
        public DateTime Geboortedatum { get; set; }
        public virtual List<Karaktertrek> Karaktertrek { get; set; }
    }
}