using CRMSanto.BusinessLayer.Services;
using CRMSanto.Models;
using CRMSanto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMSanto.Controllers
{
    public class SettingController : Controller
    {
        private IKlantService ks;
        private IAfspraakService afs;
        public SettingController(IKlantService ks,IAfspraakService afs)
        {
            this.ks = ks;
            this.afs = afs;
        }
        public ActionResult Index()
        {
            InstellingenVM model = new InstellingenVM();
            model.Mutualiteiten = ks.GetMutualiteiten();
            model.Werksituaties = ks.GetWerkSituaties();
            model.Karaktertreken = ks.GetKaraktertreken();
            return View(model);
        }
        [HttpGet]
        public ActionResult NewKaraktertrek()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewKaraktertrek(Karaktertrek k)
        {
            ks.InsertKaraktertrek(k);
            return View();
        }
        [HttpGet]
        public ActionResult NewWerksituatie()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewWerksituatie(Werksituatie w)
        {
            ks.InsertWerkSituatie(w);
            return View();
        }
        [HttpGet]
        public ActionResult OpkuisAfspraken()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OpkuisAfspraken(DateTime van, DateTime tot)
        {
            afs.InsertArchief(van,tot);
            return View();
        }

        [HttpGet]
        public ActionResult NewMutualiteit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewMutualiteit(Mutualiteit m)
        {
            ks.InsertMutualiteit(m);
            return View();
        }
    }
}