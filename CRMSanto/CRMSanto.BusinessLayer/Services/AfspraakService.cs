using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMSanto.BusinessLayer.Repository;
using CRMSanto.Models;

namespace CRMSanto.BusinessLayer.Services
{
    public class AfspraakService : CRMSanto.BusinessLayer.Services.IAfspraakService 
    {
        private IAfsprakenRepository repoAfspraken = null;
        private IGenericRepository<Afspraak> repoAfspraak = null;
        private IGenericRepository<Masseur> repoMasseur = null;

        public AfspraakService(IGenericRepository<Afspraak> repoAfspraak, IGenericRepository<Masseur> repoMasseur)
        {
            this.repoAfspraak = repoAfspraak;
            this.repoMasseur = repoMasseur;
        }

        public List<Afspraak> GetAfspraken()
        {
            return repoAfspraak.All().ToList<Afspraak>();
        }

        public List<Afspraak> GetAfsprakenToday()
        {
            return repoAfspraken.Today().ToList<Afspraak>();
        }

        public Afspraak GetAfspraakByID(int? id)
        {
            return repoAfspraak.GetByID(id.Value);
        }

        public void AddAfspraak(Afspraak a)
        {
            //ApplicationDbContext context = new ApplicationDbContext();

            //context.Klant.Add(a.Klant);
            //context.Entry<Klant>(a.Klant).State = System.Data.Entity.EntityState.Unchanged;
            

            repoAfspraken.Insert(a);
            repoAfspraak.SaveChanges();
        }

        public List<Masseur> GetMasseurs()
        {
            return repoMasseur.All().ToList<Masseur>();
        }
        public Masseur GetMasseurByID(int id)
        {
            return repoMasseur.GetByID(id);
        }
    }
}
