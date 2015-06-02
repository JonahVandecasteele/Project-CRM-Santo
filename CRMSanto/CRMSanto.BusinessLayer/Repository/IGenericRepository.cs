using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSanto.BusinessLayer.Repository
{
    public interface IGenericRepository<TEntity>
     where TEntity : class
    {
        System.Collections.Generic.IEnumerable<TEntity> All();
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        TEntity GetByID(object id);
        TEntity Insert(TEntity entity);
        void SaveChanges();
        void Update(TEntity entityToUpdate);
    }
}
