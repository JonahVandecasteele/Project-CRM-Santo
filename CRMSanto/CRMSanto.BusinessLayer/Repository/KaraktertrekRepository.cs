using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;


namespace CRMSanto.BusinessLayer.Repository
{
    public class KaraktertrekRepository : GenericRepository<Karaktertrek>, CRMSanto.BusinessLayer.Repository.IKaraktertrekRepository
    {
        public KaraktertrekRepository(ApplicationDbContext context)
            : base(context)
        {
        }
        public override IEnumerable<Karaktertrek> All()
        {
            var query = (from k in context.Karaktertrek.Include(k => k.Klanten) select k);
            return query.ToList<Karaktertrek>();
        }
        public override Karaktertrek GetByID(object id)
        {
            var query = (from k in context.Karaktertrek.Include(k => k.Klanten) where k.ID==(int)id select k);
            return query.Single<Karaktertrek>();
        }

    }
}
