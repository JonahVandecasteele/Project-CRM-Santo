using CRMSanto.BusinessLayer.Services;
using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMSanto.Controllers
{
    public class DienstController : Controller
    {
        private IAfspraakService afs;
        public DienstController(IAfspraakService afs)
        {
            this.afs = afs;         
        }
        // GET: Dienst
        public ActionResult Index()
        {
            return View(afs.GetMassages());
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(SoortAfspraak m)
        {
            afs.InsertMassage(m);
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            return View(afs.GetMassageByID(id.Value));
        }

        [HttpPost]
        public ActionResult Edit(SoortAfspraak m)
        {
            afs.UpdateMassage(m);
            return View();
        }
    }
}