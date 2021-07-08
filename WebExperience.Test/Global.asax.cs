using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebExperience.Test
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.Headers.AllKeys.Contains("Origin") && HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
               //   Response.AddHeader("Access-Control-Allow-Origin", "*");
               // Response.AppendHeader("Access-Control-Allow-Methods", "*");
               // Response.AddHeader("Access-Control-Max-Age", "8000");
                Response.Flush();
            }
        }
    }
}
