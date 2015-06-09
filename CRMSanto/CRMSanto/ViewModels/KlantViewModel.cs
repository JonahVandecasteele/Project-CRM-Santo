using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMSanto.ViewModels
{
    public class KlantViewModel : Klant
    {
        public List<Geslacht> Geslachten { get; set; }
        public List<Karaktertrek> Karaktertreken { get; set; }
        public List<Mutualiteit> Mutualiteiten { get; set; }
        public List<Werksituatie> Werksituaties { get; set; }
        public Karaktertrek SelectedKaracter { get;set;}
        public HttpPostedFileBase Upload { get; set; }
    }
}