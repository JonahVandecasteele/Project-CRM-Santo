using System;
using System.Collections.Generic;
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
        public int KlantID { get; set; }
        public SelectList Klanten { get; set; }
        public SelectList Masseurs { get; set; }
    }
}
