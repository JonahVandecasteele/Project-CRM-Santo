using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMSanto.Models
{
    public class Afspraak
    {
        public int ID { get; set; }
        public DateTime DatumTijdstip { get; set; }
        public Klant Klant { get; set; }
        public Boolean Verplaatsing { get; set; }
        public String Notitie { get; set; }
        public SoortAfspraak SoortAfspraak { get; set; }
        public int Duur { get; set; }
        public Boolean SoloDuo { get; set; }
        public Adres Adres { get; set; }
        public Boolean Geannuleerd { get; set; }
    }
}