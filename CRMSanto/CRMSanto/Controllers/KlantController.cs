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
using System.Data.SqlTypes;

namespace CRMSanto.Controllers
{
    public class KlantController : Controller
    {
        private IKlantService ks;
        private IProductService ps;
        private IAfspraakService afs;

        public KlantController(IKlantService ks, IProductService ps, IAfspraakService afs)
        {
            this.ks = ks;
            this.ps = ps;
            this.afs = afs;
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

                var klantOpAdres = from klants in klant
                                   where klants.Adres.Gemeente.Provincie.ToLower().Contains(zoeken.ToLower())
                                   select klants;

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
            List<Productregistratie> producten = ps.GetProductregistratiesByKlantenID(klant.ID);
            List<Afspraak> afspraken = afs.GetAfsprakenByKlantenID(klant.ID);
            KlantDetailsPM kdpm = new KlantDetailsPM();
            kdpm.Klant = klant;
            kdpm.Producten = producten;
            kdpm.Afspraken = afspraken;
            return View(kdpm);
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
                klant.Foto = Guid.NewGuid().ToString();
                if (photo==null)
                {
                    if (Session["PhotoUpload"] != null)
                    {
                        photo = (HttpPostedFileBase)Session["PhotoUpload"];
                        ks.SaveImage(photo,klant.Foto);
                    }
                }
                else
                {
                    ks.SaveImage(photo, klant.Foto);
                }
                
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
                        else
                        {

                            tempKlant.Adres.Gemeente = gemeentelist.First();
                        }
                            
                    }
                }
                else
                {
                    tempKlant = (Klant)TempData["NewKlantM"];
                    tempKlant.Adres.Gemeente = ks.GetGemeenteByID(klant.Adres.Gemeente.ID);
                }
                                    
                ks.InsertKlant(tempKlant);
                ks.Mails();
                return RedirectToAction("Index");
            }
            else if(Request.Form["addkar"] != null)
            {
                KlantViewModel model = klant;
                Session["PhotoUpload"] = klant.Upload;
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
        public ActionResult Photo()
        {
            if (Session["PhotoUpload"] != null)
            {
                HttpPostedFileBase file = (HttpPostedFileBase)Session["PhotoUpload"];
                //TempData["Photo"] = file;
                var stream = file.InputStream;
                return new FileStreamResult(stream, file.ContentType);
            }
            else
            {
                return new EmptyResult();
            }
        }

        //[HttpPost]
        //public ActionResult New(Klant k)
        //{
        //    ks.AddKlant(k);
        //    return View();
        //}
    }
}