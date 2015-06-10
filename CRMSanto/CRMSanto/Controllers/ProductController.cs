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
            //if (Request.Form["zoeken"] != null)
            if(Request.Form["submit"] != null)
            {
                string zoeken = Request.Form["Search"];
                //return View(ps.GetProducten());
                List<Product> producten = ps.GetProducten().Where(x => x.Naam.ToLower().Contains(zoeken.ToLower())).ToList();
                return View(producten);
            }
            else
            {
                return View(ps.GetProducten());
            }
            
            
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
            HttpPostedFileBase photo = p.Upload;
            p.Foto = photo.FileName;
            ps.SaveImage(photo);
            ps.AddProduct(p);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult NewProductRegistration()
        {
            NieuweProductRegistratiePM pm = new NieuweProductRegistratiePM();

            var values = ks.GetKlanten().Select(u => new { ID = u.ID, Naam = u.Naam + " " + u.Voornaam });
            
            pm.Producten = new SelectList(ps.GetProducten(),"ID","Naam");
            pm.Klanten = new SelectList(values, "ID", "Naam");
            pm.Productregistratie = new Productregistratie();
            return View(pm);
        }

        [HttpPost]
        public ActionResult NewProductRegistration(NieuweProductRegistratiePM nprpm)
        {
            nprpm.Klanten = new SelectList(ks.GetKlanten(), "ID", "Naam");
            nprpm.Producten = new SelectList(ps.GetProducten(), "ID", "Naam");

            Productregistratie pr = new Productregistratie();
            pr = nprpm.Productregistratie;
            pr.DatumTijdstip = DateTime.Now;
            pr.Klant = ks.GetKlantByID(nprpm.KlantID);
            pr.Product = ps.GetProductByID(nprpm.ProductID);

            ps.InsertProductregistration(pr);
            return RedirectToAction("Index");
        }

        //public ActionResult SearchForProduct(string product)
        //{
        //    ApplicationDbContext context = new ApplicationDbContext();

        //    var query = (from pr in context.Product select pr);

        //    if (!String.IsNullOrEmpty(product))
        //    {
        //        query = query.Where(q => q.Naam.Contains(product));
        //    }



        //}


    }
}