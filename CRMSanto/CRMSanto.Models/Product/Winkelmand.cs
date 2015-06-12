using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSanto.Models
{
    public class Winkelmand
    {
        public int ID { get; set; }
        public Klant Klant { get; set; }
        public List<Productregistratie> Productregistratie { get; set; }
    }
}
