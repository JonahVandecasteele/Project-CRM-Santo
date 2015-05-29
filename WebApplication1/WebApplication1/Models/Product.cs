using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public Decimal Prijs { get; set; }
        public int Inhoud { get; set; }
        public String Foto { get; set; }
    }
}