using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSanto.BusinessLayer.Repository
{
    public class MutualiteitenRepository
    {
        public List<Mutualiteit> GetMutualiteiten()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                List<Mutualiteit> mutualiteiten = new List<Mutualiteit>();
                var query = (from m in context.Mutualiteit select m);

                return query.ToList<Mutualiteit>();
            }
        }
    }
}
