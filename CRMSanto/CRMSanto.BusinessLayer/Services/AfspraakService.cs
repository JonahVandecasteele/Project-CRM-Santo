using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CRMSanto.BusinessLayer.Repository;
using CRMSanto.Models;

namespace CRMSanto.BusinessLayer.Services
{
    public class AfspraakService : CRMSanto.BusinessLayer.Services.IAfspraakService
    {
        private IAfsprakenRepository repoAfspraken = null;
        private IGenericRepository<Masseur> repoMasseur = null;
        private IGenericRepository<SoortAfspraak> repoMassage = null;
        private ISessieRepository repoSessie = null;

        public AfspraakService(IAfsprakenRepository repoAfspraken, IGenericRepository<Masseur> repoMasseur, IGenericRepository<SoortAfspraak> repoMassage, ISessieRepository repoSessie)
        {
            this.repoAfspraken = repoAfspraken;
            this.repoMasseur = repoMasseur;
            this.repoMassage = repoMassage;
            this.repoSessie = repoSessie;
        }

        public List<Afspraak> GetAfspraken()
        {
            return repoAfspraken.All().ToList<Afspraak>();
        }

        public List<Afspraak> GetAfsprakenByKlantenID(int id) 
        {
            return repoAfspraken.GetAfsprakenByKlantenID(id);
        }

        public List<Afspraak> GetLopendeAfspraken()
        {
            return repoAfspraken.LopendeAfspraken().ToList<Afspraak>();
        }

        public List<Afspraak> GetAfsprakenToday()
        {
            return repoAfspraken.AfsprakenVandaag().ToList<Afspraak>();
        }

        public Afspraak GetAfspraakByID(int? id)
        {
            return repoAfspraken.GetByID(id.Value);
        }

        public void AddAfspraak(Afspraak a)
        {
            //ApplicationDbContext context = new ApplicationDbContext();

            //context.Klant.Add(a.Klant);
            //context.Entry<Klant>(a.Klant).State = System.Data.Entity.EntityState.Unchanged;
            //Sessie s = new Sessie {Klant=a.Klant,AantalSessies=1 };
            //s.AantalSessies.Add(s);
            try
            {
                Sessie k = repoSessie.GetByKlantID(a.Klant.ID);
                k.AantalSessies++;
                repoSessie.Update(k);
                repoSessie.SaveChanges();
            }
            catch(Exception ex)
            {

                repoSessie.Insert(new Sessie() { AantalSessies = 1, Klant = a.Klant });
                repoSessie.SaveChanges();

            }         
            repoAfspraken.Insert(a);
            repoAfspraken.SaveChanges();
        } 

        public List<Masseur> GetMasseurs()
        {
            return repoMasseur.All().ToList<Masseur>();
        }
        public Masseur GetMasseurByID(int id)
        {
            return repoMasseur.GetByID(id);
        }
        public List<SoortAfspraak> GetMassages()
        {
            return repoMassage.All().ToList<SoortAfspraak>();
        }
        public SoortAfspraak GetMassageByID(int id)
        {
            return repoMassage.GetByID(id);
        }
    }
}
