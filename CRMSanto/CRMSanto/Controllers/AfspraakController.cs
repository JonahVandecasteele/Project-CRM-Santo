using CRMSanto.BusinessLayer.Services;
using CRMSanto.Models;
using CRMSanto.Models.PresentationModels;
using System;
using System.Collections.Generic;
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
            //var values = _userRepository.FindAllUsers().Select(u => new { ID = u.UserName, Name = u.FirstName + " " + u.LastName + " " + u.EmailAddress });
            //var userDropDown = new SelectList(values, "Id", "Name");
            NieuweAfspraakPM pm = new NieuweAfspraakPM();

            var values = ks.GetKlanten().Select(u => new { ID = u.ID, Naam = u.Naam + " " + u.Voornaam });
            pm.Klanten = new SelectList(values, "ID", "Naam");
            

            pm.Afspraak = new Afspraak();
           
            return View(pm);
        }

        //[HttpPost]
        //public ActionResult NieuweAfspraak(NieuweAfspraakPM napm)
        //{
        //    napm.Klanten = new SelectList(ks.GetKlanten(), "ID", "Naam");
        //    napm.Afspraak = new Afspraak();
        //    return View();
        //}


    }
}