using CRMSanto.Models;
using System;
namespace CRMSanto.BusinessLayer.Repository
{
    public interface IProductenRepository:IGenericRepository<Productregistratie>
    {
        System.Collections.Generic.IEnumerable<CRMSanto.Models.Productregistratie> All();
        System.Collections.Generic.List<CRMSanto.Models.Productregistratie> GetProductregistratiesByKlantenID(int id);
        System.Collections.Generic.List<CRMSanto.Models.Productregistratie> GetProductregistratiesByProductenID(int id);
        CRMSanto.Models.Productregistratie Insert(CRMSanto.Models.Productregistratie entity);
        void SaveImage(System.Web.HttpPostedFileBase p);
    }
}
