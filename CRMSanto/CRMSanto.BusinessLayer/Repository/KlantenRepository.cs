using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace CRMSanto.BusinessLayer.Repository
{
    public class KlantenRepository : GenericRepository<Klant>, IKlantenRepository
    {
        public KlantenRepository(ApplicationDbContext context) : base(context)
        {

        }
        public override IEnumerable<Klant> All()
        {
            var query = (from k in context.Klant.Include(g => g.Geslacht).Include(kar => kar.Karaktertrek).Include(m => m.MedischeFiche).Include(p => p.PersoonlijkeFiche) select k);
            return query.ToList<Klant>();
        }
        
    }
}
