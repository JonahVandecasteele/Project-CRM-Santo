using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMSanto.BusinessLayer.Repository;
using System.Web;

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
           // StorageHelper.AddImage("StorageConnectionString", "images", Image, System.Guid.NewGuid().ToString());
           Klant result = repoKlant.Insert(klant);
           repoKlant.SaveChanges();
           return result;
        }
        public void SaveImage(HttpPostedFileBase p)
        {
            repoKlant.SaveImage(p);
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
        public List<Karaktertrek> GetKaraktertreken()
        {
            return repoKaraktertrek.All().ToList<Karaktertrek>();
        }
        public Karaktertrek GetKaraktertrekByID(int id)
        {
            return repoKaraktertrek.GetByID(id);
        }
        public List<Gemeente> GetGemeentesByPostCode(string id)
        {
            return repoGemeente.GetGemeentesByPostCode(id);
        }
    }
}
