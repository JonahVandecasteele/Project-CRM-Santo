using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CRMSanto.Models.PresentationModels
{
    public class NieuweAfspraakPM : Afspraak
    {
        public string VolledigeNaam { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:H:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Tijdstip")]
        public DateTime Tijdstip { get; set; }
        public int KlantID { get; set; }
        public SelectList Klanten { get; set; }
        public SelectList Masseurs { get; set; }
    }
}
