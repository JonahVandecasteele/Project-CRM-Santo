using System;
namespace CRMSanto.BusinessLayer.Services
{
    public interface IProductService
    {
        CRMSanto.Models.Product GetProductByID(int? id);
        System.Collections.Generic.List<CRMSanto.Models.Product> GetProducten();
    }
}
