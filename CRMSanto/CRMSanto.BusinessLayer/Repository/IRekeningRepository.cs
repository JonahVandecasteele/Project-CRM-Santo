using System;
namespace CRMSanto.BusinessLayer.Repository
{
    public interface IRekeningRepository
    {
        System.Collections.Generic.IEnumerable<CRMSanto.Models.Rekening> All();
    }
}
