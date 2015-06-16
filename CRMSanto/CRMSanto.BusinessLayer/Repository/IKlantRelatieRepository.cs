using System;
namespace CRMSanto.BusinessLayer.Repository
{
    public interface IKlantRelatieRepository 
    {
        System.Collections.Generic.IEnumerable<CRMSanto.Models.KlantRelatie> All(int id);
        CRMSanto.Models.KlantRelatie Insert(CRMSanto.Models.KlantRelatie entity);
    }
}
