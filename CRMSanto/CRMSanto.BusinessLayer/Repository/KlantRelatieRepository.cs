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
    public class KlantRelatieRepository : GenericRepository<KlantRelatie>,IKlantRelatieRepository
    {
        public IEnumerable<KlantRelatie> All(int id)
        {
          //  var query = (from s in context.KlantRelatie    select s);
          //  return query.ToList<KlantRelatie>();
            return null;
        }
        public override KlantRelatie Insert(KlantRelatie entity)
        {
            //context.Klant.Attach(entity.Klant);
           // context.Klant.Attach(entity.Relatie);
            //context.Relatie.Attach(entity.RelatieType);
           // return context.KlantRelatie.Add(entity);
            return null;
        }
    }
}
