using CRMSanto.Models;
using System;
namespace CRMSanto.BusinessLayer.Repository
{
    public interface IKlantenRepository : IGenericRepository<Klant>
    {
        System.Collections.Generic.IEnumerable<CRMSanto.Models.Klant> All();
    }
}
