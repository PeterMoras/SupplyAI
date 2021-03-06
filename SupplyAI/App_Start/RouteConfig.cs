﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SupplyAI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            Startup.Init(); //inializes starup variables (like the database)

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Search", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
