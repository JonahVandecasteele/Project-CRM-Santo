using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSanto.Models
{
    public class Extra
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public decimal Prijs { get; set; }
        public List<Afspraak> Afspraak { get; set; }
    }
}
