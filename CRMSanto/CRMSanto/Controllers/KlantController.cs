using CRMSanto.BusinessLayer.Repository;
using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMSanto.Controllers
{
    public class KlantController : Controller
    {
        MutualiteitenRepository mr = new MutualiteitenRepository();
        // GET: Klant
        public ActionResult Index()
        {
            List<Mutualiteit> items = mr.GetMutualiteiten();

            return View(items);
        }
    }
}