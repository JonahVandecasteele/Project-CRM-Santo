using CRMSanto.BusinessLayer.Services;
using CRMSanto.Models;
using CRMSanto.Models.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMSanto.Controllers
{
    public class AfspraakController : Controller
    {

        private IAfspraakService afs;
        private IKlantService ks;

        public AfspraakController(IAfspraakService afs, IKlantService ks)
        {
            this.afs = afs;
            this.ks = ks;
        }


        // GET: Afspraak
        public ActionResult Index()
        {
            return View(afs.GetAfspraken());
        }

        [HttpGet]
        public ActionResult New()
        {
           
            NieuweAfspraakPM pm = new NieuweAfspraakPM();

            var values = ks.GetKlanten().Select(u => new { ID = u.ID, Naam = u.Naam + " " + u.Voornaam });
            pm.Klanten = new SelectList(values, "ID", "Naam");
            

            pm.Afspraak = new Afspraak();
           
            return View(pm);
        }

        //[HttpPost]
        //public ActionResult NieuweAfspraak(Afspraak a)
        //{
        //    a.Klant = new SelectList(ks.GetKlanten(), "ID", "Naam");
            
        //    afs.AddAfspraak(napm);
        //    return View();
        //}


    }
}