using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRMSanto.Models
{
    public class Klant
    {
        public int ID { get; set; }
        [Required(ErrorMessage="Gelieve een naam in te vullen!")]
        [StringLength(60, MinimumLength = 3)]
        public string Naam { get; set; }
        [Required(ErrorMessage="Gelieve een voornaam in te vullen")]
        [StringLength(60, MinimumLength = 3)]
        public string Voornaam { get; set; }
        public virtual Adres Adres { get; set; }
        public string Foto { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Telefoon { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name="E-mail")]
        public string Email { get; set; }
        public virtual Geslacht Geslacht { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Geboortedatum { get; set; }
        public virtual List<Karaktertrek> Karaktertrek { get; set; }
        public virtual List<Relatie> KlantRelatie { get; set; }
        public virtual MedischeFiche MedischeFiche { get; set; }
        public virtual PersoonlijkeFiche PersoonlijkeFiche { get; set; }
        public virtual List<KlantRelatie> KlantRelaties { get; set; }
        public virtual Voedingspatroon Voedingspatroon { get; set; }
    }
}