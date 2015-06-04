﻿using System;
namespace CRMSanto.BusinessLayer.Services
{
    public interface IAfspraakService
    {
        void AddAfspraak(CRMSanto.Models.Afspraak a);
        CRMSanto.Models.Afspraak GetAfspraakByID(int? id);
        System.Collections.Generic.List<CRMSanto.Models.Afspraak> GetAfspraken();
    }
}