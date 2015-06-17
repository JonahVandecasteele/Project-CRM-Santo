using System.Linq;
using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CRMSanto.BusinessLayer.Repository
{
    public class RekeningRepository : GenericRepository<Rekening>, CRMSanto.BusinessLayer.Repository.IRekeningRepository
    {
        public RekeningRepository(ApplicationDbContext context)
            : base(context)
        {

        }
        public override IEnumerable<Rekening> All()
        {
            var query = (from pr in context.Rekening.Include(x => x.Producten).Include(w => w.Klant).Include(y => y.Producten.Select(z => z.VerkochtProduct)) select pr);
            return query.ToList<Rekening>();
        }
    }
}
