using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using CRMSanto.BusinessLayer.Repository;
using CRMSanto.BusinessLayer.Services;
using CRMSanto.Models;
using CRMSanto.Controllers;

namespace CRMSanto
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IGenericRepository<Productregistratie>, GenericRepository<Productregistratie>>();
            container.RegisterType<IGenericRepository<Mutualiteit>, GenericRepository<Mutualiteit>>();
            container.RegisterType<IGenericRepository<Geslacht>, GenericRepository<Geslacht>>();
            container.RegisterType<IGenericRepository<Product>, GenericRepository<Product>>();
            container.RegisterType<IKaraktertrekRepository, KaraktertrekRepository>();
            container.RegisterType<ISessieRepository, SessieRepository>();
            container.RegisterType<IGenericRepository<Werksituatie>, GenericRepository<Werksituatie>>();
            container.RegisterType<IGemeenteRepository,GemeenteRepository>();
            container.RegisterType<IGenericRepository<Gemeente>, GenericRepository<Gemeente>>();
            container.RegisterType<IGenericRepository<Masseur>, GenericRepository<Masseur>>();
            container.RegisterType<IGenericRepository<SoortAfspraak>, GenericRepository<SoortAfspraak>>();
            container.RegisterType<IGenericRepository<Arrangement>, GenericRepository<Arrangement>>();
            container.RegisterType<IGenericRepository<Extra>, GenericRepository<Extra>>();
            container.RegisterType<IGenericRepository<Archief>, GenericRepository<Archief>>();
            container.RegisterType<IAfsprakenRepository, AfsprakenRepository>();
            container.RegisterType<IAfspraakService, AfspraakService>();
            container.RegisterType<IProductenRepository, ProductenRepository>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IKlantenRepository, KlantenRepository>();
            container.RegisterType<IKlantService, KlantService>();

            container.RegisterType<AccountController>(new InjectionConstructor());
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}