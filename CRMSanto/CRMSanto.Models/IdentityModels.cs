using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CRMSanto.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRMSanto.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Masseur> Masseur { get; set; }
        public DbSet<Voedingspatroon> Voedingspatroon { get; set; }
        public DbSet<MedischeFiche> MedischeFiche { get; set; }
        public DbSet<PersoonlijkeFiche> PersoonlijkeFiche { get; set; }
        public DbSet<Gemeente> Gemeente { get; set; }
        public DbSet<Adres> Adres { get; set; }
        public DbSet<Geslacht> Geslacht { get; set; }
        public DbSet<Karaktertrek> Karaktertrek { get; set; }
        public DbSet<Klant> Klant { get; set; }
        public DbSet<Mutualiteit> Mutualiteit { get; set; }
        public DbSet<Werksituatie> Werksituatie { get; set; }
        public DbSet<Afspraak> Afspraak { get; set; }
        public DbSet<SoortAfspraak> SoortAfspraak { get; set; }
        public DbSet<Sessie> Sessie { get; set; }
        public DbSet<Productregistratie> Productregistratie { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Arrangement> Arrangement { get; set; }
        public DbSet<Extra> Extra { get; set; }
        public DbSet<UserTokenCache> UserTokenCacheList { get; set; }

        public class UserTokenCache
        {
            [Key]
            public int UserTokenCacheId { get; set; }
            public string webUserUniqueId { get; set; }
            public byte[] cacheBits { get; set; }
            public DateTime LastWrite { get; set; }
        }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}