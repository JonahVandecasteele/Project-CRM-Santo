﻿using CRMSanto.Models.Klant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSanto.Models
{
    public class PersoonlijkeFiche
    {
        public int ID { get; set; }
        public string Hobby { get; set; }
        public Werksituatie Werksituatie { get; set; }
        public string Gezinssituatie { get; set; }
    }
}
