using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CRMSanto.Models.PresentationModels
{
    public class NieuweProductRegistratiePM
    {
        public Product Product { get; set; }
        public int ProductID { get; set; }
        public SelectList Klanten { get; set; }
        public int KlantID { get; set; }
        public Productregistratie Productregistratie { get; set; }
        public Winkelmand Winkelmand { get; set; }
    }
}
