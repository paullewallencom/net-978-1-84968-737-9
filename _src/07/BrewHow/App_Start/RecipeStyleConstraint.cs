using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using BrewHow.Domain.Repositories;

namespace BrewHow
{
    public class RecipeStyleConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext,
            Route route,
            string parameterName,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            if (!values.ContainsKey(parameterName))
            {
                return false;
            }

            var styleRepository = DependencyResolver
                .Current
                .GetService(typeof(IStyleRepository))
                as IStyleRepository;

            string styleSlug =
                (string)values[parameterName];

            var style = styleRepository
                .GetStyleBySlug(styleSlug);

            return style != null;
        }
    }
}