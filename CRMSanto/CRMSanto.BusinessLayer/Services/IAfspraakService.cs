using System;
namespace CRMSanto.BusinessLayer.Services
{
    public interface IAfspraakService
    {
        CRMSanto.Models.Afspraak GetAfspraakByID(int id);
        System.Collections.Generic.List<CRMSanto.Models.Afspraak> GetAfspraken();
    }
}
