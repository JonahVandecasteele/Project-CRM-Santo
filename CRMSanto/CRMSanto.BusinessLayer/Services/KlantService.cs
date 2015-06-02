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
        public KlantService(IGenericRepository<Mutualiteit> repoMutualiteit)
        {
            this.repoMutualiteit = repoMutualiteit;
        }
        public List<Klant> GetKlanten()
        {
            return new List<Klant>();
        }
        public Klant GetKlantByID(int id)
        {
            return new Klant();
        }
        public List<Klant> GetKlantenByPostCode(Adres Add)
        {
            return new List<Klant>();
        }
        public Klant AddKlant(Klant klant,Stream Image)
        {
            StorageHelper.AddImage("StorageConnectionString", "images", Image, System.Guid.NewGuid().ToString());
            return klant;
        }
        public List<Mutualiteit> GetMutualiteiten()
        {
            return repoMutualiteit.All().ToList<Mutualiteit>();
        }
    }
}
