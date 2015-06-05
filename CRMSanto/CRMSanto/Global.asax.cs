using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Globalization;
using System.Threading;

namespace CRMSanto
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{

        //    string lang = Request.RequestContext.RouteData.Values["culture"].ToString();
        //    //CultureInfo culture = CultureInfo.InvariantCulture;//if need invariant
        //    CultureInfo culture = CultureInfo.GetCultureInfo(lang);

        //    Thread.CurrentThread.CurrentUICulture = culture;
        //    Thread.CurrentThread.CurrentCulture = culture;
        //}
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();   
        }
    }
}
