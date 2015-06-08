using CRMSanto.Models;
using System;
namespace CRMSanto.BusinessLayer.Repository
{
    public interface IKaraktertrekRepository : IGenericRepository<Karaktertrek>
    {
        System.Collections.Generic.IEnumerable<CRMSanto.Models.Karaktertrek> All();
    }
}
