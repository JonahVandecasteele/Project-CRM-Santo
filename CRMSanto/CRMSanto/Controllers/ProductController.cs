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
    public class ProductController : Controller
    {
        private IProductService ps;
        private IKlantService ks;
        public ProductController(IProductService ps,IKlantService ks)
        {
            this.ps = ps;
            this.ks = ks;
        }
        // GET: Product
        public ActionResult Index()
        {
            return View(ps.GetProducten());
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            return View(ps.GetProductByID(id.Value));
        }
        [HttpPost]
        public ActionResult Edit(Product p)
        {
            ps.EditProduct(p);
            return View();
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Product p)
        {
            ps.AddProduct(p);
            return View();
        }

        [HttpGet]
        public ActionResult NewProductRegistration()
        {
            NieuweProductRegistratiePM pm = new NieuweProductRegistratiePM();

            pm.Producten = new SelectList(ps.GetProducten(),"ID","Naam");
            pm.Klanten = new SelectList(ks.GetKlanten(), "ID", "Naam");
            pm.Productregistratie = new Productregistratie();
            return View(pm);
        }

        //[HttpPost]
        //public ActionResult NewProductRegistration(NieuweProductRegistratiePM nprpm)
        //{
        //    nprpm.Klanten = new SelectList(ks.GetKlanten(), "ID", "Naam");
        //    nprpm.Producten = new SelectList(ps.GetProducten(), "ID", "Naam");

        //    Productregistratie pr = new Productregistratie();
        //    pr = nprpm.Productregistratie;

        //    pr.DatumTijdstip = DateTime.Now;
        //}


    }
}