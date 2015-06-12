using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRMSanto.Models
{
    public class Productregistratie
    {
        public int ID { get; set; }
        public Klant Klant { get; set; }
        public Product Product { get; set; }
        [Display(Name="Datum")]
        public DateTime DatumTijdstip { get; set; }
        public int Aantal { get; set; }
        public virtual List<Winkelmand> Winkelmand { get; set; }
    }
}