using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

using BrewHow.Domain.Entities;
using BrewHow.Domain.Repositories;
using BrewHow.ViewModels;
using BrewHow.Models;

namespace BrewHow.Controllers
{
    public class RecipeController : Controller
    {
        // Create references to the repositories used 
        // to retrieve and persist entities used by 
        // the recipe controller.
        private readonly IRecipeRepository _recipeRepository;
        private readonly IStyleRepository _styleRepository;

        public RecipeController(IRecipeRepository recipeRepository,
            IStyleRepository styleRepository)
            : base()
        {
            this._recipeRepository = recipeRepository;
            this._styleRepository = styleRepository;
        }

        // Respond to requests to ~/ with the list of
        // recipes in the system.
        public ActionResult Index(int page = 0)
        {
            PagedResult<RecipeEntity, RecipeDisplayViewModel>
                model = null;

            model = new PagedResult<RecipeEntity, RecipeDisplayViewModel>(
                _recipeRepository.GetRecipes(),
                page,
                ToDisplayModel);

            return View(model);
        }

        // Retrieve all of the recipes of a certain style.
        public ActionResult Style(string style, int page = 0)
        {
            var model = new PagedResult<RecipeEntity, RecipeDisplayViewModel>(
                _recipeRepository.GetRecipesByStyleSlug(style),
                page,
                ToDisplayModel);

            var styleEntity = _styleRepository.GetStyleBySlug(style);

            if (style != null)
            {
                ViewBag.Title = styleEntity.Name + " Recipes";
            }

            return View("Index", model);
        }

        // Retrieve the details of a recipe from the
        // recipe repository and send them on to the 
        // view used to display them to the user.
        public ActionResult Details(int id)
        {
            var model = ToDisplayModel(
                this
                ._recipeRepository
                .GetRecipe(id));

            return View(model);
        }

        // Return a view that allows the requestor
        // to create new recipes.
        public ActionResult Create()
        {
            return View(ToEditModel(null));
        }

        // Respond to the request to create a new 
        // recipe and return to the requestor the 
        // list of recipes if it's successful or
        // return them to the edit page on
        // failure.  ONLY respond to HTTP POST
        // requests.  The runtime will use model
        // binding to populate the recipe parameter.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecipeEditViewModel recipe)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._recipeRepository.Save(
                        ToEntity(recipe));

                    return RedirectToAction("Index");
                }
            }
            catch
            {
            }

            return View(recipe);
        }

        // Return a view that allows the requestor
        // to edit a specific recipe.  Return to the
        // view a ViewModel representing the recipe.
        public ActionResult Edit(int id)
        {
            var recipeToEdit = 
                this
                ._recipeRepository
                .GetRecipe(id);

            return View(ToEditModel(recipeToEdit));
        }

        // Respond to the request to edit an
        // existing recipe and return to the 
        // requestor the list of recipes if it's
        // successful or return them to the edit 
        // page on failure.  ONLY respond to HTTP 
        // POST requests.  The runtime will use 
        // model binding to populate the recipe 
        // parameter.
        [HttpPost]
        public ActionResult Edit(RecipeEditViewModel recipe)
        {
            try
            {
                this._recipeRepository.Save(
                    ToEntity(recipe));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Converts a recipe entity into the display model sent
        /// to the view.
        /// </summary>
        /// <param name="entity">The entity to convert</param>
        /// <returns>A RecipeDisplayViewModel that represents the underlying domain entity</returns>
        private RecipeDisplayViewModel ToDisplayModel(RecipeEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(
                    "entity",
                    "Cannot convert an entity to a view model when no entity is passed.");
            }

            return new RecipeDisplayViewModel
            {
                RecipeId = entity.RecipeId,
                Name = entity.Name,
                Category = entity.Style.Category.ToString(),
                Style = entity.Style.Name,
                OriginalGravity = entity.OriginalGravity,
                FinalGravity = entity.FinalGravity,
                PercentAlcoholByVolume = entity.PercentAlcoholByVolume,
                GrainBill = entity.GrainBill,
                Instructions = entity.Instructions,
                Slug = entity.Slug,
                StyleSlug = entity.Style.Slug
            };
        }

        // Convert a recipe entity to a recipe edit 
        // ViewModel.  The ViewModel provides all of
        // the data necessary for the view to 
        // perform any edits on the recipe.
        private RecipeEditViewModel ToEditModel(RecipeEntity recipe)
        {
            var editModel = new RecipeEditViewModel();

            var styles = this._styleRepository.GetStyles();


            editModel.StyleList = new SelectList(
                styles,
                "StyleId",
                "Name",
                0);

            if (recipe != null)
            {
                editModel.FinalGravity = recipe.FinalGravity;
                editModel.GrainBill = recipe.GrainBill;
                editModel.Instructions = recipe.Instructions;
                editModel.Name = recipe.Name;
                editModel.OriginalGravity = recipe.OriginalGravity;
                editModel.RecipeId = recipe.RecipeId;
                editModel.StyleId = recipe.Style.StyleId;
                editModel.Slug = recipe.Slug;
            }

            return editModel;
        }

        // Convert a recipe edit view model to a recipe
        // entity for persistance.
        private RecipeEntity ToEntity(RecipeEditViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(
                    "viewModel", 
                    "Must have something to convert to an entity.");
            }

            var style = 
                this
                ._styleRepository
                .GetStyle(viewModel.StyleId);

            return new RecipeEntity
            {
                FinalGravity = viewModel.FinalGravity,
                GrainBill = viewModel.GrainBill,
                Instructions = viewModel.Instructions,
                Name = viewModel.Name,
                OriginalGravity = viewModel.OriginalGravity,
                RecipeId = viewModel.RecipeId,
                Slug = viewModel.Slug,
                Style = style,
            };
        }
    }
}
