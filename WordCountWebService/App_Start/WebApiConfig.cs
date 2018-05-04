using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WordCountWebService.App_Start;

namespace WordCountWebService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            GlobalConfiguration.Configuration.Formatters.Insert(0, new TextMediaTypeFormatter());
        }
    }
}
