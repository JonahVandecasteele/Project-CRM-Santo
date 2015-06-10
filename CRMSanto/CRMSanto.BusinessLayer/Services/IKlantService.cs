using System;
namespace CRMSanto.BusinessLayer.Services
{
    public interface IKlantService
    {
        CRMSanto.Models.Gemeente GetGemeenteByID(int id);
        System.Collections.Generic.List<CRMSanto.Models.Gemeente> GetGemeentes();
        System.Collections.Generic.List<CRMSanto.Models.Gemeente> GetGemeentesByPostCode(string id);
        CRMSanto.Models.Geslacht GetGeslachtByID(int id);
        System.Collections.Generic.List<CRMSanto.Models.Geslacht> GetGeslachten();
        CRMSanto.Models.Karaktertrek GetKaraktertrekByID(int id);
        System.Collections.Generic.List<CRMSanto.Models.Karaktertrek> GetKaraktertreken();
        CRMSanto.Models.Klant GetKlantByID(int id);
        System.Collections.Generic.List<CRMSanto.Models.Klant> GetKlanten();
        System.Collections.Generic.List<CRMSanto.Models.Klant> GetKlantenByPostCode(CRMSanto.Models.Adres Add);
        CRMSanto.Models.Mutualiteit GetMutualiteitByID(int id);
        System.Collections.Generic.List<CRMSanto.Models.Mutualiteit> GetMutualiteiten();
        CRMSanto.Models.Werksituatie GetWerkSituatieByID(int id);
        System.Collections.Generic.List<CRMSanto.Models.Werksituatie> GetWerkSituaties();
        CRMSanto.Models.Klant InsertKlant(CRMSanto.Models.Klant klant);
        void Mails();
        void SaveImage(System.Web.HttpPostedFileBase p, string filename);
        void UpdateKlant(CRMSanto.Models.Klant klant);
    }
}
