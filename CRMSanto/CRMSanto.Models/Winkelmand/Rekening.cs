using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSanto.Models
{
    public class Rekening
    {
        public int ID { get; set; }
        public DateTime DatumTijdstip {get; set; }
        public Klant Klant {get;set;}
        public virtual List<ProductWinkelmand> Producten { get; set; }
    }
}
