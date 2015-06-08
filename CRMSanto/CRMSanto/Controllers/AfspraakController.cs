﻿using CRMSanto.BusinessLayer.Services;
using CRMSanto.Models;
using CRMSanto.Models.PresentationModels;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
            pm.Klanten = new SelectList(ks.GetKlanten().Select(u => new { ID = u.ID, Naam = u.Naam + " " + u.Voornaam }), "ID", "Naam");
            pm.Tijdstip = DateTime.Now;
            return View(pm);
        }

        [HttpPost]
        public ActionResult New(NieuweAfspraakPM a)
        {
            /*if (Request.Form["New"] != null)
            {*/
                if (a.Klant.ID != 0)
                {
                    a.Klant = ks.GetKlantByID(a.Klant.ID);
                }
                a.DatumTijdstip = a.DatumTijdstip.Date + a.Tijdstip.TimeOfDay;
                if (a.DatumTijdstip == DateTime.MinValue)
                    a.DatumTijdstip = (DateTime)SqlDateTime.MinValue;

                afs.AddAfspraak(a);
                return RedirectToAction("Index");
         //   }
            NieuweAfspraakPM pm = (NieuweAfspraakPM)a;
            pm.Klanten = new SelectList(ks.GetKlanten().Select(u => new { ID = u.ID, Naam = u.Naam + " " + u.Voornaam }), "ID", "Naam");
            return View(pm);
        }


    }
}