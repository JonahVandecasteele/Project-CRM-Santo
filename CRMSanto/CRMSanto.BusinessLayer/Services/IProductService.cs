using System;
namespace CRMSanto.BusinessLayer.Services
{
    public interface IProductService
    {
        void AddProduct(CRMSanto.Models.Product p);
        void EditProduct(CRMSanto.Models.Product p);
        CRMSanto.Models.Product GetProductByID(int? id);
        System.Collections.Generic.List<CRMSanto.Models.Product> GetProducten();
        System.Collections.Generic.List<CRMSanto.Models.Productregistratie> GetProductregistraties();
    }
}
