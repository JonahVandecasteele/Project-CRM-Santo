﻿using System;
namespace CRMSanto.BusinessLayer.Repository
{
     public interface IAfsprakenRepository
    {
        System.Collections.Generic.IEnumerable<CRMSanto.Models.Afspraak> All();
    }
}
