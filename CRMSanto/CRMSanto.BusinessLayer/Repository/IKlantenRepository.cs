using System;
namespace CRMSanto.BusinessLayer.Repository
{
    public interface IKlantenRepository : IGenericRepository<CRMSanto.Models.Klant>
    {
        System.Collections.Generic.IEnumerable<CRMSanto.Models.Klant> All();
        CRMSanto.Models.Klant GetByID(object id);
        System.Collections.Generic.IEnumerable<CRMSanto.Models.Klant> GetByPostCode(string postcode);
        CRMSanto.Models.Klant Insert(CRMSanto.Models.Klant entity);
        void Update(CRMSanto.Models.Klant entityToUpdate);
    }
}
