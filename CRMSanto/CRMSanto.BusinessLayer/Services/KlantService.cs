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
        private IGenericRepository<Relatie> repoRelatie = null;
        private IKlantRelatieRepository repoKlantRelatie = null;
        private IGenericRepository<Voedingspatroon> repoVoedingspatroon = null;


        public KlantService(IGenericRepository<Mutualiteit> repoMutualiteit, IGenericRepository<Geslacht> repoGeslacht, IGemeenteRepository repoGemeente, IGenericRepository<Werksituatie> repoWerksituatie, IKaraktertrekRepository repoKaraktertrek, IKlantenRepository repoKlant, IGenericRepository<Relatie> repoRelatie, IKlantRelatieRepository repoKlantRelatie,IGenericRepository<Voedingspatroon> repoVoedingspatroon)            
        {
            this.repoMutualiteit = repoMutualiteit;
            this.repoGeslacht = repoGeslacht;
            this.repoGemeente = repoGemeente;
            this.repoWerksituatie = repoWerksituatie;
            this.repoKaraktertrek = repoKaraktertrek;
            this.repoKlant = repoKlant;
            this.repoRelatie = repoRelatie;
            this.repoKlantRelatie = repoKlantRelatie;
            this.repoVoedingspatroon = repoVoedingspatroon;
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
        public List<Relatie> GetRelaties()
        {
            return repoRelatie.All().ToList<Relatie>();
        }
        public Relatie InsertRelatie(Relatie r)
        {
            Relatie result = repoRelatie.Insert(r);
            repoRelatie.SaveChanges();
            return result;
        }
        public List<KlantRelatie> GetKlantRelaties(Klant k)
        {
            return repoKlantRelatie.All(k.ID).ToList<KlantRelatie>();
        }

        public List<Voedingspatroon> GetVoedingspatronen()
        {
            return repoVoedingspatroon.All().ToList<Voedingspatroon>();
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
        public void SendMail(Klant k)
        {
	        {
                API sendinBlue = new mailinblue.API("r0GZv13CEFbk8yVq");
                Dictionary<string, string> to = new Dictionary<string, string>();
				to.Add(k.Email,k.Voornaam);
				List<string> from_name = new List<string>();
				from_name.Add("massage.santo@gmail.com");
				from_name.Add("Massage Santo");
				Dictionary<string, string> headers = new Dictionary<string, string>();
				headers.Add("Content-Type","text/html; charset=iso-8859-1");
				headers.Add("X-param1","value1");
				headers.Add("X-param2","value2");
				headers.Add("X-Mailin-custom","my custom value");
				headers.Add("X-Mailin-IP","102.102.1.2");
				headers.Add("X-Mailin-Tag","My tag");
                Object sendEmail = sendinBlue.send_email(to, "Gelukkige verjaardag", from_name, "Gelukkige verjaardag", "This is the text", new Dictionary<string, string>(), headers);
	        }
        }
    }
}
