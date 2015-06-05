using System;
namespace CRMSanto.BusinessLayer.Repository
{
    public interface IProductenRepository
    {
        System.Collections.Generic.IEnumerable<CRMSanto.Models.Productregistratie> All();
        void SaveImage(System.Web.HttpPostedFileBase p);
    }
}
