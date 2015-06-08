using CRMSanto.Models;
using System;
namespace CRMSanto.BusinessLayer.Repository
{
    public interface IProductenRepository:IGenericRepository<Productregistratie>
    {
        System.Collections.Generic.IEnumerable<CRMSanto.Models.Productregistratie> All();
        CRMSanto.Models.Productregistratie Insert(CRMSanto.Models.Productregistratie entity);
        void SaveImage(System.Web.HttpPostedFileBase p);
    }
}
