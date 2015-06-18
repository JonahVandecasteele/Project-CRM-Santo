﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CRMSanto.Models;
using System.Web;

namespace CRMSanto.BusinessLayer.Repository
{
    public class ProductenRepository:GenericRepository<Productregistratie>, CRMSanto.BusinessLayer.Repository.IProductenRepository
    {
        public ProductenRepository(ApplicationDbContext context) : base(context)
        {

        }
        public override IEnumerable<Productregistratie> All()
        {
            var query = (from pr in context.Productregistratie.Include(w=>w.Winkelmand.Klant).Include(p=>p.Product) select pr);
            return query.ToList<Productregistratie>();
        }

        public List<Productregistratie> GetProductregistratiesByKlantenID(int id)
        {
            var query = (from pr in context.Productregistratie.Include(w => w.Winkelmand.Klant).Include(p => p.Product) 
                         where pr.Winkelmand.Klant.ID == id
                         select pr);
            return query.ToList<Productregistratie>();
        }

        public List<Productregistratie> GetProductregistratiesByProductenID(int id)
        {
            var query = (from pr in context.Productregistratie.Include(p => p.Product)
                         where pr.Product.ID == id
                         select pr);

            return query.ToList<Productregistratie>();
        }

        public override Productregistratie Insert(Productregistratie entity)
        {
            context.Klant.Attach(entity.Winkelmand.Klant);
            context.Product.Attach(entity.Product);

            Productregistratie pr = context.Productregistratie.Add(entity);
            return pr;
        }

        public void SaveImage(HttpPostedFileBase p)
        {
            StorageHelper.AddImage("StorageConnectionString", "images", p.InputStream, p.FileName);
        }
        
    }
}
