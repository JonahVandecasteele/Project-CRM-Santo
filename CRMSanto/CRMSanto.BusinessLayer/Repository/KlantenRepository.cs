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
    public class KlantenRepository : GenericRepository<Klant>, CRMSanto.BusinessLayer.Repository.IKlantenRepository
    {
        public KlantenRepository(ApplicationDbContext context)
            : base(context)
        {

        }
        public override IEnumerable<Klant> All()
        {
            var query = (from k in context.Klant.Include(g => g.Geslacht).Include(kar => kar.Karaktertrek).Include(m => m.MedischeFiche).Include(p => p.PersoonlijkeFiche).Include(a =>a.Adres) select k);
            return query.ToList<Klant>();
        }
        public override Klant GetByID(object id)
        {
            var query = (from k in context.Klant.Include(g => g.Geslacht).Include(kar => kar.Karaktertrek).Include(m => m.MedischeFiche).Include(p => p.PersoonlijkeFiche).Include(a => a.Adres) where k.ID == (int)id select k);
            return query.SingleOrDefault<Klant>();
        }
        public IEnumerable<Klant> GetByPostCode(string postcode)
        {
            var query = (from k in context.Klant.Include(g => g.Geslacht).Include(kar => kar.Karaktertrek).Include(m => m.MedischeFiche).Include(p => p.PersoonlijkeFiche).Include(a => a.Adres) where k.Adres.Postcode == postcode select k);
            return query.ToList<Klant>();
        }
        public override Klant Insert(Klant entity)
        {
            context.Adres.Add(entity.Adres);
            context.Geslacht.Attach(entity.Geslacht);
            foreach (Karaktertrek item in entity.Karaktertrek)
            {
                context.Karaktertrek.Attach(item);
            }
            context.Mutualiteit.Attach(entity.MedischeFiche.Mutualiteit);
            if (entity.PersoonlijkeFiche.Werksituatie != null)
            context.Werksituatie.Attach(entity.PersoonlijkeFiche.Werksituatie);
            context.MedischeFiche.Add(entity.MedischeFiche);
            context.PersoonlijkeFiche.Add(entity.PersoonlijkeFiche);
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
            Klant klant = context.Klant.Attach(entityToUpdate);

            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public void SaveImage(HttpPostedFileBase p)
        {
            StorageHelper.AddImage("StorageConnectionString", "images", p.InputStream, p.FileName);
        }
    }
}
