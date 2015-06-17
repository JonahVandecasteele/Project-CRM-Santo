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
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;

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
        public ActionResult Index(string sortOrder, string sortGeslacht)
        {
            List<Geslacht> geslachten = ks.GetGeslachten();
            List<SelectListItem> items = new List<SelectListItem>();
            ViewBag.Leeftijdvan = 0;
            ViewBag.Leeftijdtot = 100;

            if (TempData["Klanten"] == null)
            {
                TempData["Klanten"] = ks.GetKlanten();
            }

            foreach (Geslacht geslacht in geslachten)
            {
            items.Add(new SelectListItem { Text = geslacht.Naam, Value = geslacht.ID.ToString() });
            
            }
           

            ViewData["Options"] = items;


            KlantViewModel kv = new KlantViewModel();
            
            kv.Geslachten = ks.GetGeslachten();
            kv.Gemeentes = ks.GetGemeentes();
            //List<Klant> klanten = ks.GetKlanten();
            //return View(klanten);
            if (Request.Form["submit"] != null)
            {
                if (Request.Form["Search"] != "")
                {
                    TempData["Search"] = Request.Form["Search"];
                    string zoeken = Request.Form["Search"];
                   
                    List<Klant> klant = (List<Klant>)TempData["Klanten"];
  

                    var klanten = from klants in klant
                                  where klants.Naam.ToLower().Contains(zoeken.ToLower()) || klants.Voornaam.ToLower().Contains(zoeken.ToLower())
                                        || (klants.Naam + " " + klants.Voornaam).ToLower().Contains(zoeken.ToLower()) || (klants.Voornaam + " " + klants.Naam).ToLower().Contains(zoeken.ToLower())
                                        || klants.Adres.Gemeente.Plaatsnaam.ToLower().Contains(zoeken.ToLower()) || (klants.Adres.Gemeente.Plaatsnaam + " " + klants.Adres.Gemeente.Postcode).ToLower().Contains(zoeken.ToLower())
                                        || (klants.Adres.Gemeente.Postcode + " " + klants.Adres.Gemeente.Plaatsnaam).ToLower().Contains(zoeken.ToLower())
                                        || klants.Adres.Gemeente.Provincie.ToLower().Contains(zoeken.ToLower())
                                        || klants.Adres.Gemeente.Postcode.ToLower().Contains(zoeken.ToLower())
                                  select klants;
                    TempData["Klanten"] = klanten.ToList();
                    return View(klanten);
                }
               
                else { 
                    return View(ks.GetKlanten());
                }
            }
            else if (Request.Form["filterDit"] != null)
            {

                List<Klant> klant = ks.GetKlanten();
                string geslacht = Request.Form["Options"];
                int leeftijdVan = Convert.ToInt32(Request.Form["LeeftijdVan"]);
                int leeftijdTot = Convert.ToInt32(Request.Form["LeeftijdTot"]);

                TempData["Search"] = Request.Form["Search"];
                //ViewBag.Options = Request.Form["Options"];
                ViewBag.Geslacht = Request.Form["Options"];
                ViewBag.LeeftijdVan = Request.Form["LeeftijdVan"];
                ViewBag.LeeftijdTot = Request.Form["LeeftijdTot"];

                string zoeken = Request.Form["Search"];

                int minLeeftijd = DateTime.Now.Year - leeftijdVan;
                int maxLeeftijd = DateTime.Now.Year - leeftijdTot;

                if (geslacht != null && leeftijdVan == 0 && leeftijdTot == 100 && string.IsNullOrEmpty(zoeken))
                {
                    ViewBag.sortGeslacht = sortGeslacht;

                    ViewBag.NameSortParm = sortOrder == "name_desc" ? "name" : "name_desc";
                    ViewBag.FirstNameSortParm = sortOrder == "firstname" ? "firstName_desc" : "firstname";
                    ViewBag.PhoneSortParm = sortOrder == "phoneNumber" ? "phonenumber_desc" : "phoneNumber";
                    ViewBag.EmailSortParm = sortOrder == "email" ? "email_desc" : "email";
                    ViewBag.AdresSortParm = sortOrder == "adres" ? "adres_desc" : "adres";
                    ViewBag.GemeenteSortParm = sortOrder == "gemeente" ? "gemeente_desc" : "gemeente";

                    var klantOpGeslacht = from klants in klant
                                          where klants.Geslacht.ID.ToString().Contains(geslacht)
                                          select klants;
                    var klanten = klantOpGeslacht;

                    switch (sortOrder)
                    {
                        case "name":
                            klanten = klanten.OrderBy(s => s.Naam).ToList();
                            break;
                        case "name_desc":
                            klanten = klanten.OrderByDescending(s => s.Naam).ToList();
                            break;
                        case "firstname":
                            klanten = klanten.OrderBy(s => s.Voornaam).ToList();
                            break;
                        case "firstName_desc":
                            klanten = klanten.OrderByDescending(s => s.Voornaam).ToList();
                            break;
                        case "phoneNumber":
                            klanten = klanten.OrderBy(s => s.Telefoon).ToList();
                            break;
                        case "phoneNumber_desc":
                            klanten = klanten.OrderByDescending(s => s.Telefoon).ToList();
                            break;
                        case "email":
                            klanten = klanten.OrderBy(s => s.Email).ToList();
                            break;
                        case "email_desc":
                            klanten = klanten.OrderByDescending(s => s.Email).ToList();
                            break;
                        case "adres":
                            klanten = klanten.OrderBy(s => s.Adres.Straat).ToList();
                            break;
                        case "adres_desc":
                            klanten = klanten.OrderByDescending(s => s.Adres.Straat).ToList();
                            break;
                        case "gemeente":
                            klanten = klanten.OrderBy(s => s.Adres.Gemeente.Plaatsnaam).ToList();
                            break;
                        case "gemeente_desc":
                            klanten = klanten.OrderByDescending(s => s.Adres.Gemeente.Plaatsnaam).ToList();
                            break;
                        default:
                            klanten = klanten.OrderBy(s => s.Naam).ToList();
                            break;
                    }

                    TempData["Klanten"] = klanten.ToList();
                    return View(klanten);
                }
                else if (geslacht != null && leeftijdVan == 0 && leeftijdTot == 100 && !string.IsNullOrEmpty(zoeken))
                {
                    ViewBag.NameSortParm = sortOrder == "name_desc" ? "name" : "name_desc";
                    ViewBag.FirstNameSortParm = sortOrder == "firstname" ? "firstName_desc" : "firstname";
                    ViewBag.PhoneSortParm = sortOrder == "phoneNumber" ? "phonenumber_desc" : "phoneNumber";
                    ViewBag.EmailSortParm = sortOrder == "email" ? "email_desc" : "email";
                    ViewBag.AdresSortParm = sortOrder == "adres" ? "adres_desc" : "adres";
                    ViewBag.GemeenteSortParm = sortOrder == "gemeente" ? "gemeente_desc" : "gemeente";

                    

                    var klantOpGeslachtEnSearch = from klants in klant
                                                  where klants.Naam.ToLower().Contains(zoeken.ToLower()) || klants.Voornaam.ToLower().Contains(zoeken.ToLower())
                                                        || (klants.Naam + " " + klants.Voornaam).ToLower().Contains(zoeken.ToLower()) || (klants.Voornaam + " " + klants.Naam).ToLower().Contains(zoeken.ToLower())
                                                        || klants.Adres.Gemeente.Plaatsnaam.ToLower().Contains(zoeken.ToLower()) || (klants.Adres.Gemeente.Plaatsnaam + " " + klants.Adres.Gemeente.Postcode).ToLower().Contains(zoeken.ToLower())
                                                        || (klants.Adres.Gemeente.Postcode + " " + klants.Adres.Gemeente.Plaatsnaam).ToLower().Contains(zoeken.ToLower())
                                                        || klants.Adres.Gemeente.Provincie.ToLower().Contains(zoeken.ToLower())
                                                        || klants.Adres.Gemeente.Postcode.ToLower().Contains(zoeken.ToLower()) 
                                                  select klants;

                    var klanten = klantOpGeslachtEnSearch.Where(a => a.Geslacht.ID.ToString().Contains(geslacht));
                    
                    switch (sortOrder)
                    {
                        case "name":
                            klanten = klanten.OrderBy(s => s.Naam).ToList();
                            break;
                        case "name_desc":
                            klanten = klanten.OrderByDescending(s => s.Naam).ToList();
                            break;
                        case "firstname":
                            klanten = klanten.OrderBy(s => s.Voornaam).ToList();
                            break;
                        case "firstName_desc":
                            klanten = klanten.OrderByDescending(s => s.Voornaam).ToList();
                            break;
                        case "phoneNumber":
                            klanten = klanten.OrderBy(s => s.Telefoon).ToList();
                            break;
                        case "phoneNumber_desc":
                            klanten = klanten.OrderByDescending(s => s.Telefoon).ToList();
                            break;
                        case "email":
                            klanten = klanten.OrderBy(s => s.Email).ToList();
                            break;
                        case "email_desc":
                            klanten = klanten.OrderByDescending(s => s.Email).ToList();
                            break;
                        case "adres":
                            klanten = klanten.OrderBy(s => s.Adres.Straat).ToList();
                            break;
                        case "adres_desc":
                            klanten = klanten.OrderByDescending(s => s.Adres.Straat).ToList();
                            break;
                        case "gemeente":
                            klanten = klanten.OrderBy(s => s.Adres.Gemeente.Plaatsnaam).ToList();
                            break;
                        case "gemeente_desc":
                            klanten = klanten.OrderByDescending(s => s.Adres.Gemeente.Plaatsnaam).ToList();
                            break;
                        default:
                            klanten = klanten.OrderBy(s => s.Naam).ToList();
                            break;
                    }
                    TempData["Klanten"] = klanten.ToList();
                    return View(klanten);
                }
                else if (geslacht == null && leeftijdVan != 0 && leeftijdTot != 100 && !string.IsNullOrEmpty(zoeken))
                {
                    ViewBag.NameSortParm = sortOrder == "name_desc" ? "name" : "name_desc";
                    ViewBag.FirstNameSortParm = sortOrder == "firstname" ? "firstName_desc" : "firstname";
                    ViewBag.PhoneSortParm = sortOrder == "phoneNumber" ? "phonenumber_desc" : "phoneNumber";
                    ViewBag.EmailSortParm = sortOrder == "email" ? "email_desc" : "email";
                    ViewBag.AdresSortParm = sortOrder == "adres" ? "adres_desc" : "adres";
                    ViewBag.GemeenteSortParm = sortOrder == "gemeente" ? "gemeente_desc" : "gemeente";

                    var klantOpLeeftijdEnSearch = from klants in klant
                                                  where klants.Naam.ToLower().Contains(zoeken.ToLower()) || klants.Voornaam.ToLower().Contains(zoeken.ToLower())
                                                        || (klants.Naam + " " + klants.Voornaam).ToLower().Contains(zoeken.ToLower()) || (klants.Voornaam + " " + klants.Naam).ToLower().Contains(zoeken.ToLower())
                                                        || klants.Adres.Gemeente.Plaatsnaam.ToLower().Contains(zoeken.ToLower()) || (klants.Adres.Gemeente.Plaatsnaam + " " + klants.Adres.Gemeente.Postcode).ToLower().Contains(zoeken.ToLower())
                                                        || (klants.Adres.Gemeente.Postcode + " " + klants.Adres.Gemeente.Plaatsnaam).ToLower().Contains(zoeken.ToLower())
                                                        || klants.Adres.Gemeente.Provincie.ToLower().Contains(zoeken.ToLower())
                                                        || klants.Adres.Gemeente.Postcode.ToLower().Contains(zoeken.ToLower())
                                                  select klants;
                    var klanten = klantOpLeeftijdEnSearch.Where(a => a.Geboortedatum.Year <= minLeeftijd && a.Geboortedatum.Year >= maxLeeftijd);

                    switch (sortOrder)
                    {
                        case "name":
                            klanten = klanten.OrderBy(s => s.Naam).ToList();
                            break;
                        case "name_desc":
                            klanten = klanten.OrderByDescending(s => s.Naam).ToList();
                            break;
                        case "firstname":
                            klanten = klanten.OrderBy(s => s.Voornaam).ToList();
                            break;
                        case "firstName_desc":
                            klanten = klanten.OrderByDescending(s => s.Voornaam).ToList();
                            break;
                        case "phoneNumber":
                            klanten = klanten.OrderBy(s => s.Telefoon).ToList();
                            break;
                        case "phoneNumber_desc":
                            klanten = klanten.OrderByDescending(s => s.Telefoon).ToList();
                            break;
                        case "email":
                            klanten = klanten.OrderBy(s => s.Email).ToList();
                            break;
                        case "email_desc":
                            klanten = klanten.OrderByDescending(s => s.Email).ToList();
                            break;
                        case "adres":
                            klanten = klanten.OrderBy(s => s.Adres.Straat).ToList();
                            break;
                        case "adres_desc":
                            klanten = klanten.OrderByDescending(s => s.Adres.Straat).ToList();
                            break;
                        case "gemeente":
                            klanten = klanten.OrderBy(s => s.Adres.Gemeente.Plaatsnaam).ToList();
                            break;
                        case "gemeente_desc":
                            klanten = klanten.OrderByDescending(s => s.Adres.Gemeente.Plaatsnaam).ToList();
                            break;
                        default:
                            klanten = klanten.OrderBy(s => s.Naam).ToList();
                            break;
                    }
                    TempData["Klanten"] = klanten.ToList();
                    return View(klanten);
                }
                else if(geslacht != null && leeftijdVan != 0 && leeftijdTot != 100 && !string.IsNullOrEmpty(zoeken))
                {
                    ViewBag.NameSortParm = sortOrder == "name_desc" ? "name" : "name_desc";
                    ViewBag.FirstNameSortParm = sortOrder == "firstname" ? "firstName_desc" : "firstname";
                    ViewBag.PhoneSortParm = sortOrder == "phoneNumber" ? "phonenumber_desc" : "phoneNumber";
                    ViewBag.EmailSortParm = sortOrder == "email" ? "email_desc" : "email";
                    ViewBag.AdresSortParm = sortOrder == "adres" ? "adres_desc" : "adres";
                    ViewBag.GemeenteSortParm = sortOrder == "gemeente" ? "gemeente_desc" : "gemeente";

                    var klantOpLeeftijdGeslachtEnSearch = from klants in klant
                                                  where klants.Naam.ToLower().Contains(zoeken.ToLower()) || klants.Voornaam.ToLower().Contains(zoeken.ToLower())
                                                        || (klants.Naam + " " + klants.Voornaam).ToLower().Contains(zoeken.ToLower()) || (klants.Voornaam + " " + klants.Naam).ToLower().Contains(zoeken.ToLower())
                                                        || klants.Adres.Gemeente.Plaatsnaam.ToLower().Contains(zoeken.ToLower()) || (klants.Adres.Gemeente.Plaatsnaam + " " + klants.Adres.Gemeente.Postcode).ToLower().Contains(zoeken.ToLower())
                                                        || (klants.Adres.Gemeente.Postcode + " " + klants.Adres.Gemeente.Plaatsnaam).ToLower().Contains(zoeken.ToLower())
                                                        || klants.Adres.Gemeente.Provincie.ToLower().Contains(zoeken.ToLower())
                                                        || klants.Adres.Gemeente.Postcode.ToLower().Contains(zoeken.ToLower())
                                                  select klants;
                    var klanten = klantOpLeeftijdGeslachtEnSearch.Where(a => a.Geboortedatum.Year <= minLeeftijd && a.Geboortedatum.Year >= maxLeeftijd && a.Geslacht.ID.ToString().Contains(geslacht));

                    switch (sortOrder)
                    {
                        case "name":
                            klanten = klanten.OrderBy(s => s.Naam).ToList();
                            break;
                        case "name_desc":
                            klanten = klanten.OrderByDescending(s => s.Naam).ToList();
                            break;
                        case "firstname":
                            klanten = klanten.OrderBy(s => s.Voornaam).ToList();
                            break;
                        case "firstName_desc":
                            klanten = klanten.OrderByDescending(s => s.Voornaam).ToList();
                            break;
                        case "phoneNumber":
                            klanten = klanten.OrderBy(s => s.Telefoon).ToList();
                            break;
                        case "phoneNumber_desc":
                            klanten = klanten.OrderByDescending(s => s.Telefoon).ToList();
                            break;
                        case "email":
                            klanten = klanten.OrderBy(s => s.Email).ToList();
                            break;
                        case "email_desc":
                            klanten = klanten.OrderByDescending(s => s.Email).ToList();
                            break;
                        case "adres":
                            klanten = klanten.OrderBy(s => s.Adres.Straat).ToList();
                            break;
                        case "adres_desc":
                            klanten = klanten.OrderByDescending(s => s.Adres.Straat).ToList();
                            break;
                        case "gemeente":
                            klanten = klanten.OrderBy(s => s.Adres.Gemeente.Plaatsnaam).ToList();
                            break;
                        case "gemeente_desc":
                            klanten = klanten.OrderByDescending(s => s.Adres.Gemeente.Plaatsnaam).ToList();
                            break;
                        default:
                            klanten = klanten.OrderBy(s => s.Naam).ToList();
                            break;
                    }
                    TempData["Klanten"] = klanten.ToList();
                    return View(klanten);
                }
                else if(geslacht == null && leeftijdVan != 0 && leeftijdTot != 100 && string.IsNullOrEmpty(zoeken))
                {
                    ViewBag.NameSortParm = sortOrder == "name_desc" ? "name" : "name_desc";
                    ViewBag.FirstNameSortParm = sortOrder == "firstname" ? "firstName_desc" : "firstname";
                    ViewBag.PhoneSortParm = sortOrder == "phoneNumber" ? "phonenumber_desc" : "phoneNumber";
                    ViewBag.EmailSortParm = sortOrder == "email" ? "email_desc" : "email";
                    ViewBag.AdresSortParm = sortOrder == "adres" ? "adres_desc" : "adres";
                    ViewBag.GemeenteSortParm = sortOrder == "gemeente" ? "gemeente_desc" : "gemeente";

                    var klantOpLeeftijd = from klants in klant
                                          where klants.Geboortedatum.Year <= minLeeftijd && klants.Geboortedatum.Year >= maxLeeftijd
                                          select klants;
                    var klanten = klantOpLeeftijd;

                    switch (sortOrder)
                    {
                        case "name":
                            klanten = klanten.OrderBy(s => s.Naam).ToList();
                            break;
                        case "name_desc":
                            klanten = klanten.OrderByDescending(s => s.Naam).ToList();
                            break;
                        case "firstname":
                            klanten = klanten.OrderBy(s => s.Voornaam).ToList();
                            break;
                        case "firstName_desc":
                            klanten = klanten.OrderByDescending(s => s.Voornaam).ToList();
                            break;
                        case "phoneNumber":
                            klanten = klanten.OrderBy(s => s.Telefoon).ToList();
                            break;
                        case "phoneNumber_desc":
                            klanten = klanten.OrderByDescending(s => s.Telefoon).ToList();
                            break;
                        case "email":
                            klanten = klanten.OrderBy(s => s.Email).ToList();
                            break;
                        case "email_desc":
                            klanten = klanten.OrderByDescending(s => s.Email).ToList();
                            break;
                        case "adres":
                            klanten = klanten.OrderBy(s => s.Adres.Straat).ToList();
                            break;
                        case "adres_desc":
                            klanten = klanten.OrderByDescending(s => s.Adres.Straat).ToList();
                            break;
                        case "gemeente":
                            klanten = klanten.OrderBy(s => s.Adres.Gemeente.Plaatsnaam).ToList();
                            break;
                        case "gemeente_desc":
                            klanten = klanten.OrderByDescending(s => s.Adres.Gemeente.Plaatsnaam).ToList();
                            break;
                        default:
                            klanten = klanten.OrderBy(s => s.Naam).ToList();
                            break;
                    }
                    TempData["Klanten"] = klanten.ToList();
                    return View(klanten);
                }
                else if (geslacht != null && leeftijdVan != 0 && leeftijdTot != 100 && string.IsNullOrEmpty(zoeken))
                {
                    ViewBag.NameSortParm = sortOrder == "name_desc" ? "name" : "name_desc";
                    ViewBag.FirstNameSortParm = sortOrder == "firstname" ? "firstName_desc" : "firstname";
                    ViewBag.PhoneSortParm = sortOrder == "phoneNumber" ? "phonenumber_desc" : "phoneNumber";
                    ViewBag.EmailSortParm = sortOrder == "email" ? "email_desc" : "email";
                    ViewBag.AdresSortParm = sortOrder == "adres" ? "adres_desc" : "adres";
                    ViewBag.GemeenteSortParm = sortOrder == "gemeente" ? "gemeente_desc" : "gemeente";

                    var klantOpLeeftijdEnGeslacht = from klants in klant
                                                    where klants.Geboortedatum.Year <= minLeeftijd && klants.Geboortedatum.Year >= maxLeeftijd && klants.Geslacht.ID.ToString().Contains(geslacht)
                                                    select klants;
                    var klanten = klantOpLeeftijdEnGeslacht;

                    switch (sortOrder)
                    {
                        case "name":
                            klanten = klanten.OrderBy(s => s.Naam).ToList();
                            break;
                        case "name_desc":
                            klanten = klanten.OrderByDescending(s => s.Naam).ToList();
                            break;
                        case "firstname":
                            klanten = klanten.OrderBy(s => s.Voornaam).ToList();
                            break;
                        case "firstName_desc":
                            klanten = klanten.OrderByDescending(s => s.Voornaam).ToList();
                            break;
                        case "phoneNumber":
                            klanten = klanten.OrderBy(s => s.Telefoon).ToList();
                            break;
                        case "phoneNumber_desc":
                            klanten = klanten.OrderByDescending(s => s.Telefoon).ToList();
                            break;
                        case "email":
                            klanten = klanten.OrderBy(s => s.Email).ToList();
                            break;
                        case "email_desc":
                            klanten = klanten.OrderByDescending(s => s.Email).ToList();
                            break;
                        case "adres":
                            klanten = klanten.OrderBy(s => s.Adres.Straat).ToList();
                            break;
                        case "adres_desc":
                            klanten = klanten.OrderByDescending(s => s.Adres.Straat).ToList();
                            break;
                        case "gemeente":
                            klanten = klanten.OrderBy(s => s.Adres.Gemeente.Plaatsnaam).ToList();
                            break;
                        case "gemeente_desc":
                            klanten = klanten.OrderByDescending(s => s.Adres.Gemeente.Plaatsnaam).ToList();
                            break;
                        default:
                            klanten = klanten.OrderBy(s => s.Naam).ToList();
                            break;
                    }
                    TempData["Klanten"] = klanten.ToList();
                    return View(klanten);
                }
                else
                {
                    return View(klant);
                }
                
            }
            else if (Request.Form["clear"] != null)
            {
                TempData["Search"] = null;
                return View(ks.GetKlanten());
            }
            else
            {
                ViewBag.sortGeslacht = sortGeslacht;

                ViewBag.NameSortParm = sortOrder == "name_desc" ? "name" : "name_desc";
                ViewBag.FirstNameSortParm = sortOrder == "firstname" ? "firstName_desc" : "firstname";
                ViewBag.PhoneSortParm = sortOrder == "phoneNumber" ? "phonenumber_desc" : "phoneNumber";
                ViewBag.EmailSortParm = sortOrder == "email" ? "email_desc" : "email";
                ViewBag.AdresSortParm = sortOrder == "adres" ? "adres_desc" : "adres";
                ViewBag.GemeenteSortParm = sortOrder == "gemeente" ? "gemeente_desc" : "gemeente";

                List<Klant> klanten = (List<Klant>)TempData["Klanten"];

                switch (sortOrder)
                {
                    case "name":
                        klanten = klanten.OrderBy(s => s.Naam).ToList();
                        break;
                    case "name_desc":
                        klanten = klanten.OrderByDescending(s => s.Naam).ToList();
                        break;
                    case "firstname":
                        klanten = klanten.OrderBy(s => s.Voornaam).ToList();
                        break;
                    case "firstName_desc":
                        klanten = klanten.OrderByDescending(s => s.Voornaam).ToList();
                        break;
                    case "phoneNumber":
                        klanten = klanten.OrderBy(s => s.Telefoon).ToList();
                        break;
                    case "phoneNumber_desc":
                        klanten = klanten.OrderByDescending(s => s.Telefoon).ToList();
                        break;
                    case "email":
                        klanten = klanten.OrderBy(s => s.Email).ToList();
                        break;
                    case "email_desc":
                        klanten = klanten.OrderByDescending(s => s.Email).ToList();
                        break;
                    case "adres":
                        klanten = klanten.OrderBy(s => s.Adres.Straat).ToList();
                        break;
                    case "adres_desc":
                        klanten = klanten.OrderByDescending(s => s.Adres.Straat).ToList();
                        break;
                    case "gemeente":
                        klanten = klanten.OrderBy(s => s.Adres.Gemeente.Plaatsnaam).ToList();
                        break;
                    case "gemeente_desc":
                        klanten = klanten.OrderByDescending(s => s.Adres.Gemeente.Plaatsnaam).ToList();
                        break;
                    default:
                        klanten = klanten.OrderBy(s => s.Naam).ToList();
                        break;
                }
                TempData["Klanten"] = klanten;
                return View(klanten);
                
            }
        }
        public ActionResult Details(int? id)
        {
            if (id == null) { return RedirectToAction("Index"); }
            int id2 = (int)id;
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
        [HttpGet]
        public ActionResult New()
        {
            KlantViewModel model;
            if (Session["EditKlant"] == null)
            {
            model = new KlantViewModel();
            model.Geslachten = ks.GetGeslachten();
            model.Mutualiteiten = ks.GetMutualiteiten();
            model.Werksituaties = ks.GetWerkSituaties();
            model.Karaktertreken = ks.GetKaraktertreken();
            model.Voedingspatronen = ks.GetVoedingspatronen();
            model.Relaties = ks.GetRelaties();
            model.Klanten = ks.GetKlanten();
            model.KlantRelatie = new List<KlantRelatie>();
            model.Geboortedatum = new DateTime();
            model.Karaktertrek = new List<Karaktertrek>();
            model.KlantRelaties = new List<KlantRelatie>();
            model.Geslacht = new Geslacht();
            model.Voedingspatroon = new Voedingspatroon();
            model.MedischeFiche = new MedischeFiche();
            model.PersoonlijkeFiche = new PersoonlijkeFiche();
            model.MedischeFiche.Mutualiteit = new Mutualiteit();
            model.Vandaag = DateTime.Now.ToString("dd-MM-yyyy");
            model.SelectedKlantRelatie = new CRMSanto.Models.KlantRelatie() { Relatie = new CRMSanto.Models.Klant() { ID = 0, Naam = "Empty", Voornaam = "Empty" }, RelatieType = new Relatie() { Naam = "Empty" } };
            }
            else
            {
                Klant k = ks.GetKlantByID(Convert.ToInt32(Session["EditKlant"]));
                model = new KlantViewModel()
                {
                    Adres = k.Adres,
                    Email = k.Email,
                    Foto = k.Foto,
                    Geboortedatum = k.Geboortedatum,
                    Geslacht = k.Geslacht,
                    ID = k.ID,
                    Karaktertrek = k.Karaktertrek,
                    KlantRelatie = k.KlantRelatie,
                    MedischeFiche = k.MedischeFiche,
                    Naam = k.Naam,
                    PersoonlijkeFiche = k.PersoonlijkeFiche,
                    Telefoon = k.Telefoon,
                    Voornaam = k.Voornaam,
                    Voedingspatroon = k.Voedingspatroon,
                };
                model.Geslachten = ks.GetGeslachten();
                model.Mutualiteiten = ks.GetMutualiteiten();
                model.Werksituaties = ks.GetWerkSituaties();
                model.Karaktertreken = ks.GetKaraktertreken();
                model.Voedingspatronen = ks.GetVoedingspatronen();
                model.Relaties = ks.GetRelaties();
                model.Klanten = ks.GetKlanten();
                model.SelectedKlantRelatie = new CRMSanto.Models.KlantRelatie() { Relatie = new CRMSanto.Models.Klant() { ID = 0, Naam = "Empty", Voornaam = "Empty" }, RelatieType = new Relatie() { Naam = "Empty" } };
                if (model.KlantRelatie == null)
                    model.KlantRelatie = new List<KlantRelatie>();
            }
            
            return View(model);
        }
        public ActionResult Edit(int ID)
        {
            Session["EditKlant"] = ID;
            return RedirectToAction("New");
        }
        [HttpPost]
        public ActionResult New(KlantViewModel klant)
        {
            
                    if (Request.Form["addkar"] != null)
                    {
                        try
                        {
                            //Add Karaktertrek
                            KlantViewModel model = klant;
                            if (model.Upload != null) //Only set photo when upload not null
                            {
                                Session["PhotoUpload"] = model.Upload;
                            }

                            model.Karaktertrek = (List<Karaktertrek>)TempData["Karaktertreken"]; // Get Karaktertrekken From last postback
                            if (model.Karaktertrek == null) // If no karakters where added before , this will be null (safety agains nullrefexeption)
                            {
                                model.Karaktertrek = new List<Karaktertrek>();
                            }
                            Karaktertrek newKaraktertrek = ks.GetKaraktertrekByID(model.SelectedKaracter.ID);
                            bool containsItem = model.Karaktertrek.Any(item => item.ID == newKaraktertrek.ID);
                            if (containsItem != true) { model.Karaktertrek.Add(newKaraktertrek); }
                            TempData["Karaktertreken"] = model.Karaktertrek; // Save values for next rebound

                            ////// Fill Lists
                            model.Geslachten = ks.GetGeslachten();
                            model.Mutualiteiten = ks.GetMutualiteiten();
                            model.Werksituaties = ks.GetWerkSituaties();
                            model.Karaktertreken = ks.GetKaraktertreken();
                            model.Voedingspatronen = ks.GetVoedingspatronen();
                            model.Relaties = ks.GetRelaties();
                            model.Klanten = ks.GetKlanten();
                            return View(model);
                        }
                        catch (Exception ex)
                        {
                            return View(klant);//If expetion happens , return filled in data
                        }

                    }
                    else
                    {
                        if(Request.Form["addrelatie"]!=null)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            if (ModelState.IsValid)
                            {
                                if(klant.ID==0)
                                {
                                    Klant tempKlant = new Klant();
                                    if (TempData["NewKlantM"] == null)//If Klant doesn't exist from prev postback (Happens when multiple gemeentes are possible
                                    {
                                        if (klant.Geslacht != null)// If Geslacht is filled in , get Object from database
                                        {
                                            if (klant.Geslacht.ID != 0)
                                                klant.Geslacht = ks.GetGeslachtByID(klant.Geslacht.ID);
                                        }
                                        if (klant.MedischeFiche != null)
                                        {
                                            if (klant.MedischeFiche.Mutualiteit != null)//If mutualiteit is filled in , get object from database
                                                klant.MedischeFiche.Mutualiteit = ks.GetMutualiteitByID(klant.MedischeFiche.Mutualiteit.ID);
                                        }

                                        klant.Foto = Guid.NewGuid().ToString();

                                        if (klant.Upload == null)//If the upload would magicaly become null , we grab it from the database.
                                        {
                                            if (Session["PhotoUpload"] != null)
                                            {
                                                ks.SaveImage((HttpPostedFileBase)Session["PhotoUpload"], klant.Foto);//Save photo from last postback
                                            }
                                        }
                                        else
                                        {
                                            ks.SaveImage(klant.Upload, klant.Foto);//Save photo from current post
                                        }

                                        //Set a temp klant object with data from ViewModel and Tempdata
                                        tempKlant = new Klant() { Voornaam = klant.Voornaam, Naam = klant.Naam, Geboortedatum = klant.Geboortedatum, Adres = klant.Adres, Email = klant.Email, Karaktertrek = (List<Karaktertrek>)TempData["Karaktertreken"], Telefoon = klant.Telefoon, Foto = klant.Foto, Geslacht = klant.Geslacht, ID = klant.ID, MedischeFiche = klant.MedischeFiche, PersoonlijkeFiche = klant.PersoonlijkeFiche, Voedingspatroon = klant.Voedingspatroon, KlantRelatie = klant.KlantRelatie };
                                        if (tempKlant.Karaktertrek == null)
                                            tempKlant.Karaktertrek = new List<Karaktertrek>();
                                        //datetime-sql only can go as low as January 1, 1753
                                        if (klant.Geboortedatum < (DateTime)SqlDateTime.MinValue)
                                            tempKlant.Geboortedatum = (DateTime)SqlDateTime.MinValue;

                                        //Get all possible gemeentes
                                        List<Gemeente> gemeentelist = new List<Gemeente>();
                                        if (tempKlant.Adres.Postcode == null)
                                        {
                                            tempKlant.Adres.Postcode = "0000";
                                        }
                                        if (tempKlant.Adres.Gemeente.Plaatsnaam == null)
                                        {
                                            gemeentelist = ks.GetGemeentesByPostCode(tempKlant.Adres.Postcode);//Get list of gemeentes by postcode
                                            if (gemeentelist.Count > 1)//If more then 1 gemeente possible
                                            {
                                                TempData["NewKlantM"] = klant;//Save klant data for postback
                                                KlantViewModel model = klant;//Prep View Model
                                                model.Gemeentes = gemeentelist;//Fill gemeentes so view knows that it has to show all possible gemeentes
                                                return View(model);//Return this for view to handle
                                            }
                                            else
                                            {

                                                tempKlant.Adres.Gemeente = gemeentelist.First();//Only 1 possible so no furder actions needed
                                            }

                                        }
                                    }
                                    else
                                    {
                                        tempKlant = (Klant)TempData["NewKlantM"];//Get data from before gemeente selection
                                        tempKlant.Adres.Gemeente = ks.GetGemeenteByID(klant.Adres.Gemeente.ID);//Get selected gemeente
                                    }

                                    ks.InsertKlant(tempKlant); // save klant
                                    ks.Mails();
                                    return RedirectToAction("Index");
                                }
                                else
                                {
                                    Klant tempKlant = new Klant();
                                    tempKlant = new Klant() { Voornaam = klant.Voornaam, Naam = klant.Naam, Geboortedatum = klant.Geboortedatum, Adres = klant.Adres, Email = klant.Email, Karaktertrek = (List<Karaktertrek>)TempData["Karaktertreken"], Telefoon = klant.Telefoon, Foto = klant.Foto, Geslacht = klant.Geslacht, ID = klant.ID, MedischeFiche = klant.MedischeFiche, PersoonlijkeFiche = klant.PersoonlijkeFiche, Voedingspatroon = klant.Voedingspatroon, KlantRelatie = klant.KlantRelatie };
                                    if (tempKlant.Karaktertrek == null)
                                        tempKlant.Karaktertrek = new List<Karaktertrek>();
                                    if (tempKlant.Voedingspatroon == null)
                                        tempKlant.Voedingspatroon = new Voedingspatroon();
                                    ks.UpdateKlant(tempKlant);
                                    return RedirectToAction("Index");
                                }
                                
                            }
                            else
                            {
                                klant.Karaktertrek = (List<Karaktertrek>)TempData["Karaktertreken"]; 
                                if (klant.Karaktertrek == null) 
                                {
                                    klant.Karaktertrek = new List<Karaktertrek>();
                                }
                                klant.Geslachten = ks.GetGeslachten();
                                klant.Mutualiteiten = ks.GetMutualiteiten();
                                klant.Werksituaties = ks.GetWerkSituaties();
                                klant.Karaktertreken = ks.GetKaraktertreken();
                                klant.Voedingspatronen = ks.GetVoedingspatronen();
                                klant.Relaties = ks.GetRelaties();
                                klant.Klanten = ks.GetKlanten();
                                return View(klant);
                            }
                        }
                        
                        
                    }
        }
        public ActionResult Photo()
        {
            if (Session["PhotoUpload"] != null)
            {
                HttpPostedFileBase file = (HttpPostedFileBase)Session["PhotoUpload"];
                var stream = file.InputStream;
                return new FileStreamResult(stream, file.ContentType);
            }
            else
            {
                Image photo = Image.FromStream(new MemoryStream(new WebClient().DownloadData(@"http://massagesanto.blob.core.windows.net/images/profile.jpg")));
                var stream = ToStream(photo, ImageFormat.Jpeg);
                return new FileStreamResult(stream, "image/jpeg");
            }
        }
        public Stream ToStream(Image image, ImageFormat formaw)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, formaw);
            stream.Position = 0;
            return stream;
        }
    }
}