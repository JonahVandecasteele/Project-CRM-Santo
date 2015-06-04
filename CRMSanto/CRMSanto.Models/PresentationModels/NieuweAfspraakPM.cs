using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CRMSanto.Models.PresentationModels
{
    public class NieuweAfspraakPM
    {
        public SelectList Klanten { get; set; }
        public Afspraak Afspraak { get; set; }
    }
}
