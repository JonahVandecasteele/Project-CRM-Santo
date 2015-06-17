using System;
namespace CRMSanto.BusinessLayer.Services
{
    public interface IRekeningService
    {
        System.Collections.Generic.List<CRMSanto.Models.Rekening> GetRekeningen();
    }
}
