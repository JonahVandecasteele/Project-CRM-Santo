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
        //[Authorize]
        public ActionResult Index(string sortOrder)
        {
            if (TempData["Producten"] == null)
            {
                TempData["Producten"] = ps.GetProducten();
            }
            TempData["Search"] = Request.Form["Search"];
            //if (Request.Form["zoeken"] != null)
            if(Request.Form["submit"] != null)
            {
                if (Request.Form["Search"] != "")
                {
                    string zoeken = Request.Form["Search"];
                    //return View(ps.GetProducten());
                    List<Product> producten = ps.GetProducten().Where(x => x.Naam.ToLower().Contains(zoeken.ToLower())).ToList();
                    TempData["Producten"] = producten.ToList();
                    return View(producten);
                }
                else
                {
                    return View(ps.GetProducten().OrderBy(n => n.Naam));
                }
            }
            else if (Request.Form["clear"] != null)
            {
                TempData["Search"] = null;
                return View(ps.GetProducten().OrderBy(x => x.Naam));
            }
            else
            {
                ViewBag.NameSortParm = sortOrder == "name_desc" ? "name" : "name_desc";
                ViewBag.InhoudSortParm = sortOrder == "inhoud" ? "inhoud_desc" : "inhoud";
                ViewBag.APSortParm = sortOrder == "aankoopprijs" ? "aankoopprijs_desc" : "aankoopprijs";
                ViewBag.VPSortParm = sortOrder == "verkoopprijs" ? "verkoopprijs_desc" : "verkoopprijs";

                List<Product> producten = (List<Product>)TempData["Producten"];
                //ViewBag.Search = null;
                switch (sortOrder)
                {
                    case "name":
                        producten = producten.OrderBy(s => s.Naam).ToList();
                        break;
                    case "name_desc":
                        producten = producten.OrderByDescending(s => s.Naam).ToList();
                        break;
                    case "inhoud":
                        producten = producten.OrderBy(s => s.Inhoud).ToList();
                        break;
                    case "inhoud_desc":
                        producten = producten.OrderByDescending(s => s.Inhoud).ToList();
                        break;
                    case "aankoopprijs":
                        producten = producten.OrderBy(s => s.AankoopPrijs).ToList();
                        break;
                    case "aankoopprijs_desc":
                        producten = producten.OrderByDescending(s => s.AankoopPrijs).ToList();
                        break;
                    case "verkoopprijs":
                        producten = producten.OrderBy(s => s.VerkoopPrijs).ToList();
                        break;
                    case "verkoopprijs_desc":
                        producten = producten.OrderByDescending(s => s.VerkoopPrijs).ToList();
                        break;
                    
                    default:
                        producten = producten.OrderBy(s => s.Naam).ToList();
                        break;

                }
                TempData["Producten"] = producten;
                return View(producten);
            }
            
            
        }
        [HttpGet]
        //[Authorize]
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                return View(ps.GetProductByID(id.Value));
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        //[Authorize]
        public ActionResult Edit(Product p)
        {
            ps.EditProduct(p);
            return RedirectToAction("Index");
        }

        [HttpGet]
        //[Authorize]
        public ActionResult New()
        {
            return View();
        }


        [HttpPost]
        //[Authorize]
        public ActionResult New(Product p)
        {
            ps.AddProduct(p);
            return RedirectToAction("Index");
        }

        [HttpGet]
        //[Authorize]
        public ActionResult NewProductRegistration(int? id)
        {
            if (id.HasValue)
            {
                NieuweProductRegistratiePM pm = new NieuweProductRegistratiePM();

                var values = ks.GetKlanten().Select(u => new { ID = u.ID, Naam = u.Naam + " " + u.Voornaam });

                pm.Product = ps.GetProductByID(id.Value);
                pm.Klanten = new SelectList(values, "ID", "Naam");
                pm.Productregistratie = new Productregistratie();
                pm.Winkelmand = new Winkelmand();
                return View(pm);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        //[Authorize]
        public ActionResult NewProductRegistration(NieuweProductRegistratiePM nprpm)
        {
            nprpm.Klanten = new SelectList(ks.GetKlanten(), "ID", "Naam");

            Productregistratie pr = new Productregistratie();
            pr = nprpm.Productregistratie;
            pr.Winkelmand = new Winkelmand();
            pr.Winkelmand.DatumTijdstip = DateTime.Now;
            pr.Winkelmand.Klant = ks.GetKlantByID(nprpm.KlantID);
            pr.Product = ps.GetProductByID(nprpm.Product.ID);

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