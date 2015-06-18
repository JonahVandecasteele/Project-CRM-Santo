using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity.SqlServer;
using mailinblue;

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
            var query = (from k in context.Klant.Include(g => g.Geslacht).Include(kar => kar.Karaktertrek).Include(m => m.MedischeFiche).Include(p => p.PersoonlijkeFiche).Include(p => p.PersoonlijkeFiche.Werksituatie).Include(a => a.Adres).Include(g => g.Adres.Gemeente).Include(r => r.KlantRelatie).Include(v => v.Voedingspatroon)
                         orderby k.Naam
                         select k);
           
            return query.ToList<Klant>();
        }
        public List<Klant> GetJarigen()
        {
            DateTime dt = DateTime.Now;
            var query = (from k in context.Klant
                         where ((SqlFunctions.DatePart("DAY", k.Geboortedatum) == SqlFunctions.DatePart("DAY", dt)) && (SqlFunctions.DatePart("MONTH", k.Geboortedatum) == SqlFunctions.DatePart("MONTH", dt)))
                         select k);
            return query.ToList<Klant>();
        }
        public override Klant GetByID(object id)
        {
            var query = (from k in context.Klant.Include(g => g.Geslacht).Include(kar => kar.Karaktertrek).Include(m => m.MedischeFiche).Include(p => p.PersoonlijkeFiche).Include(p => p.PersoonlijkeFiche.Werksituatie).Include(a => a.Adres).Include(g => g.Adres.Gemeente).Include(v => v.Voedingspatroon) where k.ID == (int)id select k);
            return query.SingleOrDefault<Klant>();
        }
        public IEnumerable<Klant> GetByPostCode(string postcode)
        {
            var query = (from k in context.Klant.Include(g => g.Geslacht).Include(kar => kar.Karaktertrek).Include(m => m.MedischeFiche).Include(p => p.PersoonlijkeFiche).Include(p => p.PersoonlijkeFiche.Werksituatie).Include(a => a.Adres).Include(g => g.Adres.Gemeente).Include(v => v.Voedingspatroon) where k.Adres.Postcode == postcode select k);
            return query.ToList<Klant>();
        }
        public override Klant Insert(Klant entity)
        {
            context.Adres.Add(entity.Adres);
            //

            if (entity.Geslacht != null)
            {
                context.Geslacht.Attach(entity.Geslacht);
            }
            if (entity.Karaktertrek != null)
            {
                foreach (Karaktertrek item in entity.Karaktertrek)
                {
                    context.Karaktertrek.Attach(item);
                }
            }

            if (entity.Adres.Gemeente != null)
            {
                context.Gemeente.Attach(entity.Adres.Gemeente);
            }

            context.Mutualiteit.Attach(entity.MedischeFiche.Mutualiteit);
            if (entity.PersoonlijkeFiche != null)
            {
                context.PersoonlijkeFiche.Add(entity.PersoonlijkeFiche);
                context.Werksituatie.Attach(entity.PersoonlijkeFiche.Werksituatie);
            }
            if(entity.MedischeFiche!=null)context.MedischeFiche.Add(entity.MedischeFiche);
            
            if (entity.KlantRelatie != null)
            {
                foreach (KlantRelatie item in entity.KlantRelatie)
                {
                   // context.Relatie.Attach(item.RelatieType);
                }
            }
            //context.KlantRelatie.AddRange(entity.KlantRelaties);
            Klant klant = context.Klant.Add(entity);
            return klant;
        }
        public override void Update(Klant entityToUpdate)
        {
            Klant old = GetByID(entityToUpdate.ID);
            
            //Lower Vars
            context.PersoonlijkeFiche.Attach(old.PersoonlijkeFiche);
            context.Entry(old.PersoonlijkeFiche).CurrentValues.SetValues(entityToUpdate.PersoonlijkeFiche);
            if (old.PersoonlijkeFiche.Werksituatie != null && old.PersoonlijkeFiche.Werksituatie.ID != entityToUpdate.PersoonlijkeFiche.Werksituatie.ID)
            {
                context.Werksituatie.Attach(entityToUpdate.PersoonlijkeFiche.Werksituatie);
                context.PersoonlijkeFiche.Include(m => m.Werksituatie).FirstOrDefault(m => m.ID == entityToUpdate.PersoonlijkeFiche.ID).Werksituatie = entityToUpdate.PersoonlijkeFiche.Werksituatie;
            }
            context.MedischeFiche.Attach(old.MedischeFiche);
            context.Entry(old.MedischeFiche).CurrentValues.SetValues(entityToUpdate.MedischeFiche);
            //Top Level Vars
            if (old.Geslacht != null && old.Geslacht.ID != entityToUpdate.Geslacht.ID)
            {
                context.Geslacht.Attach(entityToUpdate.Geslacht);
                context.Klant.Include(m => m.Geslacht).FirstOrDefault(m => m.ID == entityToUpdate.ID).Geslacht = entityToUpdate.Geslacht;
            }
            context.Entry(old.Adres).CurrentValues.SetValues(entityToUpdate.Adres);
            if (old.Adres.Gemeente != null && old.Adres.Gemeente.ID != entityToUpdate.Adres.Gemeente.ID && entityToUpdate.Adres.Gemeente.ID != 0)
            {
                context.Gemeente.Attach(entityToUpdate.Adres.Gemeente);
                context.Adres.Include(m => m.Gemeente).FirstOrDefault(m => m.ID == entityToUpdate.Adres.ID).Gemeente = entityToUpdate.Adres.Gemeente;
            }
            foreach (Karaktertrek karakter in entityToUpdate.Karaktertrek)
            {
                if (old.Karaktertrek != null&&!old.Karaktertrek.Exists(a => a.ID == karakter.ID))
                {
                    context.Karaktertrek.Attach(karakter);
                    context.Klant.Include(m => m.Karaktertrek).FirstOrDefault(m => m.ID == old.ID).Karaktertrek.Add(karakter);
                }

            }
            //if (old.Voedingspatroon != null && old.Voedingspatroon.ID != entityToUpdate.Voedingspatroon.ID)
            //{
            //    context.Voedingspatroon.Attach(entityToUpdate.Voedingspatroon);
            //    context.Klant.Include(m => m.Voedingspatroon).FirstOrDefault(m => m.ID == entityToUpdate.ID).Voedingspatroon = entityToUpdate.Voedingspatroon;
            //}
            context.Entry(old).CurrentValues.SetValues(entityToUpdate);
        }
        public void SaveImage(HttpPostedFileBase p,string filename)
        {
            StorageHelper.AddImage("StorageConnectionString", "images", p.InputStream, filename);
        }
        public void SendMail(Klant k)
        {
            {
                API sendinBlue = new mailinblue.API("r0GZv13CEFbk8yVq");
                Dictionary<string, string> to = new Dictionary<string, string>();
                to.Add(k.Email, k.Voornaam);
                List<string> from_name = new List<string>();
                from_name.Add("massage.santo@gmail.com");
                from_name.Add("Massage Santo");
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Content-Type", "text/html; charset=iso-8859-1");
                headers.Add("X-param1", "value1");
                headers.Add("X-param2", "value2");
                headers.Add("X-Mailin-custom", "my custom value");
                headers.Add("X-Mailin-IP", "102.102.1.2");
                headers.Add("X-Mailin-Tag", "My tag");
                Object sendEmail = sendinBlue.send_email(to, "Gelukkige verjaardag", from_name, "Gelukkige verjaardag", "This is the text", new Dictionary<string, string>(), headers);
            }
        }
    }
}
