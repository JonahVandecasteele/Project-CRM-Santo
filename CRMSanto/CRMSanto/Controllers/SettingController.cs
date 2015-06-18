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
        //[Authorize]
        public ActionResult NewKaraktertrek(List<Karaktertrek> Karaktertrekken)
        {
            ViewBag.List = ks.GetKaraktertreken();
            return View();
        }

        [HttpPost]
        //[Authorize]
        public ActionResult NewKaraktertrek(Karaktertrek k)
        {
            ks.InsertKaraktertrek(k);
            return RedirectToAction("Index");
        }

        [HttpGet]
        //[Authorize]
        public ActionResult NewWerksituatie()
        {
            ViewBag.List = ks.GetWerkSituaties();
            return View();
        }

        [HttpPost]
        //[Authorize]
        public ActionResult NewWerksituatie(Werksituatie w)
        {
            ks.InsertWerkSituatie(w);
            return RedirectToAction("Index");
        }

        [HttpGet]
        //[Authorize]
        public ActionResult OpkuisAfspraken()
        {
            return View();
        }

        [HttpPost]
        //[Authorize]
        public ActionResult OpkuisAfspraken(DateTime van, DateTime tot)
        {
            afs.InsertArchief(van,tot);
            return RedirectToAction("Index");
        }

        [HttpGet]
        //[Authorize]
        public ActionResult NewMutualiteit()
        {
            ViewBag.List = ks.GetMutualiteiten();
            return View();
        }

        [HttpPost]
        //[Authorize]
        public ActionResult NewMutualiteit(Mutualiteit m)
        {
            ks.InsertMutualiteit(m);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult NewRelatie()
        {
            ViewBag.List = ks.GetRelaties();
            return View();
        }
        [HttpPost]
        public ActionResult NewRelatie(Relatie r)
        {
            ks.InsertRelatie(r);
            return RedirectToAction("Index");
        }

        [HttpGet]
        //[Authorize]
        public ActionResult NewMailImage()
        {
            return View();
        }

        [HttpPost]
        //[Authorize]
        public ActionResult NewMailImage(HttpPostedFileBase file)
        {
            if(file!=null)
            {
                ks.SaveImage(file, "verjaardagsmail.jpg");
            }

            return RedirectToAction("Index");
        }
    }
}