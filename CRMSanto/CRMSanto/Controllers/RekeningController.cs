using CRMSanto.BusinessLayer.Services;
using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMSanto.Controllers
{
    public class RekeningController : Controller
    {

        private IRekeningService irs;

        public RekeningController(IRekeningService irs)
        {
            this.irs = irs;
        }

        // GET: Rekening
        public ActionResult Index()
        {
            List<Rekening> rekeningen = new List<Rekening>();
            rekeningen = irs.GetRekeningen();
            return View(rekeningen);
        }
    }
}