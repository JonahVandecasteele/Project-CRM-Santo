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
    public class SessieRepository : GenericRepository<Sessie>, CRMSanto.BusinessLayer.Repository.ISessieRepository
    {
        public Sessie GetByKlantID(object id)
        {
            var query = (from s in context.Sessie.Include(k => k.Klant) where s.Klant.ID==(int)id select s);
            return query.Single<Sessie>();
        }
        public override Sessie Insert(Sessie entity)
        {
            context.Klant.Attach(entity.Klant);
            return context.Sessie.Add(entity);
        }
        public override void Update(Sessie entityToUpdate)
        {
            context.Klant.Attach(entityToUpdate.Klant);
            context.Sessie.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
