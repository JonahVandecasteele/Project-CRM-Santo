using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace CRMSanto.BusinessLayer.Repository
{
    public class AfsprakenRepository : GenericRepository<Afspraak>, CRMSanto.BusinessLayer.Repository.IAfsprakenRepository
    {
        public AfsprakenRepository(ApplicationDbContext context) : base(context)
        {

        }
        public override IEnumerable<Afspraak> All()
        {
            var query = (from a in context.Afspraak.Include(k => k.Klant).Include(m=>m.Masseur).Include(ms=>ms.SoortAfspraak) select a);
            return query.ToList<Afspraak>();
        }
        public List<Afspraak> AfsprakenVandaag() 
        {
            DateTime dt = DateTime.Now;
            var query = (from a in context.Afspraak.Include(k => k.Klant).Include(m => m.Masseur).Include(ms=>ms.SoortAfspraak).Include(k=>k.Klant.Adres)
                         where a.Geannuleerd == false && SqlFunctions.DateDiff("DAY",dt.Date, DbFunctions.TruncateTime(a.DatumTijdstip)) == 0
                         orderby a.DatumTijdstip ascending
                         select a);
            return query.ToList<Afspraak>();
        }
        public List<Afspraak> LopendeAfspraken()
        {
            var query = (from a in context.Afspraak.Include(k => k.Klant).Include(m=>m.Masseur).Include(ms=>ms.SoortAfspraak).Include(k=>k.Klant.Adres)
                         where a.Geannuleerd == false
                         select a);

            return query.ToList<Afspraak>();
        }
        public override Afspraak Insert(Afspraak entity)
        {
            context.Klant.Attach(entity.Klant);
            context.Masseur.Attach(entity.Masseur);
            context.SoortAfspraak.Attach(entity.SoortAfspraak);
            //context.Adres.Attach(entity.Adres);

            Afspraak afspraak = context.Afspraak.Add(entity);
            return afspraak;
        }
        public override Afspraak GetByID(object id)
        {
            var query = (from a in context.Afspraak.Include(k => k.Klant) select a);
            return query.SingleOrDefault<Afspraak>();
        }
    }
}
