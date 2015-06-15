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
        public ActionResult Index()
        {
            ViewBag.Massages = afs.GetMassages();
            ViewBag.Arrangementen = afs.GetArrangementen();
            ViewBag.Extras = afs.GetExtras();
            return View();
        }
        // GET: Dienst
        
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
        [HttpGet]
        public ActionResult New2()
        {
            return View();
        }
        [HttpPost]
        public ActionResult New2(Arrangement a)
        {
            afs.InsertArrangement(a);
            return View();
        }
        [HttpGet]
        public ActionResult Edit2(int? id)
        {
            return View(afs.GetArrangementByID(id.Value));
        }
        [HttpPost]
        public ActionResult Edit2(Arrangement a)
        {
            afs.UpdateArrangement(a);
            return View();
        }
        [HttpGet]
        public ActionResult New3()
        {
            return View();
        }
        [HttpPost]
        public ActionResult New3(Extra e)
        {
            afs.InsertExtra(e);
            return View();
        }
        [HttpGet]
        public ActionResult Edit3(int? id)
        {
            return View(afs.GetExtraByID(id.Value));
        }
        [HttpPost]
        public ActionResult Edit3(Extra e)
        {
            afs.UpdateExtra(e);
            return View();
        }
    }
}