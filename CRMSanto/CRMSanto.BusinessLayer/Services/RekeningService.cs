using CRMSanto.BusinessLayer.Repository;
using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSanto.BusinessLayer.Services
{
    public class RekeningService : CRMSanto.BusinessLayer.Services.IRekeningService
    {

        private IRekeningRepository repoRekeningen = null;

        public RekeningService(IRekeningRepository repoRekeningen)
        {
            this.repoRekeningen = repoRekeningen;
        }

        public List<Rekening> GetRekeningen()
        {
            return repoRekeningen.All().ToList<Rekening>();
        }

    }
}
