using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BrewHow.Models;

namespace BrewHow.Controllers
{
    public class RecipeController : Controller
    {
        //
        // GET: /Recipe/

        public ActionResult Index()
        {
            var recipeListModel = new List<Recipe>
            {
                new Recipe { Name = "Sweaty Brown Ale", Style = "Brown Ale", OriginalGravity = 1.05f, FinalGravity = 1.01f },
                new Recipe { Name = "Festive Milk Stout", Style = "Sweet/Milk Stout", OriginalGravity = 1.058f, FinalGravity = 1.015f },
                new Recipe { Name = "Andy's Heffy", Style = "Heffeweisen", OriginalGravity = 1.045f, FinalGravity = 1.012f }
            };

            return View(recipeListModel);
        }

    }
}
