using CRMSanto.BusinessLayer.Services;
using CRMSanto.Models;
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
        public ProductController(IProductService ps)
        {
            this.ps = ps;
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
    }
}