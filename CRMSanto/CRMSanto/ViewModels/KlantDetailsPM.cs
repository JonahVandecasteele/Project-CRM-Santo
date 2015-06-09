using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMSanto.ViewModels
{
    public class KlantDetailsPM
    {
        public Klant Klant;
        public List<Productregistratie> Producten;
        public List<Afspraak> Afspraken;
    }
}