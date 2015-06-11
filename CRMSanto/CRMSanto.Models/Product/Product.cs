using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CRMSanto.Models
{
    public class Product
    {
        public int ID { get; set; }
        public int ProductNr { get; set; }
        public string Naam { get; set; }
        public Decimal AankoopPrijs { get; set; }
        public Decimal VerkoopPrijs { get; set; }
        [Display(Name="Inhoud (in ml of gram)")]
        public int Inhoud { get; set; }
        public string Foto { get; set; }
        public string Omschrijving { get; set; }
        public int MinStock { get; set; }
        public string Barcode { get; set; }
        [NotMapped]
        public HttpPostedFileBase Upload { get; set; }
    }
}