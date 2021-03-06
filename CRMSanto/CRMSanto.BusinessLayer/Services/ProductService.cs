﻿using CRMSanto.BusinessLayer.Repository;
using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CRMSanto.BusinessLayer.Services
{
    public class ProductService : CRMSanto.BusinessLayer.Services.IProductService
    {
        private IProductenRepository repoProducten = null;
        private IGenericRepository<Product> repoProduct = null;
        public ProductService(IProductenRepository repoProducten,IGenericRepository<Product> repoProduct)
        {
            this.repoProducten = repoProducten;
            this.repoProduct = repoProduct;
        }
        public List<Product> GetProducten()
        {
            return repoProduct.All().ToList<Product>();
        }
        public List<Productregistratie> GetProductregistratiesByKlantenID(int id) 
        {
            return repoProducten.GetProductregistratiesByKlantenID(id);
        }
        public List<Productregistratie> GetProductregistratiesByProductenID(int id)
        {
            return repoProducten.GetProductregistratiesByProductenID(id);
        }
        public Product GetProductByID(int? id)
        {
            return repoProduct.GetByID(id.Value);
        }
        public void EditProduct(Product p)
        {
            repoProduct.Update(p);
            repoProduct.SaveChanges();
        }
        public void AddProduct(Product p)
        {
            repoProduct.Insert(p);
            repoProduct.SaveChanges();
        }
        public void SaveImage(HttpPostedFileBase p)
        {
            repoProducten.SaveImage(p);
        }
        public void DeleteProduct(Product p)
        {
            repoProduct.Delete(p);
            repoProduct.SaveChanges();
        }
        public List<Productregistratie> GetProductregistraties()
        {
            return repoProducten.All().ToList<Productregistratie>();
        }
        public Productregistratie InsertProductregistration(Productregistratie productregistratie)
        {
            Productregistratie result = repoProducten.Insert(productregistratie);
            repoProducten.SaveChanges();
            return result;
        }
    }
}
