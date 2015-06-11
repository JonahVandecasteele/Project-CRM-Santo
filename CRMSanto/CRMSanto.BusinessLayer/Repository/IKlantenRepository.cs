using CRMSanto.Models;
using System;
namespace CRMSanto.BusinessLayer.Repository
{
    public interface IKlantenRepository:IGenericRepository<Klant>
    {
        System.Collections.Generic.IEnumerable<CRMSanto.Models.Klant> All();
        CRMSanto.Models.Klant GetByID(object id);
        System.Collections.Generic.IEnumerable<CRMSanto.Models.Klant> GetByPostCode(string postcode);
        System.Collections.Generic.List<CRMSanto.Models.Klant> GetJarigen();
        CRMSanto.Models.Klant Insert(CRMSanto.Models.Klant entity);
        void SaveImage(System.Web.HttpPostedFileBase p, string filename);
        void Update(CRMSanto.Models.Klant entityToUpdate);
    }
}
