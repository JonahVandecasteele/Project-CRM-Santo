using CRMSanto.Models;
using System;
namespace CRMSanto.BusinessLayer.Repository
{
    public interface IGemeenteRepository : IGenericRepository<Gemeente>
    {
        System.Collections.Generic.List<CRMSanto.Models.Gemeente> GetGemeentesByPostCode(string postcode);
    }
}
