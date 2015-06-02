﻿using CRMSanto.BusinessLayer.Services;
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

        public ActionResult Edit(int? id)
        {
            return View(ps.GetProductByID(id.Value));
        }
    }
}