using CRMSanto.Models;
using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRMSanto.Models
{
    public class Afspraak
    {
        public int ID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name="Datum")]
        public DateTime DatumTijdstip { get; set; }
        public Klant Klant { get; set; }
        public Boolean Verplaatsing { get; set; }
        [Display(Name="Extra")]
        public String Notitie { get; set; }
        [Display(Name="Massage")]
        public SoortAfspraak SoortAfspraak { get; set; }
        public int Duur { get; set; }
        [Display(Name="Duo")]
        public Boolean SoloDuo { get; set; }
        public Adres Adres { get; set; }
        public Boolean Geannuleerd { get; set; }
        public Masseur Masseur { get; set; }
    }
}