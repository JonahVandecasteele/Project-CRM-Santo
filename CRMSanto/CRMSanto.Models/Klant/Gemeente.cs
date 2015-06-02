using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSanto.Models
{
    public class Gemeente
    {
        public int ID { get; set; }
        public string Postcode { get; set; }
        public string Plaatsnaam { get; set; }
        public string Provincie { get; set; }
    }
}
