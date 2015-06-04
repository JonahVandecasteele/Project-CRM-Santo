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
        public override Klant GetByID(object id)
        {
            var query = (from k in context.Klant.Include(g => g.Geslacht).Include(kar => kar.Karaktertrek).Include(m => m.MedischeFiche).Include(p => p.PersoonlijkeFiche) where k.ID==(int)id select k);
            return query.SingleOrDefault<Klant>();
        }
        public IEnumerable<Klant> GetByPostCode(string postcode)
        {
            var query = (from k in context.Klant.Include(g => g.Geslacht).Include(kar => kar.Karaktertrek).Include(m => m.MedischeFiche).Include(p => p.PersoonlijkeFiche) where k.Adres.Postcode == postcode select k);
            return query.ToList<Klant>();
        }
        public override Klant Insert(Klant entity)
        {
            context.Adres.Attach(entity.Adres);
            context.Geslacht.Attach(entity.Geslacht);
            foreach (Karaktertrek kar in entity.Karaktertrek)
            {
                context.Karaktertrek.Attach(kar);
            }
            context.MedischeFiche.Attach(entity.MedischeFiche);
            context.PersoonlijkeFiche.Attach(entity.PersoonlijkeFiche);
            Klant klant = context.Klant.Add(entity);
            return klant;
        }
        public override void Update(Klant entityToUpdate)
        {
            context.Adres.Attach(entityToUpdate.Adres);
            context.Geslacht.Attach(entityToUpdate.Geslacht);
            foreach (Karaktertrek kar in entityToUpdate.Karaktertrek)
            {
                context.Karaktertrek.Attach(kar);
            }
            context.MedischeFiche.Attach(entityToUpdate.MedischeFiche);
            context.PersoonlijkeFiche.Attach(entityToUpdate.PersoonlijkeFiche);
            Klant klant = context.Klant.Add(entityToUpdate);

            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

    }
}
