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
    public class NieuweAfspraakPM
    {
        public string VolledigeNaam { get; set; }
        public Afspraak Afspraak { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum")]
        public DateTime Datum { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Tijdstip")]
        public DateTime Tijdstip { get; set; }

        public int KlantID { get; set; }
        public SelectList Klanten { get; set; }
    }
}
