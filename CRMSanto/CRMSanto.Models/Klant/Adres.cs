using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRMSanto.Models
{
    public class Adres
    {
        public int ID { get; set; }
        public string Straat { get; set; }
        public string Nummer { get; set; }
        [Range(1000,9999,ErrorMessage = "Gelieve een geldige postcode in te vullen!")]
        public string Postbus { get; set; }
        public string Postcode { get; set; }
        public virtual Gemeente Gemeente { get; set; }

        public override string ToString()
        {
            return this.Straat + " " + this.Nummer + this.Postbus + "(" + this.Postbus + ")";
        }
    }
}