using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CRMSanto.BusinessLayer.Repository
{
    public class AfsprakenRepository : GenericRepository<Afspraak>, CRMSanto.BusinessLayer.Repository.IAfsprakenRepository
    {
        public AfsprakenRepository(ApplicationDbContext context) : base(context)
        {

        }
        public override IEnumerable<Afspraak> All()
        {
            var query = (from a in context.Afspraak.Include(k => k.Klant) select a);
            return query.ToList<Afspraak>();
        }

        public override Afspraak GetByID(object id)
        {
            var query = (from a in context.Afspraak.Include(k => k.Klant) select a);
            return query.SingleOrDefault<Afspraak>();
        }
    }
}
