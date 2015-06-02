using System;
namespace CRMSanto.BusinessLayer.Services
{
    public interface IKlantService
    {
        CRMSanto.Models.Klant AddKlant(CRMSanto.Models.Klant klant, System.IO.Stream Image);
        CRMSanto.Models.Klant GetKlantByID(int id);
        System.Collections.Generic.List<CRMSanto.Models.Klant> GetKlanten();
        System.Collections.Generic.List<CRMSanto.Models.Klant> GetKlantenByPostCode(CRMSanto.Models.Adres Add);
        System.Collections.Generic.List<CRMSanto.Models.Mutualiteit> GetMutualiteiten();
    }
}
