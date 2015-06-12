using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRMSanto.Models
{
    public class SoortAfspraak
    {
        public int ID { get; set; }
        [Required]
        public string Naam { get; set; }
        public string Omschrijving { get; set; }
        [Required]
        public int Duur { get; set; }
        public Decimal Prijs { get; set; }
        public Boolean Verplaatsingmogelijk { get; set; }
    }
}