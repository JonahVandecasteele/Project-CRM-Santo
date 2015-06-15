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
            return View();
        }
        [HttpGet]
        public ActionResult NewKaraktertrek(List<Karaktertrek> Karaktertrekken)
        {
            ViewBag.List = ks.GetKaraktertreken();
            return View();
        }
        [HttpPost]
        public ActionResult NewKaraktertrek(Karaktertrek k)
        {
            ks.InsertKaraktertrek(k);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult NewWerksituatie()
        {
            ViewBag.List = ks.GetWerkSituaties();
            return View();
        }
        [HttpPost]
        public ActionResult NewWerksituatie(Werksituatie w)
        {
            ks.InsertWerkSituatie(w);
            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult NewMutualiteit()
        {
            ViewBag.List = ks.GetMutualiteiten();
            return View();
        }
        [HttpPost]
        public ActionResult NewMutualiteit(Mutualiteit m)
        {
            ks.InsertMutualiteit(m);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult NewRelatie()
        {
            ViewBag.List = ks.GetMutualiteiten();
            return View();
        }
        [HttpPost]
        public ActionResult NewRelatie(Mutualiteit m)
        {
            ks.InsertMutualiteit(m);
            return RedirectToAction("Index");
        }
    }
}