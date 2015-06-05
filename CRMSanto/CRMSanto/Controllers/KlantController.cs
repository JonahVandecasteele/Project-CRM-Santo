﻿using CRMSanto.BusinessLayer.Repository;
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
        public ActionResult New(KlantViewModel klant)
        {
            if (Request.Form["Create"] != null)
            {
                if(klant.Geslacht.ID!=0)
                klant.Geslacht = ks.GetGeslachtByID(klant.Geslacht.ID);
                if(klant.MedischeFiche.Mutualiteit.ID!=0)
                klant.MedischeFiche.Mutualiteit = ks.GetMutualiteitByID(klant.MedischeFiche.Mutualiteit.ID);
                ks.InsertKlant(klant);
                return RedirectToAction("AllKlanten");
            }
            else if(Request.Form["addkar"] != null)
            {
                KlantViewModel model = klant;
                Karaktertrek trek = ks.GetKaraktertrekByID(model.SelectedKaracter.ID);
                if (model.Karaktertrek == null)
                {
                    model.Karaktertrek = new List<Karaktertrek>();
                }
                model.Karaktertrek.Add(trek);
                model.Geslachten = ks.GetGeslachten();
                model.Mutualiteiten = ks.GetMutualiteiten();
                model.Werksituaties = ks.GetWerkSituaties();
                model.Karaktertreken = ks.GetKaraktertreken();
                return View(model);
            }
            return View();
            
        }

        //[HttpPost]
        //public ActionResult New(Klant k)
        //{
        //    ks.AddKlant(k);
        //    return View();
        //}
    }
}