using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMSanto.BusinessLayer.Repository;
using System.Web;
using mailinblue;

namespace CRMSanto.BusinessLayer.Services
{
    public class KlantService : CRMSanto.BusinessLayer.Services.IKlantService
    {
        private IGenericRepository<Mutualiteit> repoMutualiteit = null;
        private IGenericRepository<Geslacht> repoGeslacht = null;
        private IGemeenteRepository repoGemeente = null;
        private IGenericRepository<Werksituatie> repoWerksituatie = null;
        private IKaraktertrekRepository repoKaraktertrek = null;
        private IKlantenRepository repoKlant = null;


        public KlantService(IGenericRepository<Mutualiteit> repoMutualiteit, IGenericRepository<Geslacht> repoGeslacht, IGemeenteRepository repoGemeente, IGenericRepository<Werksituatie> repoWerksituatie, IKaraktertrekRepository repoKaraktertrek, IKlantenRepository repoKlant)
        {
            this.repoMutualiteit = repoMutualiteit;
            this.repoGeslacht = repoGeslacht;
            this.repoGemeente = repoGemeente;
            this.repoWerksituatie = repoWerksituatie;
            this.repoKaraktertrek = repoKaraktertrek;
            this.repoKlant = repoKlant;
        }
        public List<Klant> GetKlanten()
        {
            return repoKlant.All().ToList<Klant>();
        }
        public List<Klant> GetJarigen()
        {
            return repoKlant.GetJarigen().ToList<Klant>();
        }
        public Klant GetKlantByID(int id)
        {
            return repoKlant.GetByID(id);
        }
        public List<Klant> GetKlantenByPostCode(Adres Add)
        {
            return repoKlant.GetByPostCode(Add.Postcode).ToList<Klant>();
        }
        public void UpdateKlant(Klant klant)
        {
            repoKlant.Update(klant);
            repoKlant.SaveChanges();
        }
        public Klant InsertKlant(Klant klant)
        {
            
            Klant result = repoKlant.Insert(klant);
            repoKlant.SaveChanges();
            //StorageHelper.AddImage("StorageConnectionString", "images", Image, System.Guid.NewGuid().ToString());
            return result;
        }
        public void SaveImage(HttpPostedFileBase p, string filename)
        {
            repoKlant.SaveImage(p, filename);
        }
        public List<Mutualiteit> GetMutualiteiten()
        {
            return repoMutualiteit.All().ToList<Mutualiteit>();
        }
        public Mutualiteit GetMutualiteitByID(int id)
        {
            return repoMutualiteit.GetByID(id);
        }
        public List<Gemeente> GetGemeentes()
        {
            return repoGemeente.All().ToList<Gemeente>();
        }
        public Gemeente GetGemeenteByID(int id)
        {
            return repoGemeente.GetByID(id);
        }
        public List<Geslacht> GetGeslachten()
        {
            return repoGeslacht.All().ToList<Geslacht>();
        }
        public Geslacht GetGeslachtByID(int id)
        {
            return repoGeslacht.GetByID(id);
        }
        public List<Werksituatie> GetWerkSituaties()
        {
            return repoWerksituatie.All().ToList<Werksituatie>();
        }
        public Werksituatie GetWerkSituatieByID(int id)
        {
            return repoWerksituatie.GetByID(id);
        }
        public Werksituatie InsertWerkSituatie(Werksituatie w)
        {
           Werksituatie result = repoWerksituatie.Insert(w);
           repoWerksituatie.SaveChanges();
           return result;
        }
        public List<Karaktertrek> GetKaraktertreken()
        {
            return repoKaraktertrek.All().ToList<Karaktertrek>();
        }
        public Karaktertrek GetKaraktertrekByID(int id)
        {
            return repoKaraktertrek.GetByID(id);
        }
        public Karaktertrek InsertKaraktertrek(Karaktertrek k)
        {
            Karaktertrek result = repoKaraktertrek.Insert(k);
            repoKaraktertrek.SaveChanges();
            return result;
        }
        public Mutualiteit InsertMutualiteit(Mutualiteit m)
        {
            Mutualiteit result = repoMutualiteit.Insert(m);
            repoMutualiteit.SaveChanges();
            return result;
        }
        public List<Gemeente> GetGemeentesByPostCode(string id)
        {
            return repoGemeente.GetGemeentesByPostCode(id);
        }
        public void Mails()
        {
            List<Klant> klanten = new List<Klant>();
            klanten = GetKlanten();
            API sendinBlue = new mailinblue.API("r0GZv13CEFbk8yVq");
            foreach (Klant k in klanten)
            {
                Dictionary<string, string> attributes = new Dictionary<string, string>();
                attributes.Add("NAME", k.Voornaam);
                attributes.Add("SURNAME", k.Naam);
                List<int> listid = new List<int>();
                listid.Add(1);
                listid.Add(4);
                listid.Add(4);
                List<int> listid_unlink = new List<int>();
                listid_unlink.Add(2);
                listid_unlink.Add(5);
                Object createUpdatetUser = sendinBlue.create_update_user(k.Email, attributes, 0, listid, listid_unlink, 0);
                Console.WriteLine(createUpdatetUser);
            }
        }
        public void SendMail()
        {
	        {
                API sendinBlue = new mailinblue.API("r0GZv13CEFbk8yVq");
                Dictionary<string, string> to = new Dictionary<string, string>();
				to.Add("vanhoucke.lukas@gmail.com","Lukas Vanhoucke");
				List<string> from_name = new List<string>();
				from_name.Add("jim.butseraen@gmail.com");
				from_name.Add("Den Buts");
				Dictionary<string, string> cc = new Dictionary<string, string>();
				cc.Add("cc@example.net","cc whom!");
				Dictionary<string, string> bcc = new Dictionary<string, string>();
				bcc.Add("bcc@example.net", "bcc whom!");
				List<string> replyto = new List<string>();
				replyto.Add("replyto@email.com");
				replyto.Add("reply to!");
				Dictionary<string, string> headers = new Dictionary<string, string>();
				headers.Add("Content-Type","text/html; charset=iso-8859-1");
				headers.Add("X-param1","value1");
				headers.Add("X-param2","value2");
				headers.Add("X-Mailin-custom","my custom value");
				headers.Add("X-Mailin-IP","102.102.1.2");
				headers.Add("X-Mailin-Tag","My tag");
                Object sendEmail = sendinBlue.send_email(to, "My subject", from_name, "This is the <h1>HTML</h1>", "This is the text", cc, bcc, replyto, new Dictionary<string, string>(), headers);
	        }
        }
    }
}
