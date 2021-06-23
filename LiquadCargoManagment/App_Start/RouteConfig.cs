﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LiquadCargoManagment
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
                 namespaces: new[] {
                "LiquadCargoManagment.Controllers"
                }
            );


            routes.MapRoute(
               name: "accounts",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "accounts", action = "customerledger", id = UrlParameter.Optional },
                namespaces: new[] {
                "LiquadCargoManagment.Controllers"
               }
           );
        }
    }
}
