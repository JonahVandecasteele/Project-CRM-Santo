using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRMSanto.Models;

namespace CRMSanto.ViewModels
{
    public class InstellingenVM
    {
        public List<Karaktertrek> Karaktertreken { get; set; }
        public List<Werksituatie> Werksituaties { get; set; }
        public List<Mutualiteit> Mutualiteiten { get; set; }
    }
}