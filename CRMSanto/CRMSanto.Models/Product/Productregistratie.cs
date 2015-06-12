using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRMSanto.Models
{
    public class Productregistratie
    {
        public int ID { get; set; }
        public Product Product { get; set; }
        public int Aantal { get; set; }
        public Winkelmand Winkelmand { get; set; }
    }
}