﻿using CRMSanto.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRMSanto.BusinessLayer.Repository;

namespace CRMSanto.BusinessLayer.Services
{
    public class KlantService : IKlantService
    {
        private IGenericRepository<Mutualiteit> repoMutualiteit = null;
        private IGenericRepository<Geslacht> repoGeslacht = null;
        private IGenericRepository<Gemeente> repoGemeente = null;
        private IGenericRepository<Werksituatie> repoWerksituatie = null;


        public KlantService(IGenericRepository<Mutualiteit> repoMutualiteit, IGenericRepository<Geslacht> repoGeslacht, IGenericRepository<Gemeente> repoGemeente, IGenericRepository<Werksituatie> repoWerksituatie)
        {
            this.repoMutualiteit = repoMutualiteit;
            this.repoGeslacht = repoGeslacht;
            this.repoGemeente = repoGemeente;
            this.repoWerksituatie = repoWerksituatie;
        }
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
            StorageHelper.AddImage("StorageConnectionString", "images", Image, System.Guid.NewGuid().ToString());
            return klant;
        }
        public List<Mutualiteit> GetMutualiteiten()
        {
            return repoMutualiteit.All().ToList<Mutualiteit>();
        }
        public List<Gemeente> GetGemeentes()
        {
            return repoGemeente.All().ToList<Gemeente>();
        }
        public List<Geslacht> GetGeslachten()
        {
            return repoGeslacht.All().ToList<Geslacht>();
        }
        public List<Werksituatie> GetWerkSituaties()
        {
            return repoWerksituatie.All().ToList<Werksituatie>();
        }
    }
}
