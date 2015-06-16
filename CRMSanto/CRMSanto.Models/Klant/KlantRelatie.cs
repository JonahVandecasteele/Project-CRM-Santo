using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSanto.Models
{
    public class KlantRelatie
    {
         public int ID { get; set; }
          public Klant Relatie { get; set; }
          public Relatie RelatieType { get; set; }
    }
}
