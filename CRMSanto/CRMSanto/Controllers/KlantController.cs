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
            //List<Klant> klanten = ks.GetKlanten();
            //return View(klanten);

            if (Request.Form["submit"] != null)
            {
                string zoeken = Request.Form["Search"];
                //return View(ps.GetProducten());
                List<Klant> klant = ks.GetKlanten();
                //List<Klant> klanten = ks.GetKlanten().Where(x => x.Naam.ToLower().Contains(zoeken.ToLower())).ToList();
                var klanten = from klants in klant
                              where klants.Naam.ToLower().Contains(zoeken.ToLower()) || klants.Voornaam.ToLower().Contains(zoeken.ToLower())
                                    || (klants.Naam + " " + klants.Voornaam).ToLower().Contains(zoeken.ToLower()) || (klants.Voornaam + " " + klants.Naam).ToLower().Contains(zoeken.ToLower())
                              select klants;
            return View(klanten);
        }
            else
            {
                return View(ks.GetKlanten());
            }
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
                Klant tempKlant = new Klant();
                if (TempData["NewKlantM"]==null)
                {
                    if (klant.Geslacht.ID != 0)
                klant.Geslacht = ks.GetGeslachtByID(klant.Geslacht.ID);

                    if (klant.MedischeFiche.Mutualiteit.ID != 0)
                klant.MedischeFiche.Mutualiteit = ks.GetMutualiteitByID(klant.MedischeFiche.Mutualiteit.ID);
                HttpPostedFileBase photo = klant.Upload;
                klant.Foto = photo.FileName;
                ks.SaveImage(photo);
                tempKlant = new Klant() { Voornaam = klant.Voornaam, Naam = klant.Naam, Adres = klant.Adres, Email = klant.Email,  Karaktertrek = klant.Karaktertrek, Telefoon = klant.Telefoon, Foto = klant.Foto, Geslacht = klant.Geslacht, ID = klant.ID, MedischeFiche = klant.MedischeFiche, PersoonlijkeFiche = klant.PersoonlijkeFiche };
                if (klant.Geboortedatum == DateTime.MinValue)
                    tempKlant.Geboortedatum = (DateTime)SqlDateTime.MinValue;
                else
                    tempKlant.Geboortedatum = klant.Geboortedatum;

                tempKlant.Karaktertrek = (List<Karaktertrek>)TempData["KarTrek"];

                    List<Gemeente> gemeentelist = new List<Gemeente>();
                    if (tempKlant.Adres.Gemeente == null)
                    {
                        gemeentelist = ks.GetGemeentesByPostCode(tempKlant.Adres.Postcode);
                        if (gemeentelist.Count > 1)
                        {
                            TempData["NewKlantM"] = klant;
                            KlantViewModel model = klant;
                            model.Gemeentes = gemeentelist;
                            return View(model);
                        }
                    }
                }
                else
                {
                    tempKlant = (Klant)TempData["NewKlantM"];
                    tempKlant.Adres.Gemeente = ks.GetGemeenteByID(klant.Adres.Gemeente.ID);
                }
                                    
                ks.InsertKlant(tempKlant);
                return RedirectToAction("Index");
            }
            else if(Request.Form["addkar"] != null)
            {
                KlantViewModel model = klant;
                Karaktertrek trek = ks.GetKaraktertrekByID(model.SelectedKaracter.ID);
                model.Karaktertrek = (List<Karaktertrek>)TempData["KarTrek"];
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