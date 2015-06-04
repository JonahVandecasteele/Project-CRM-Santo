using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMSanto.BusinessLayer.Repository;

namespace CRMSanto.BusinessLayer.Services
{
    public class KlantService : IKlantService
    {
        private IGenericRepository<Mutualiteit> repoMutualiteit = null;
        private IGenericRepository<Geslacht> repoGeslacht = null;
        private IGenericRepository<Gemeente> repoGemeente = null;
        private IGenericRepository<Werksituatie> repoWerksituatie = null;
        private IGenericRepository<Karaktertrek> repoKaraktertrek = null;
        private IKlantenRepository repoKlant = null;


        public KlantService(IGenericRepository<Mutualiteit> repoMutualiteit, IGenericRepository<Geslacht> repoGeslacht, IGenericRepository<Gemeente> repoGemeente, IGenericRepository<Werksituatie> repoWerksituatie, IGenericRepository<Karaktertrek> repoKaraktertrek, IKlantenRepository repoKlant)
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
        public Klant InsertKlant(Klant klant,Stream Image)
        {
            StorageHelper.AddImage("StorageConnectionString", "images", Image, System.Guid.NewGuid().ToString());
           Klant result = repoKlant.Insert(klant);
           repoKlant.SaveChanges();
           return result;
        }
        public List<Mutualiteit> GetMutualiteiten()
        {
            return repoMutualiteit.All().ToList<Mutualiteit>();
        }
        public List<Gemeente> GetGemeentes()
        {
            return repoGemeente.All().ToList<Gemeente>();
        }
        public List<Geslacht> GetGeslachten()
        {
            return repoGeslacht.All().ToList<Geslacht>();
        }
        public List<Werksituatie> GetWerkSituaties()
        {
            return repoWerksituatie.All().ToList<Werksituatie>();
        }
    }
}
