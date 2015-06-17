using CRMSanto.BusinessLayer.Services;
using CRMSanto.Models;
using CRMSanto.Models.PresentationModels;
using CRMSanto.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Office365.OutlookServices;
using CRMSanto.Helpers;
using System.Threading.Tasks;
using model = CRMSanto.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace CRMSanto.Controllers
{
    public class AfspraakController : Controller
    {

        private IAfspraakService afs;
        private IKlantService ks;

        private CalendarOperations _calenderOperations = new CalendarOperations();
        private const int NumberOfHoursBefore = 240;
        private const int NumberOfHoursAfter = 240;

        private static bool _O365ServiceOperationFailed = false;

        public AfspraakController(IAfspraakService afs, IKlantService ks)
        {
            this.afs = afs;
            this.ks = ks;
        }


        [HttpGet]
        public ActionResult Index()
        {
            AfspraakPM apm = new AfspraakPM();
            apm.Afspraken = afs.GetLopendeAfspraken();
            //List<Klant> jarigen = ks.GetJarigen();
            //foreach(Klant k in jarigen)
            //{
            //    ks.SendMail(k);
            //}
            ViewBag.Vanaf = DateTime.Today.ToString("dd-MM-yyyy");
            return View(apm);
        }

        [HttpPost]
        public ActionResult Index(DateTime vanaf)
        {
            AfspraakPM apm = new AfspraakPM();
            ViewBag.Vanaf = vanaf.ToString("dd-MM-yyyy");
            apm.Afspraken = afs.VanafAfspraken(vanaf);
            return View(apm);
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id.HasValue)
            {
                NieuweAfspraakPM pm = new NieuweAfspraakPM();
                pm.Klanten = new SelectList(ks.GetKlanten().Select(u => new { ID = u.ID, Naam = u.Naam + " " + u.Voornaam }), "ID", "Naam");
                pm.Masseurs = new SelectList(afs.GetMasseurs().Select(m => new { ID = m.ID, Naam = m.Naam }), "ID", "Naam");
                pm.SoortAfspraken = new SelectList(afs.GetMassages().Select(ms => new { ID = ms.ID, Naam = ms.Naam }), "ID", "Naam");
                pm.Arrangementen = new SelectList(afs.GetArrangementen().Select(ar => new { ID = ar.ID, Naam = ar.Naam }), "ID", "Naam");
                pm.Extras = new SelectList(afs.GetExtras().Select(e => new { ID = e.ID, Naam = e.Naam }), "ID", "Naam");
                Afspraak a = afs.GetAfspraakByID(id.Value);
                pm.Afspraak = a;
                pm.Afspraak.ID = a.ID;
                pm.Datum = a.DatumTijdstip.Date;
                ViewBag.Datum = a.DatumTijdstip;
                pm.Tijdstip = Convert.ToDateTime(a.DatumTijdstip.ToString("HH:mm"));
                pm.Afspraak.DatumTijdstip = a.DatumTijdstip;
                pm.Afspraak.Verplaatsing = a.Verplaatsing;
                a.Notitie = pm.Afspraak.Notitie;
                a.Duur = pm.Afspraak.Duur;
                a.SoloDuo = pm.Afspraak.SoloDuo;
                a.SoortAfspraak = pm.Afspraak.SoortAfspraak;
                a.Arrangement = pm.Afspraak.Arrangement;
                a.AantalPersonen = pm.Afspraak.AantalPersonen;
                a.Klant = pm.Afspraak.Klant;
                a.Archief = pm.Afspraak.Archief;
                a.Geannuleerd = pm.Afspraak.Geannuleerd;
                a.Masseur = pm.Afspraak.Masseur;


                return View(pm);
            }

            else
            {
                return RedirectToAction("Index");
            }
        }      
        [HttpPost]
        public ActionResult Edit(NieuweAfspraakPM a)
        {
            if (a.Afspraak.Klant.ID != 0)
                {
                    a.Afspraak.Klant = ks.GetKlantByID(a.Afspraak.Klant.ID);
                }
                a.Afspraak.Masseur = afs.GetMasseurByID(a.Afspraak.Masseur.ID);
                a.Afspraak.SoortAfspraak = afs.GetMassageByID(a.Afspraak.SoortAfspraak.ID);
                a.Afspraak.Arrangement = afs.GetArrangementByID(a.Afspraak.Arrangement.ID);
                a.Afspraak.Extra = afs.GetExtraByID(a.Afspraak.Extra.ID);
                if (a.Afspraak.Arrangement != null && a.Afspraak.SoortAfspraak != null)
                {
                    a.Afspraak.Duur = a.Afspraak.SoortAfspraak.Duur + a.Afspraak.Arrangement.Duur;
                }
                else if (a.Afspraak.SoortAfspraak != null)
                {
                    a.Afspraak.Duur = a.Afspraak.SoortAfspraak.Duur;
                }
                else if(a.Afspraak.Arrangement != null)

                {
                    a.Afspraak.Duur = a.Afspraak.Arrangement.Duur;
                }

                else
                {
                    a.Afspraak.Duur = 60;
                }
                
                
                if (a.Datum.Date == DateTime.MinValue)
                    a.Afspraak.DatumTijdstip = a.Tijdstip;
                else
                    a.Afspraak.DatumTijdstip = a.Datum.Date + a.Tijdstip.TimeOfDay;

                    
                    if (a.Afspraak.Geannuleerd == false)
                    {
                        afs.UpdateAfspraak(a.Afspraak);
                        return RedirectToAction("Index");
                    }
                        
                    else
                    {
                        a.Afspraak.Geannuleerd = false;
                        //ViewBag.Error = "Afspraak reeds gemaakt op dit tijdstip";
                        return RedirectToAction("Index");
                        //return View(a);
                    }                   
                 return View(a);
        }
        [HttpGet]
        public ActionResult New()
        {
           
            NieuweAfspraakPM pm = new NieuweAfspraakPM();
            pm.Klanten = new SelectList(ks.GetKlanten().Select(u => new { ID = u.ID, Naam = u.Naam + " " + u.Voornaam }), "ID", "Naam");
            pm.Masseurs = new SelectList(afs.GetMasseurs().Select(m => new { ID = m.ID, Naam = m.Naam }), "ID", "Naam");
            pm.SoortAfspraken = new SelectList(afs.GetMassages().Select(ms => new {ID=ms.ID, Naam = ms.Naam}), "ID","Naam");
            pm.Arrangementen = new SelectList(afs.GetArrangementen().Select(ar => new { ID = ar.ID, Naam = ar.Naam }), "ID", "Naam");
            pm.Extras = new SelectList(afs.GetExtras().Select(e => new { ID = e.ID, Naam = e.Naam }), "ID", "Naam");
            pm.Afspraak = new Afspraak();
            pm.Afspraak.DatumTijdstip = DateTime.Now;
            pm.Datum = DateTime.Now;
            pm.Tijdstip = DateTime.Now;
            return View(pm);
        }
        [HttpPost]
        public async Task<ActionResult> New(NieuweAfspraakPM a)
        {
                if (a.Afspraak.Klant.ID != 0)
                {
                    a.Afspraak.Klant = ks.GetKlantByID(a.Afspraak.Klant.ID);
                }
                a.Afspraak.Masseur = afs.GetMasseurByID(a.Afspraak.Masseur.ID);
                a.Afspraak.SoortAfspraak = afs.GetMassageByID(a.Afspraak.SoortAfspraak.ID);
                a.Afspraak.Arrangement = afs.GetArrangementByID(a.Afspraak.Arrangement.ID);
                a.Afspraak.Extra = afs.GetExtraByID(a.Afspraak.Extra.ID);
                if (a.Afspraak.Arrangement != null && a.Afspraak.SoortAfspraak != null)
                {
                    a.Afspraak.Duur = a.Afspraak.SoortAfspraak.Duur + a.Afspraak.Arrangement.Duur;
                }
                else if (a.Afspraak.SoortAfspraak != null)
                {
                    a.Afspraak.Duur = a.Afspraak.SoortAfspraak.Duur;
                }
                else if(a.Afspraak.Arrangement != null)

                {
                    a.Afspraak.Duur = a.Afspraak.Arrangement.Duur;
                }

                else
                {
                    a.Afspraak.Duur = 60;
                }
                
                a.Afspraak.DatumTijdstip = a.Datum.Date + a.Tijdstip.TimeOfDay;
                if (a.Afspraak.DatumTijdstip == DateTime.MinValue)
                    a.Afspraak.DatumTijdstip = (DateTime)SqlDateTime.MinValue;

                _O365ServiceOperationFailed = false;
                String newEventID = "";
                try
                {
                    newEventID = await _calenderOperations.AddCalendarEventAsync(a.Afspraak.Klant.Adres.ToString(), a.Afspraak.Klant.ToString(), a.Afspraak.Klant.Naam + " " + a.Afspraak.Klant.Voornaam, a.Afspraak.SoortAfspraak.Naam + " - " + a.Afspraak.Masseur.Naam, DateTimeOffset.Parse(a.Afspraak.DatumTijdstip.ToString()), DateTimeOffset.Parse(a.Afspraak.DatumTijdstip.ToString()));
                }
                catch (Exception)
                {
                    _O365ServiceOperationFailed = true;
                }
                if (_O365ServiceOperationFailed)
                {
                    afs.AddAfspraak(a.Afspraak);
                    if (a.Afspraak.Geannuleerd == false)
                    {
                        return RedirectToAction("Index");
                    }
                        
                    else
                    {
                        a.Afspraak.Geannuleerd = false;
                        //ViewBag.Error = "Afspraak reeds gemaakt op dit tijdstip";
                        return RedirectToAction("New");
                        //return View(a);
                    }
                    }
                   
                    
                 return View(a);
        
            /*if (Request.Form["New"] != null)
            {*/
                
         //   }
            //NieuweAfspraakPM pm = (NieuweAfspraakPM)a;
            //pm.Klanten = new SelectList(ks.GetKlanten().Select(u => new { ID = u.ID, Naam = u.Naam + " " + u.Voornaam }), "ID", "Naam");
            //return View(pm);
        }

        [HttpGet]
        public ActionResult Annuleer(int? id)
        {
            if(id.HasValue)
            {
                NieuweAfspraakPM pm = new NieuweAfspraakPM();
                pm.Klanten = new SelectList(ks.GetKlanten().Select(u => new { ID = u.ID, Naam = u.Naam + " " + u.Voornaam }), "ID", "Naam");
                pm.Masseurs = new SelectList(afs.GetMasseurs().Select(m => new { ID = m.ID, Naam = m.Naam }), "ID", "Naam");
                pm.SoortAfspraken = new SelectList(afs.GetMassages().Select(ms => new { ID = ms.ID, Naam = ms.Naam }), "ID", "Naam");
                pm.Arrangementen = new SelectList(afs.GetArrangementen().Select(ar => new { ID = ar.ID, Naam = ar.Naam }), "ID", "Naam");
                pm.Extras = new SelectList(afs.GetExtras().Select(e => new { ID = e.ID, Naam = e.Naam }), "ID", "Naam");
                Afspraak a = afs.GetAfspraakByID(id.Value);
                pm.Afspraak = a;
                pm.Afspraak.ID = a.ID;
                pm.Datum = a.DatumTijdstip.Date;
                ViewBag.Datum = a.DatumTijdstip;
                pm.Tijdstip = Convert.ToDateTime(a.DatumTijdstip.ToString("HH:mm"));
                pm.Afspraak.DatumTijdstip = a.DatumTijdstip;
                pm.Afspraak.Verplaatsing = a.Verplaatsing;
                a.Notitie = pm.Afspraak.Notitie;
                a.Duur = pm.Afspraak.Duur;
                a.SoloDuo = pm.Afspraak.SoloDuo;
                a.SoortAfspraak = pm.Afspraak.SoortAfspraak;
                a.Arrangement = pm.Afspraak.Arrangement;
                a.AantalPersonen = pm.Afspraak.AantalPersonen;
                a.Klant = pm.Afspraak.Klant;
                a.Archief = pm.Afspraak.Archief;
                a.Geannuleerd = pm.Afspraak.Geannuleerd;
                a.Masseur = pm.Afspraak.Masseur;
                return View(pm);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Annuleer(NieuweAfspraakPM a)
        {
            if (a.Afspraak.Klant.ID != 0)
            {
                a.Afspraak.Klant = ks.GetKlantByID(a.Afspraak.Klant.ID);
            }
            a.Afspraak.Masseur = afs.GetMasseurByID(a.Afspraak.Masseur.ID);
            a.Afspraak.SoortAfspraak = afs.GetMassageByID(a.Afspraak.SoortAfspraak.ID);
            a.Afspraak.Arrangement = afs.GetArrangementByID(a.Afspraak.Arrangement.ID);
            a.Afspraak.Extra = afs.GetExtraByID(a.Afspraak.Extra.ID);

            if (a.Datum.Date == DateTime.MinValue)
                a.Afspraak.DatumTijdstip = a.Tijdstip;
            else
                a.Afspraak.DatumTijdstip = a.Datum.Date + a.Tijdstip.TimeOfDay;

            a.Afspraak.Geannuleerd = true;
            afs.UpdateAnnuleer(a.Afspraak);
            return RedirectToAction("Index");
        }
        //public async Task<ActionResult> Create(FormCollection collection)
        //{
        //    _O365ServiceOperationFailed = false;
        //    String newEventID = "";

        //    try
        //    {


        //        newEventID = await _calenderOperations.AddCalendarEventAsync(collection["Location"],
        //                                                                        collection["Body"],
        //                                                                        collection["Attendees"],
        //                                                                        collection["Subject"],
        //                                                                        DateTimeOffset.Parse(collection["StartDate"]),
        //                                                                        DateTimeOffset.Parse(collection["EndDate"]));
        //    }

        //    catch (Exception)
        //    {
        //        _O365ServiceOperationFailed = true;
        //    }

        //    return RedirectToAction("Index", new { newid = newEventID });
        //}


    }
}