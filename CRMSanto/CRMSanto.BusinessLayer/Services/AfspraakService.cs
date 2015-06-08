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

        public AfspraakService(IGenericRepository<Afspraak> repoAfspraak)
        {
            this.repoAfspraak = repoAfspraak;
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
            repoAfspraak.Insert(a);
            repoAfspraak.SaveChanges();
        }
    }
}
