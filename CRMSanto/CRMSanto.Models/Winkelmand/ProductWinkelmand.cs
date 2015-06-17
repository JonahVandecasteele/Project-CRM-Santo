using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSanto.Models
{
    public class ProductWinkelmand
    {
        public int ID { get; set; }
        public Product VerkochtProduct { get; set; }
        public int Aantal { get; set; }
    }
}
