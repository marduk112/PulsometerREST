using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace REST
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class RouteConfig
    {
        /// <summary>
        /// Register routes i.e {controller}/{action}/{id}
        /// </summary>
        /// <param name="routes"></param>
        /// <remarks></remarks>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
