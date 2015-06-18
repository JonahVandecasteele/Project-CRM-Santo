using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMSanto.ViewModels
{
    public class KlantViewModel : Klant
    {
        public List<Geslacht> Geslachten { get; set; }
        public List<Karaktertrek> Karaktertreken { get; set; }
        public List<Mutualiteit> Mutualiteiten { get; set; }
        public List<Werksituatie> Werksituaties { get; set; }
        public List<Klant> Klanten { get; set; }
        public List<Relatie> Relaties { get; set; }
        public List<Voedingspatroon> Voedingspatronen { get; set; }
        public List<KlantRelatie> KlantRelaties { get; set; }
        public List<Gemeente> Gemeentes { get; set; }
        public Karaktertrek SelectedKaracter { get;set;}
        public KlantRelatie SelectedKlantRelatie { get; set; }
        public HttpPostedFileBase Upload { get; set; }
        public String Vandaag { get; set; }

        public List<Productregistratie> Producten;
        public List<Afspraak> Afspraken;
    }
}