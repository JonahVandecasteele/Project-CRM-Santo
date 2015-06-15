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
        System.Collections.Generic.List<CRMSanto.Models.Klant> GetJarigen();
        CRMSanto.Models.Karaktertrek GetKaraktertrekByID(int id);
        System.Collections.Generic.List<CRMSanto.Models.Karaktertrek> GetKaraktertreken();
        CRMSanto.Models.Klant GetKlantByID(int id);
        System.Collections.Generic.List<CRMSanto.Models.Klant> GetKlanten();
        System.Collections.Generic.List<CRMSanto.Models.Klant> GetKlantenByPostCode(CRMSanto.Models.Adres Add);
        System.Collections.Generic.List<CRMSanto.Models.KlantRelatie> GetKlantRelaties(CRMSanto.Models.Klant k);
        CRMSanto.Models.Mutualiteit GetMutualiteitByID(int id);
        System.Collections.Generic.List<CRMSanto.Models.Mutualiteit> GetMutualiteiten();
        System.Collections.Generic.List<CRMSanto.Models.Relatie> GetRelaties();
        CRMSanto.Models.Werksituatie GetWerkSituatieByID(int id);
        System.Collections.Generic.List<CRMSanto.Models.Werksituatie> GetWerkSituaties();
        CRMSanto.Models.Karaktertrek InsertKaraktertrek(CRMSanto.Models.Karaktertrek k);
        CRMSanto.Models.Klant InsertKlant(CRMSanto.Models.Klant klant);
        CRMSanto.Models.Mutualiteit InsertMutualiteit(CRMSanto.Models.Mutualiteit m);
        CRMSanto.Models.Relatie InsertRelatie(CRMSanto.Models.Relatie r);
        CRMSanto.Models.Werksituatie InsertWerkSituatie(CRMSanto.Models.Werksituatie w);
        void Mails();
        void SaveImage(System.Web.HttpPostedFileBase p, string filename);
        void SendMail(CRMSanto.Models.Klant k);
        void UpdateKlant(CRMSanto.Models.Klant klant);
    }
}
