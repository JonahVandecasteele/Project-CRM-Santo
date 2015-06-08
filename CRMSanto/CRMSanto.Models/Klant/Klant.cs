﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRMSanto.Models
{
    public class Klant
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public virtual Adres Adres { get; set; }
        public string Foto { get; set; }
        public string Telefoon { get; set; }
        public string Email { get; set; }
        public virtual Geslacht Geslacht { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Geboortedatum { get; set; }
        public virtual List<Karaktertrek> Karaktertrek { get; set; }
        public virtual MedischeFiche MedischeFiche { get; set; }
        public virtual PersoonlijkeFiche PersoonlijkeFiche { get; set; }
    }
}