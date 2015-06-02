using CRMSanto.BusinessLayer.Repository;
using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSanto.BusinessLayer.Services
{
    public class ProductService : CRMSanto.BusinessLayer.Services.IProductService
    {
        private IGenericRepository<Product> repoProduct = null;
        public ProductService(IGenericRepository<Product> repoProduct)
        {
            this.repoProduct = repoProduct;
        }
        public List<Product> GetProducten()
        {
            return repoProduct.All().ToList<Product>();
        }

        public Product GetProductByID(int? id)
        {
            return repoProduct.GetByID(id.Value);
        }

        public void EditProduct(Product p)
        {
            repoProduct.Update(p);
            repoProduct.SaveChanges();
        }
    }
}
