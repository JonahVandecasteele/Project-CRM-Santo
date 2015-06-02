using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSanto.BusinessLayer.Services
{
    class KlantService
    {
        public List<Klant> GetKlanten()
        {
            return new List<Klant>();
        }
        public Klant GetKlantByID(int id)
        {
            return new Klant();
        }
        public List<Klant> GetKlantenByPostCode(Adres Add)
        {
            return new List<Klant>();
        }
        public Klant AddKlant(Klant klant,Stream Image)
        {
            StorageHelper.AddImage("WebShop.Properties.Settings.StorageConnectionString", "images", Image, System.Guid.NewGuid().ToString());
            return klant;
        }
    }
}
