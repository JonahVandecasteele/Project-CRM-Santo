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
    public class KlantRelatieRepository : GenericRepository<KlantRelatie>
    {
        public IEnumerable<KlantRelatie> All(int id)
        {
            var query = (from s in context.KlantRelatie.Include(a => a.RelatieType) where s.Relatie.ID == id select s);
            return query.ToList<KlantRelatie>();
        }
        public override KlantRelatie Insert(KlantRelatie entity)
        {
            context.Relatie.Attach(entity.RelatieType);
            return context.KlantRelatie.Add(entity);
        }
    }
}
