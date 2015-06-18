using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Core.Objects;

namespace CRMSanto.BusinessLayer.Repository
{
    public class AfsprakenRepository : GenericRepository<Afspraak>, CRMSanto.BusinessLayer.Repository.IAfsprakenRepository
    {
        public AfsprakenRepository(ApplicationDbContext context) : base(context)
        {

        }
        public override IEnumerable<Afspraak> All()
        {
            var query = (from a in context.Afspraak.Include(k => k.Klant).Include(m=>m.Masseur).Include(ms=>ms.SoortAfspraak) where a.Archief==false select a);
            return query.ToList<Afspraak>();
        }
        public IEnumerable<Afspraak> AllArchief()
        {
            var query = (from a in context.Afspraak.Include(k => k.Klant).Include(m => m.Masseur).Include(ms => ms.SoortAfspraak) where a.Archief == true select a);
            return query.ToList<Afspraak>();
        }
        public List<Afspraak> AfsprakenVandaag() 
        {
            DateTime dt = DateTime.Now;
            var query = (from a in context.Afspraak.Include(k => k.Klant).Include(m => m.Masseur).Include(ms=>ms.SoortAfspraak).Include(k=>k.Klant.Adres)
                         where a.Geannuleerd == false && a.Archief == false && SqlFunctions.DateDiff("DAY", dt.Date, DbFunctions.TruncateTime(a.DatumTijdstip)) == 0
                         orderby a.DatumTijdstip ascending
                         select a);
            return query.ToList<Afspraak>();
        }
        public List<Afspraak> AfsprakenSpecifiekeDag(DateTime dag)
        {
            var query = (from a in context.Afspraak.Include(k => k.Klant).Include(m => m.Masseur).Include(ms => ms.SoortAfspraak).Include(k => k.Klant.Adres)
                         where a.Geannuleerd == false && a.Archief == false && SqlFunctions.DateDiff("DAY", dag.Date, DbFunctions.TruncateTime(a.DatumTijdstip)) == 0
                         orderby a.DatumTijdstip ascending
                         select a);
            return query.ToList<Afspraak>();
        }
        public List<Afspraak> TussenTweeDatums(DateTime van,DateTime tot)
        {
            var query = (from a in context.Afspraak.Include(k=>k.Klant).Include(m=>m.Masseur).Include(ms=>ms.SoortAfspraak).Include(k=>k.Klant.Adres).Include(e=>e.Extra).Include(ar=>ar.Arrangement)
                         where a.DatumTijdstip>=van && a.DatumTijdstip<=tot
                         select a);

            return query.ToList<Afspraak>();
        }
        public List<Afspraak> LopendeAfspraken()
        {
            DateTime dt = DateTime.Now;
            var query = (from a in context.Afspraak.Include(k => k.Klant).Include(m=>m.Masseur).Include(ms=>ms.SoortAfspraak).Include(k=>k.Klant.Adres).Include(a=>a.Arrangement).Include(e=>e.Extra)
                         where a.Geannuleerd == false && a.Archief == false && SqlFunctions.DateDiff("DAY", dt.Date, DbFunctions.TruncateTime(a.DatumTijdstip)) >= 0
                         orderby a.DatumTijdstip ascending
                         select a);

            return query.ToList<Afspraak>();
        }
        public List<Afspraak> VanafAfspraken(DateTime vanaf)
        {
            var query = (from a in context.Afspraak.Include(k => k.Klant).Include(m => m.Masseur).Include(ms => ms.SoortAfspraak).Include(k => k.Klant.Adres)
                         where a.Geannuleerd == false && a.Archief == false && SqlFunctions.DateDiff("DAY", vanaf.Date, DbFunctions.TruncateTime(a.DatumTijdstip)) >= 0
                         orderby a.DatumTijdstip ascending
                         select a);

            return query.ToList<Afspraak>();
        }
        public List<Afspraak> GetAfsprakenByKlantenID(int id)
        {
            var query = (from a in context.Afspraak.Include(k => k.Klant).Include(m => m.Masseur).Include(ms => ms.SoortAfspraak).Include(k => k.Klant.Adres)
                         where a.Klant.ID == id
                         select a);

            return query.ToList<Afspraak>();
        }
        public override Afspraak Insert(Afspraak entity)
        {
            context.Klant.Attach(entity.Klant);
            context.Masseur.Attach(entity.Masseur);
            if (entity.SoortAfspraak != null)
            {
                context.SoortAfspraak.Attach(entity.SoortAfspraak);
            }
            
            if(entity.Arrangement != null)
            {
                context.Arrangement.Attach(entity.Arrangement);
            }

            if (entity.Extra != null)
            {
                context.Extra.Attach(entity.Extra);
            }
            //context.Adres.Attach(entity.Adres);

            Afspraak afspraak = context.Afspraak.Add(entity);
            return afspraak;
        }
        public override Afspraak GetByID(object id)
        {
            var query = (from a in context.Afspraak.Include(k => k.Klant).Include(m=>m.Masseur).Include(ms=>ms.SoortAfspraak).Include(k=>k.Klant.Adres).Include(a=>a.Arrangement).Include(e=>e.Extra)
                         where a.ID == (int)id
                         select a);
            return query.SingleOrDefault<Afspraak>();
        }
        public override void Update(Afspraak entityToUpdate)
        {
            Afspraak old = GetByID(entityToUpdate.ID);
           /* if (old.Klant.Adres == null)
                old.Klant.Adres = new Adres();
            if (old.Klant.Adres.Gemeente == null)
                old.Klant.Adres.Gemeente = new Gemeente();*/
            if (old.SoortAfspraak == null)
                old.SoortAfspraak = new SoortAfspraak();
            if (old.Masseur == null)
                old.Masseur = new Masseur();
            if (old.Arrangement == null)
                old.Arrangement = new Arrangement();
            if (old.Extra == null)
                old.Extra = new Extra();

            if (old.Klant.ID != entityToUpdate.Klant.ID)
            {
                context.Klant.Attach(entityToUpdate.Klant);
                context.Afspraak.Include(m => m.Klant).FirstOrDefault(m => m.ID == entityToUpdate.ID).Klant = entityToUpdate.Klant;
            }
            if (old.SoortAfspraak.ID != entityToUpdate.SoortAfspraak.ID)
            {
                context.SoortAfspraak.Attach(entityToUpdate.SoortAfspraak);
                context.Afspraak.Include(m => m.SoortAfspraak).FirstOrDefault(m => m.ID == entityToUpdate.ID).SoortAfspraak = entityToUpdate.SoortAfspraak;
            }
          /*  context.Entry(old.Klant.Adres).CurrentValues.SetValues(entityToUpdate.Klant.Adres);
            if (old.Klant.Adres.Gemeente.ID != entityToUpdate.Klant.Adres.Gemeente.ID && entityToUpdate.Klant.Adres.Gemeente.ID != 0)
            {
                context.Gemeente.Attach(entityToUpdate.Klant.Adres.Gemeente);
                context.Adres.Include(m => m.Gemeente).FirstOrDefault(m => m.ID == entityToUpdate.Klant.Adres.ID).Gemeente = entityToUpdate.Klant.Adres.Gemeente;
            }*/

            if (old.Masseur.ID != entityToUpdate.Masseur.ID)
            {
                context.Masseur.Attach(entityToUpdate.Masseur);
                context.Afspraak.Include(m => m.Masseur).FirstOrDefault(m => m.ID == entityToUpdate.ID).Masseur = entityToUpdate.Masseur;
            }

            if (old.Arrangement.ID != entityToUpdate.Arrangement.ID)
            {
                context.Arrangement.Attach(entityToUpdate.Arrangement);
                context.Afspraak.Include(m => m.Arrangement).FirstOrDefault(m => m.ID == entityToUpdate.ID).Arrangement = entityToUpdate.Arrangement;
            }
            if (old.Extra.ID != entityToUpdate.Extra.ID)
            {
                context.Extra.Attach(entityToUpdate.Extra);
                context.Afspraak.Include(m => m.Extra).FirstOrDefault(m => m.ID == entityToUpdate.ID).Extra = entityToUpdate.Extra;
            }
            
            context.Entry(old).CurrentValues.SetValues(entityToUpdate);
        }
        public void UpdateAnnuleer(Afspraak a)
        {
            Afspraak afspraak = context.Afspraak.Attach(a);
            context.Entry(a).State = EntityState.Modified;
        }
        public List<Afspraak> GetDuurEnTijdstip(Afspraak b)
        {
            double duur = (b.Duur + 60);
            DateTime beginNieuw = b.DatumTijdstip;
            DateTime eindeNieuw = beginNieuw.AddMinutes(duur);

            var query = (from a in context.Afspraak
                         where a.DatumTijdstip <= eindeNieuw && System.Data.Entity.DbFunctions.AddMinutes(a.DatumTijdstip, a.Duur + 60) >= beginNieuw && a.Geannuleerd != true && a.ID != b.ID
                         select a);
                        
            return query.ToList<Afspraak>();

            //System.Data.Entity.Core.Objects.ObjectQuery.Addminutes(a.DatumTijdstip, a.Duur + 60)
        }
    }
}
