using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMSanto.Models
{
    public class Sessie
    {
        public int ID { get; set; }
        public Klant Klant { get; set; }
        public int AantalSessies { get; set; }
    }
}