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
        private IGenericRepository<Arrangement> repoArrangement = null;
        private IGenericRepository<Archief> repoArchief = null;
        private IGenericRepository<Extra> repoExtra = null;
        private ISessieRepository repoSessie = null;

        public AfspraakService(IAfsprakenRepository repoAfspraken, IGenericRepository<Masseur> repoMasseur, IGenericRepository<SoortAfspraak> repoMassage,IGenericRepository<Arrangement> repoArrangement,IGenericRepository<Archief> repoArchief,IGenericRepository<Extra> repoExtra, ISessieRepository repoSessie)
        {
            this.repoAfspraken = repoAfspraken;
            this.repoMasseur = repoMasseur;
            this.repoMassage = repoMassage;
            this.repoArrangement = repoArrangement;
            this.repoArchief = repoArchief;
            this.repoExtra = repoExtra;
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

        public List<Afspraak> AfsprakenSpecifiekeDag(DateTime dag)
        {
            return repoAfspraken.AfsprakenSpecifiekeDag(dag);
        }

        public List<Afspraak> VanafAfspraken(DateTime vanaf)
        {
            return repoAfspraken.VanafAfspraken(vanaf).ToList<Afspraak>();
        }
        public List<Afspraak> TussenTweeDatums(DateTime van,DateTime tot)
        {
            return repoAfspraken.TussenTweeDatums(van, tot);
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
            
            List<Afspraak> afspraken = repoAfspraken.LopendeAfspraken();
            //foreach (Afspraak afspraak in afspraken)
            //{
                bool nieuweAfspraakBookable = !afspraken.Any(x => x.DatumTijdstip >= a.DatumTijdstip.AddHours((a.Duur / 60) + 1) && x.DatumTijdstip.AddHours((x.Duur / 60) + 1) <= a.DatumTijdstip);
                //bool nieuweAfspraakBookable = !repoAfspraken.LopendeAfspraken().Any(x => x.DatumTijdstip >= a.DatumTijdstip.AddHours(a.Duur / 60) && x.DatumTijdstip.AddHours(x.Duur / 60) <= a.DatumTijdstip);
                if (nieuweAfspraakBookable == false)
                {
                    repoAfspraken.Insert(a);
                    repoAfspraken.SaveChanges();

                    try
                    {
                        Sessie k = repoSessie.GetByKlantID(a.Klant.ID);
                        k.AantalSessies++;
                        repoSessie.Update(k);
                        repoSessie.SaveChanges();
                    }
                    catch (Exception ex)
                    {

                        repoSessie.Insert(new Sessie() { AantalSessies = 1, Klant = a.Klant });
                        repoSessie.SaveChanges();

                    }
            }
            
             //}
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
        public SoortAfspraak InsertMassage(SoortAfspraak m)
        {
            SoortAfspraak result = repoMassage.Insert(m);
            repoMassage.SaveChanges();
            return result;
        }
        public void UpdateMassage(SoortAfspraak m)
        {
            repoMassage.Update(m);
            repoMassage.SaveChanges();
        }
        public List<Arrangement> GetArrangementen()
        {
            return repoArrangement.All().ToList<Arrangement>();
        }
        public Arrangement GetArrangementByID(int id)
        {
            return repoArrangement.GetByID(id);
        }
        public Arrangement InsertArrangement(Arrangement a)
        {
            Arrangement result = repoArrangement.Insert(a);
            repoArrangement.SaveChanges();
            return result;
        }
        public void UpdateArrangement(Arrangement a)
        {
            repoArrangement.Update(a);
            repoArrangement.SaveChanges();
        }
        public List<Extra> GetExtras()
        {
            return repoExtra.All().ToList<Extra>();
        }
        public Extra GetExtraByID(int id)
        {
            return repoExtra.GetByID(id);
        }
        public Extra InsertExtra(Extra e)
        {
            Extra result = repoExtra.Insert(e);
            repoExtra.SaveChanges();
            return result;
        }
        public void UpdateExtra(Extra e)
        {
            repoExtra.Update(e);
            repoExtra.SaveChanges();
        }
        public void InsertArchief(DateTime van, DateTime tot)
        {
            List<Afspraak> afspraken = repoAfspraken.TussenTweeDatums(van, tot);
            foreach(Afspraak a in afspraken)
            {
                Archief ar = new Archief();
                ar.AdresID = a.Adres.ID;
                ar.KlantID = a.Klant.ID;
                ar.MasseurID = a.Masseur.ID;
                ar.Notitie = a.Notitie;
                ar.SoloDuo = a.SoloDuo;
                ar.SoortAfspraakID = a.SoortAfspraak.ID;
                ar.Verplaatsing = a.Verplaatsing;
                ar.AantalPersonen = a.AantalPersonen;
                ar.ArrangementID = a.Arrangement.ID;
                ar.DatumTijdstip = a.DatumTijdstip;
                ar.Duur = a.Duur;
                repoArchief.Insert(ar);
                repoAfspraken.Delete(a);
            }
            repoArchief.SaveChanges();
            repoAfspraken.SaveChanges();
        }
    }
}
