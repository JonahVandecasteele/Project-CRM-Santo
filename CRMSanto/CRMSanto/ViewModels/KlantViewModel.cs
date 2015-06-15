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
        public List<Gemeente> Gemeentes { get; set; }
        public Karaktertrek SelectedKaracter { get;set;}
        public HttpPostedFileBase Upload { get; set; }
        public String Vandaag { get; set; }
    }
}