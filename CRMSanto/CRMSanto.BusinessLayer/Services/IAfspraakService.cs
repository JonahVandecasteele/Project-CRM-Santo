using System;
namespace CRMSanto.BusinessLayer.Services
{
    public interface IAfspraakService
    {
        void AddAfspraak(CRMSanto.Models.Afspraak a);
        System.Collections.Generic.List<CRMSanto.Models.Afspraak> AfsprakenSpecifiekeDag(DateTime dag);
        CRMSanto.Models.Afspraak GetAfspraakByID(int? id);
        System.Collections.Generic.List<CRMSanto.Models.Afspraak> GetAfspraken();
        System.Collections.Generic.List<CRMSanto.Models.Afspraak> GetAfsprakenByKlantenID(int id);
        System.Collections.Generic.List<CRMSanto.Models.Afspraak> GetAfsprakenToday();
        CRMSanto.Models.Arrangement GetArrangementByID(int id);
        System.Collections.Generic.List<CRMSanto.Models.Arrangement> GetArrangementen();
        System.Collections.Generic.List<CRMSanto.Models.Afspraak> GetDuurEnTijdstip(CRMSanto.Models.Afspraak a);
        CRMSanto.Models.Extra GetExtraByID(int id);
        System.Collections.Generic.List<CRMSanto.Models.Extra> GetExtras();
        System.Collections.Generic.List<CRMSanto.Models.Afspraak> GetLopendeAfspraken();
        CRMSanto.Models.SoortAfspraak GetMassageByID(int id);
        System.Collections.Generic.List<CRMSanto.Models.SoortAfspraak> GetMassages();
        CRMSanto.Models.Masseur GetMasseurByID(int id);
        System.Collections.Generic.List<CRMSanto.Models.Masseur> GetMasseurs();
        void InsertArchief(DateTime van, DateTime tot);
        CRMSanto.Models.Arrangement InsertArrangement(CRMSanto.Models.Arrangement a);
        CRMSanto.Models.Extra InsertExtra(CRMSanto.Models.Extra e);
        CRMSanto.Models.SoortAfspraak InsertMassage(CRMSanto.Models.SoortAfspraak m);
        System.Collections.Generic.List<CRMSanto.Models.Afspraak> TussenTweeDatums(DateTime van, DateTime tot);
        void UpdateArrangement(CRMSanto.Models.Arrangement a);
        void UpdateExtra(CRMSanto.Models.Extra e);
        void UpdateMassage(CRMSanto.Models.SoortAfspraak m);
        System.Collections.Generic.List<CRMSanto.Models.Afspraak> VanafAfspraken(DateTime vanaf);
    }
}
