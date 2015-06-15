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
        public ActionResult Index(string sortOrder)
        {
            KlantViewModel kv = new KlantViewModel();
            
            kv.Geslachten = ks.GetGeslachten();
            kv.Gemeentes = ks.GetGemeentes();
            //List<Klant> klanten = ks.GetKlanten();
            //return View(klanten);
            if (Request.Form["submit"] != null)
            {
                if (Request.Form["Search"] != "")
                {
                    ViewBag.Search = Request.Form["Search"];
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
                                        || klants.Adres.Gemeente.Plaatsnaam.ToLower().Contains(zoeken.ToLower()) || (klants.Adres.Gemeente.Plaatsnaam + " " + klants.Adres.Gemeente.Postcode).ToLower().Contains(zoeken.ToLower())
                                        || (klants.Adres.Gemeente.Postcode + " " + klants.Adres.Gemeente.Plaatsnaam).ToLower().Contains(zoeken.ToLower())
                                        || klants.Adres.Gemeente.Provincie.ToLower().Contains(zoeken.ToLower())
                                        || klants.Adres.Gemeente.Postcode.ToLower().Contains(zoeken.ToLower())
                                  select klants;

                    return View(klanten);
                }
                else { 
                    ViewBag.Search = null;
                    return View(ks.GetKlanten());
                }
            }
            else if (Request.Form["clear"] != null)
            {
                ViewBag.Search = null;
                return View(ks.GetKlanten());
            }
            else
            {
                ViewBag.NameSortParm = sortOrder == "name_desc" ? "name" : "name_desc";
                ViewBag.FirstNameSortParm = sortOrder == "firstname" ? "firstName_desc" : "firstname";
                ViewBag.PhoneSortParm = sortOrder == "phoneNumber" ? "phonenumber_desc" : "phoneNumber";
                ViewBag.EmailSortParm = sortOrder == "email" ? "email_desc" : "email";
                ViewBag.AdresSortParm = sortOrder == "adres" ? "adres_desc" : "adres";
                ViewBag.GemeenteSortParm = sortOrder == "gemeente" ? "gemeente_desc" : "gemeente";

                List<Klant> klanten = ks.GetKlanten();
                ViewBag.Search = null;
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
        public ActionResult New()
        {
            KlantViewModel model = new KlantViewModel();
            model.Geslachten = ks.GetGeslachten();
            model.Mutualiteiten = ks.GetMutualiteiten();
            model.Werksituaties = ks.GetWerkSituaties();
            model.Karaktertreken = ks.GetKaraktertreken();
            model.Vandaag = DateTime.Now.ToString("dd-MM-yyyy");
            return View(model);
        }
        [HttpPost]
        public ActionResult New(KlantViewModel klant)
        {
            if (Request.Form["Create"] != null)
            {
                Klant tempKlant = new Klant();
                if (TempData["NewKlantM"] == null)
                {
                    if (klant.Geslacht.ID != 0)
                        klant.Geslacht = ks.GetGeslachtByID(klant.Geslacht.ID);

                    if (klant.MedischeFiche.Mutualiteit.ID != 0)
                        klant.MedischeFiche.Mutualiteit = ks.GetMutualiteitByID(klant.MedischeFiche.Mutualiteit.ID);
                    HttpPostedFileBase photo = klant.Upload;
                    klant.Foto = Guid.NewGuid().ToString();
                    if (photo == null)
                    {
                        if (Session["PhotoUpload"] != null)
                        {
                            photo = (HttpPostedFileBase)Session["PhotoUpload"];
                            ks.SaveImage(photo, klant.Foto);
                        }
                    }
                    else
                    {
                        ks.SaveImage(photo, klant.Foto);
                    }

                    tempKlant = new Klant() { Voornaam = klant.Voornaam, Naam = klant.Naam, Adres = klant.Adres, Email = klant.Email, Karaktertrek = klant.Karaktertrek, Telefoon = klant.Telefoon, Foto = klant.Foto, Geslacht = klant.Geslacht, ID = klant.ID, MedischeFiche = klant.MedischeFiche, PersoonlijkeFiche = klant.PersoonlijkeFiche };
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
            else if (Request.Form["addkar"] != null)
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

        //[HttpPost]
        //public ActionResult New(Klant k)
        //{
        //    ks.AddKlant(k);
        //    return View();
        //}
    }
}