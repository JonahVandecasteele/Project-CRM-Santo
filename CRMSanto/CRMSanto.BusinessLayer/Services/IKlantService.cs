using System;
namespace CRMSanto.BusinessLayer.Services
{
    public interface IKlantService
    {
        System.Collections.Generic.List<CRMSanto.Models.Gemeente> GetGemeentes();
        System.Collections.Generic.List<CRMSanto.Models.Geslacht> GetGeslachten();
        CRMSanto.Models.Klant GetKlantByID(int id);
        System.Collections.Generic.List<CRMSanto.Models.Klant> GetKlanten();
        System.Collections.Generic.List<CRMSanto.Models.Klant> GetKlantenByPostCode(CRMSanto.Models.Adres Add);
        System.Collections.Generic.List<CRMSanto.Models.Mutualiteit> GetMutualiteiten();
        System.Collections.Generic.List<CRMSanto.Models.Werksituatie> GetWerkSituaties();
        CRMSanto.Models.Klant InsertKlant(CRMSanto.Models.Klant klant, System.IO.Stream Image);
        void UpdateKlant(CRMSanto.Models.Klant klant);
    }
}
