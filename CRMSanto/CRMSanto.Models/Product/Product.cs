using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMSanto.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public Decimal AankoopPrijs { get; set; }
        public Decimal VerkoopPrijs { get; set; }
        public int Inhoud { get; set; }
        public string Foto { get; set; }
        public string Omschrijving { get; set; }
        public string Barcode { get; set; }
    }
}