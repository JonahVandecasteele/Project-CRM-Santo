using CRMSanto.Models;
using System;
namespace CRMSanto.BusinessLayer.Repository
{
    public interface IAfsprakenRepository : IGenericRepository<Afspraak>
    {
        System.Collections.Generic.List<CRMSanto.Models.Afspraak> AfsprakenSpecifiekeDag(DateTime dag);
        System.Collections.Generic.List<CRMSanto.Models.Afspraak> AfsprakenVandaag();
        System.Collections.Generic.IEnumerable<CRMSanto.Models.Afspraak> All();
        System.Collections.Generic.List<CRMSanto.Models.Afspraak> GetAfsprakenByKlantenID(int id);
        CRMSanto.Models.Afspraak GetByID(object id);
        System.Collections.Generic.List<CRMSanto.Models.Afspraak> GetDuurEnTijdstip(CRMSanto.Models.Afspraak b);
        CRMSanto.Models.Afspraak Insert(CRMSanto.Models.Afspraak entity);
        System.Collections.Generic.List<CRMSanto.Models.Afspraak> LopendeAfspraken();
        System.Collections.Generic.List<CRMSanto.Models.Afspraak> TussenTweeDatums(DateTime van, DateTime tot);
        System.Collections.Generic.List<CRMSanto.Models.Afspraak> VanafAfspraken(DateTime vanaf);
    }
}
