using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMSanto.Models
{
    public class Productregistratie
    {
        public int ID { get; set; }
        public Klant Klant { get; set; }
        public Product Product { get; set; }
        public DateTime DatumTijdstip { get; set; }
        public int Aantal { get; set; }
    }
}