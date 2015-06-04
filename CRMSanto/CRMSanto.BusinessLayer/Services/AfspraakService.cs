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
        private IAfsprakenRepository repoAfspraak = null;
        //private IGenericRepository<Afspraak> repoAfspraak = null;

        public AfspraakService(IAfsprakenRepository repoAfspraak)
        {
            this.repoAfspraak = repoAfspraak;
        }

        public List<Afspraak> GetAfspraken()
        {
            return repoAfspraak.All().ToList<Afspraak>();
        }

        public Afspraak GetAfspraakByID(int? id)
        {
            return repoAfspraak.GetByID(id.Value);
        }
    }
}
