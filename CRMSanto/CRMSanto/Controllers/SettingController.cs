using CRMSanto.BusinessLayer.Services;
using CRMSanto.Models;
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
        public SettingController(IKlantService ks)
        {
            this.ks = ks;
        }
        
        [HttpGet]
        public ActionResult NewKaraktertrek()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewKaraktertrek(Karaktertrek k)
        {
            ks.InsertKaraktertrek(k);
            return View();
        }
        [HttpGet]
        public ActionResult NewWerksituatie()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewWerksituatie(Werksituatie w)
        {
            ks.InsertWerkSituatie(w);
            return View();
        }
    }
}