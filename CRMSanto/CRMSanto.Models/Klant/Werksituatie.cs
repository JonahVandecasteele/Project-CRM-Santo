﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSanto.Models
{
    public class Werksituatie
    {
        public int ID { get; set; }
        [Required]
        public string Naam { get; set; }
    }
}
