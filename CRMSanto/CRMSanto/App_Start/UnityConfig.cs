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
            container.RegisterType<IGenericRepository<Product>, GenericRepository<Product>>();
            container.RegisterType<IGenericRepository<Mutualiteit>, GenericRepository<Mutualiteit>>();
            container.RegisterType<IGenericRepository<Geslacht>, GenericRepository<Geslacht>>();
            container.RegisterType<IGenericRepository<Karaktertrek>, GenericRepository<Karaktertrek>>();
            container.RegisterType<IGenericRepository<Werksituatie>, GenericRepository<Werksituatie>>();
            container.RegisterType<IGenericRepository<Gemeente>, GenericRepository<Gemeente>>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IKlantService, KlantService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}