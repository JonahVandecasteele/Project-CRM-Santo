using CRMSanto.Models;
using System;
namespace CRMSanto.BusinessLayer.Repository
{
    public interface ISessieRepository : IGenericRepository<Sessie>
    {
        CRMSanto.Models.Sessie GetByKlantID(object id);
        CRMSanto.Models.Sessie Insert(CRMSanto.Models.Sessie entity);
        void Update(CRMSanto.Models.Sessie entityToUpdate);
    }
}
