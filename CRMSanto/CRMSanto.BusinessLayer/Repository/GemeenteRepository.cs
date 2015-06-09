using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSanto.BusinessLayer.Repository
{
    public class GemeenteRepository : GenericRepository<Gemeente>, IGemeenteRepository
    {
        public GemeenteRepository(ApplicationDbContext context)
            : base(context)
        {

        }
        public List<Gemeente> GetGemeentesByPostCode(string postcode)
        {
            var query = (from g in context.Gemeente where g.Postcode==postcode select g);
            return query.ToList<Gemeente>();
        }
    }
}
