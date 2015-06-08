using System;
namespace CRMSanto.BusinessLayer.Repository
{
    public interface IAfsprakenRepository
    {
        System.Collections.Generic.IEnumerable<CRMSanto.Models.Afspraak> All();
        CRMSanto.Models.Afspraak GetByID(object id);
        CRMSanto.Models.Afspraak Insert(CRMSanto.Models.Afspraak entity);
        System.Collections.Generic.List<CRMSanto.Models.Afspraak> Today();
    }
}
