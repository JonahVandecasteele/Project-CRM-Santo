using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CRMSanto.Models;

namespace CRMSanto.BusinessLayer.Repository
{
    public class ProductenRepository:GenericRepository<Productregistratie>, CRMSanto.BusinessLayer.Repository.IProductenRepository
    {
        public ProductenRepository(ApplicationDbContext context) : base(context)
        {

        }
        public override IEnumerable<Productregistratie> All()
        {
            var query = (from pr in context.Productregistratie.Include(k => k.Klant).Include(p=>p.Product) select pr);
            return query.ToList<Productregistratie>();
        }
    }
}
