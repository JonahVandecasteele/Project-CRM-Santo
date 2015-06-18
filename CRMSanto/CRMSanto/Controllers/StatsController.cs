using CRMSanto.BusinessLayer.Services;
using CRMSanto.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMSanto.Controllers
{
    public class StatsController : Controller
    {
        private IKlantService ks;
        private IProductService ps;
        private IAfspraakService afs;

        public StatsController(IKlantService ks, IProductService ps, IAfspraakService afs)
        {
            this.ks = ks;
            this.ps = ps;
            this.afs = afs;
        }

        // GET: Stats
        //[Authorize]
        public ActionResult Index()
        {
            StatsPM stats = new StatsPM();
            stats.Klanten = ks.GetKlanten();
            return View(stats);
        }
    }
}