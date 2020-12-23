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
        private RecipeRepository _recipeRepository 
            = new RecipeRepository();
        private StyleRepository _styleRepository 
            = new StyleRepository();

        // Respond to requests to ~/ with the list of
        // recipes in the system.
        public ActionResult Index(int page = 0)
        {
            var model = new PagedResult<RecipeEntity, RecipeDisplayViewModel>(
                _recipeRepository.GetRecipes(), 
                page,
                ToDisplayModel);

            return View(model);
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
        public ActionResult Create(RecipeEditViewModel recipe)
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
            }

            return editModel;
        }

        // Convert a recipe edit ViewModel to a recipe
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
                Style = style,
            };
        }

        /// <summary>
        /// Called when the controller is being disposed.  
        /// Clean up any resources that implement disposable
        /// and are being maintained as a reference.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._recipeRepository.Dispose();
                this._recipeRepository = null;

                this._styleRepository.Dispose();
                this._styleRepository = null;
            }

            base.Dispose(disposing);
        }
    }
}
