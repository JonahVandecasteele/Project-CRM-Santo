using CRMSanto.BusinessLayer.Repository;
using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMSanto.BusinessLayer.Services;
using System.IO;
using CRMSanto.ViewModels;

namespace CRMSanto.Controllers
{
    public class KlantController : Controller
    {
        private IKlantService ks;
        
        public KlantController(IKlantService ks)
        {
            this.ks = ks;
        }
        // GET: Klant
        public ActionResult Index()
        {
            
            return View(ks.GetMutualiteiten());
        }
        public ActionResult AllKlanten()
        {

            return View(ks.GetKlanten());
        }

        public ActionResult New()
        {
            KlantViewModel model = new KlantViewModel();
            model.Geslachten = ks.GetGeslachten();
            model.Mutualiteiten = ks.GetMutualiteiten();
            model.Werksituaties = ks.GetWerkSituaties();
            model.Karaktertreken = ks.GetKaraktertreken();
            return View(model);
        }
        [HttpPost]
        public ActionResult New(Klant klant)
        {
            klant.Geslacht = ks.GetGeslachtByID(klant.Geslacht.ID);
            klant.Karaktertrek = new List<Karaktertrek>();
            ks.InsertKlant(klant);
            KlantViewModel model = (KlantViewModel)klant;
            model.Geslachten = ks.GetGeslachten();
            model.Mutualiteiten = ks.GetMutualiteiten();
            model.Werksituaties = ks.GetWerkSituaties();
            model.Karaktertreken = ks.GetKaraktertreken();
            return View(model);
        }

        //[HttpPost]
        //public ActionResult New(Klant k)
        //{
        //    ks.AddKlant(k);
        //    return View();
        //}
    }
}