﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSanto.Models.Archief
{
    public class Archief
    {
        [Key]
        public Afspraak Afspraak { get; set; }
        public DateTime DatumTijdstip { get; set; }
        public Klant Klant { get; set; }
        public Boolean Verplaatsing { get; set; }
        public String Notitie { get; set; }
        public SoortAfspraak SoortAfspraak { get; set; }
        public int Duur { get; set; }
        public Boolean SoloDuo { get; set; }
        public Adres Adres { get; set; }
        public Boolean Geannuleerd { get; set; }
        public Masseur Masseur { get; set; }
        public Arrangement Arrangement { get; set; }
        public int AantalPersonen { get; set; }
        public virtual List<Extra> Extra { get; set; }
    }
}
