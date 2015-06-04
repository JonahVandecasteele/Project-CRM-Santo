using CRMSanto.BusinessLayer.Repository;
using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMSanto.BusinessLayer.Services;

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

        //[HttpPost]
        //public ActionResult New(Klant k)
        //{
        //    ks.AddKlant(k);
        //    return View();
        //}
    }
}