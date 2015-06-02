using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSanto.Models
{
    public class MedischeFiche
    {
        public string Voedingspatroon { get; set; }
        public string Spijsvertering { get; set; }
        public string HuidigeKlachten { get; set; }
        public virtual Mutualiteit Mutualiteit { get; set; }
    }
}
