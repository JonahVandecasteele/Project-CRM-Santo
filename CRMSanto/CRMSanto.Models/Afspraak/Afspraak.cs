﻿using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRMSanto.Models
{
    public class Afspraak
    {
        public int ID { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Tijdstip")]
        [Required]
        public DateTime DatumTijdstip { get; set; }
        [Required]
        public Klant Klant { get; set; }
        public Boolean Verplaatsing { get; set; }
        [Display(Name="Extra")]
        public String Notitie { get; set; }
        [Display(Name="Massage")]
        public SoortAfspraak SoortAfspraak { get; set; }
        public int Duur { get; set; }
        [Display(Name="Duo")]
        public Boolean SoloDuo { get; set; }
        public Adres Adres { get; set; }
        public Boolean Geannuleerd { get; set; }
        [Required]
        public Masseur Masseur { get; set; }
        public Arrangement Arrangement { get; set; }
        public int AantalPersonen { get; set; }
        public Extra Extra { get; set; }
        public bool Archief { get; set; }
    }
}