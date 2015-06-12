using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSanto.Models
{
    public class Archief
    {
        public int ID { get; set; }
        public DateTime DatumTijdstip { get; set; }
        public int KlantID { get; set; }
        public Boolean Verplaatsing { get; set; }
        public String Notitie { get; set; }
        public int SoortAfspraakID { get; set; }
        public int Duur { get; set; }
        public Boolean SoloDuo { get; set; }
        public int AdresID { get; set; }
        public Boolean Geannuleerd { get; set; }
        public int MasseurID { get; set; }
        public int ArrangementID { get; set; }
        public int AantalPersonen { get; set; }
    }
}
