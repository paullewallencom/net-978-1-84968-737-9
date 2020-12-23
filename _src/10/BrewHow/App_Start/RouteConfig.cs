using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BrewHow
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
   	            name: "BeerByStyle",
                url: "Recipe/{style}",
                defaults: new { 
		            controller = "Recipe", 
		            action = "Style" 
	            },
	            constraints: new { 
		            style = new RecipeStyleConstraint()
                },
                namespaces: new [] { "BrewHow.Controllers" }	            
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{slug}",
                defaults: new { 
                    controller = "Recipe", 
                    action = "Index", 
                    id = UrlParameter.Optional, 
                    slug = UrlParameter.Optional
                },
                namespaces: new[] { "BrewHow.Controllers" }	            
            );
        }
    }
}