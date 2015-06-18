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
            var query = (from k in context.Klant.Include(g => g.Geslacht).Include(kar => kar.Karaktertrek).Include(m => m.MedischeFiche).Include(p => p.PersoonlijkeFiche).Include(a =>a.Adres).Include(g => g.Adres.Gemeente).Include(r => r.KlantRelatie)  select k);
           
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
            var query = (from k in context.Klant.Include(g => g.Geslacht).Include(kar => kar.Karaktertrek).Include(m => m.MedischeFiche).Include(p => p.PersoonlijkeFiche).Include(a => a.Adres).Include(g => g.Adres.Gemeente) where k.ID == (int)id select k);
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
            else
                context.Geslacht.Attach(entity.Geslacht);
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
            context.Adres.Attach(entityToUpdate.Adres);
            context.Geslacht.Attach(entityToUpdate.Geslacht);
            foreach (Karaktertrek kar in entityToUpdate.Karaktertrek)
            {
                context.Karaktertrek.Attach(kar);
            }
            context.MedischeFiche.Attach(entityToUpdate.MedischeFiche);
            context.PersoonlijkeFiche.Attach(entityToUpdate.PersoonlijkeFiche);
            context.Voedingspatroon.Attach(entityToUpdate.Voedingspatroon);
            Klant klant = context.Klant.Attach(entityToUpdate);

            context.Entry(entityToUpdate).State = EntityState.Modified;
            
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
