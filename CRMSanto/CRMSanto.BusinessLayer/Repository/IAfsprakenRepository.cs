using System;
namespace CRMSanto.BusinessLayer.Repository
{
    public interface IAfsprakenRepository
    {
        System.Collections.Generic.IEnumerable<CRMSanto.Models.Afspraak> All();
        CRMSanto.Models.Afspraak GetByID(object id);
        System.Collections.Generic.List<CRMSanto.Models.Afspraak> Today();
    }
}
