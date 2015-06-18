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
        //[Authorize]
        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        //[Authorize]
        public ActionResult New(SoortAfspraak m)
        {
            if (ModelState.IsValid)
            {
                afs.InsertMassage(m);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        //[Authorize]
        public ActionResult Edit(int? id)
        {
            if(id==null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(afs.GetMassageByID(id.Value));
            }            
        }
        [HttpPost]
        //[Authorize]
        public ActionResult Edit(SoortAfspraak m)
        {
            if (ModelState.IsValid)
            {
                afs.UpdateMassage(m);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }        
        [HttpGet]
        //[Authorize]
        public ActionResult New2()
        {
            return View();
        }
        [HttpPost]
        //[Authorize]
        public ActionResult New2(Arrangement a)
        {
            if (ModelState.IsValid)
            {
                afs.InsertArrangement(a);
                return RedirectToAction("Index");
            }

            else
            {
                return View();
            }
        }
        [HttpGet]
        //[Authorize]
        public ActionResult Edit2(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(afs.GetArrangementByID(id.Value));
            }
        }
        [HttpPost]
        //[Authorize]
        public ActionResult Edit2(Arrangement a)
        {
            if(ModelState.IsValid)
            {
                afs.UpdateArrangement(a);
                return RedirectToAction("Index");
            }

            else
            {
                return View();
            }
            
        }
        [HttpGet]
        //[Authorize]
        public ActionResult New3()
        {
            return View();
        }
        [HttpPost]
        //[Authorize]
        public ActionResult New3(Extra e)
        {
            if (ModelState.IsValid)
            {
                afs.InsertExtra(e);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        //[Authorize]
        public ActionResult Edit3(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(afs.GetExtraByID(id.Value));
            }
        }
        [HttpPost]
        //[Authorize]
        public ActionResult Edit3(Extra e)
        {
            if (ModelState.IsValid)
            {
                afs.UpdateExtra(e);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
    }
}