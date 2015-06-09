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
using System.Data.SqlTypes;

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
        //public ActionResult Index()
        //{
            
        //    return View(ks.GetMutualiteiten());
        //}
        public ActionResult Index()
        {
            List<Klant> klanten = ks.GetKlanten();
            return View(klanten);
        }

        public ActionResult Details(int? id) 
        {
            if (id == null) { return RedirectToAction("Index"); }
            int id2 = (int) id;
            Klant klant = ks.GetKlantByID(id2);
            if (klant == null) { return RedirectToAction("Index"); }
            return View(klant);
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
                Klant tempKlant = new Klant() { Voornaam = klant.Voornaam, Naam = klant.Naam, Adres = klant.Adres, Email = klant.Email,  Karaktertrek = klant.Karaktertrek, Telefoon = klant.Telefoon, Foto = klant.Foto, Geslacht = klant.Geslacht, ID = klant.ID, MedischeFiche = klant.MedischeFiche, PersoonlijkeFiche = klant.PersoonlijkeFiche };
                if (klant.Geboortedatum == DateTime.MinValue)
                    tempKlant.Geboortedatum = (DateTime)SqlDateTime.MinValue;
                else
                    tempKlant.Geboortedatum = klant.Geboortedatum;
                tempKlant.Karaktertrek = (List<Karaktertrek>)TempData["KarTrek"];
                ks.InsertKlant(tempKlant);
                return RedirectToAction("Index");
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
                TempData["KarTrek"] = model.Karaktertrek;
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