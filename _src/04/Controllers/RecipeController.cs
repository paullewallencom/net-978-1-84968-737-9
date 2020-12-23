using System;
using System.Linq;
using System.Web.Mvc;

using BrewHow.Models;

namespace BrewHow.Controllers
{
    public class RecipeController : Controller
    {
        public ActionResult Index(int page = 0)
        {
            PagedResult<Recipe> recipes = null;

            using (var context = new BrewHowContext())
            {
                recipes = new PagedResult<Recipe>(
                    context.Recipes.OrderBy(r => r.Name),
                    page);
            }

            return View(recipes);
        }

    }
}
