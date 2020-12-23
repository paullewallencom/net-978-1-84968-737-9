using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

using BrewHow.Domain.Entities;
using BrewHow.Domain.Repositories;
using BrewHow.Models;
using BrewHow.ViewModels;
using BrewHow.ViewModels.Mappers;
using WebMatrix.WebData;

namespace BrewHow.Controllers
{
    public class RecipeController : Controller
    {
        // Create references to the repositories used 
        // to retrieve and persist entities used by 
        // the recipe controller.
        private readonly IRecipeRepository _recipeRepository;
        private readonly IStyleRepository _styleRepository;
        private readonly IUserProfileEntityFactory _userProfileEntityFactory;
        private readonly IRecipeDisplayViewModelMapper _displayViewModelMapper;
        private readonly IRecipeEditViewModelMapper _editViewModelMapper;

        public RecipeController(IRecipeRepository recipeRepository,
            IStyleRepository styleRepository,
            IUserProfileEntityFactory userProfileEntityFactory,
            IRecipeDisplayViewModelMapper displayViewModelMapper,
            IRecipeEditViewModelMapper editViewModelMapper)
            : base()
        {
            this._recipeRepository = recipeRepository;
            this._styleRepository = styleRepository;
            this._userProfileEntityFactory = userProfileEntityFactory;
            this._displayViewModelMapper = displayViewModelMapper;
            this._editViewModelMapper = editViewModelMapper;
        }

        // Respond to requests to ~/ with the list of
        // recipes in the system.
        public ActionResult Index(int page = 0)
        {
            var recipes = _recipeRepository.GetRecipes();

            var viewModel = new PagedResult<RecipeEntity, RecipeDisplayViewModel>(
                recipes,
                page,
                this._displayViewModelMapper.EntityToViewModel);

            return View(viewModel);
        }

        // Retrieve all of the recipes of a certain style.
        public ActionResult Style(string style, int page = 0)
        {
            var recipesForStyle = 
                _recipeRepository.GetRecipesByStyleSlug(style);

            var viewModel = new PagedResult<RecipeEntity, RecipeDisplayViewModel>(
                recipesForStyle,
                page,
                this._displayViewModelMapper.EntityToViewModel);

            var styleEntity = _styleRepository.GetStyleBySlug(style);

            if (style != null)
            {
                ViewBag.Title = styleEntity.Name + " Recipes";
            }

            return View("Index", viewModel);
        }

        // Retrieve the details of a recipe from the
        // recipe repository and send them on to the 
        // view used to display them to the user.
        public ActionResult Details(int id)
        {
            var recipe = 
                this._recipeRepository.GetRecipe(id);

            var viewModel = 
                this._displayViewModelMapper.EntityToViewModel(recipe);

            return View(viewModel);
        }

        // Return a view that allows the requestor
        // to create new recipes.
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = this
                ._editViewModelMapper
                .EntityToViewModel(null);

            return View(viewModel);
        }

        // Respond to the request to create a new 
        // recipe and return to the requestor the 
        // list of recipes if it's successful or
        // return them to the edit page on
        // failure.  ONLY respond to HTTP POST
        // requests.  The runtime will use model
        // binding to populate the recipe parameter.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecipeEditViewModel recipe)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var recipeEntity =
                        this._editViewModelMapper.ViewModelToEntity(recipe);

                    this._recipeRepository.Save(recipeEntity);

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
        [Authorize]
        public ActionResult Edit(int id)
        {
            var recipeToEdit = 
                this
                ._recipeRepository
                .GetRecipe(id);

            if (!CanEdit(recipeToEdit))
            {
                // Simply return the user to the detail view.
                // Not too worried about the throw.
                return RedirectToAction("Details", 
                    new 
                    { 
                        id = id 
                    });
            }

            var viewModel = this
                ._editViewModelMapper
                .EntityToViewModel(recipeToEdit);

            return View(viewModel);
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
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RecipeEditViewModel recipe)
        {
            try
            {
                // Yes, it's a bit chatty, but we need to 
                // validate the entity from the database.
                var recipeEntity =
                    this._recipeRepository.GetRecipe(recipe.RecipeId);

                if (!CanEdit(recipeEntity))
                {
                    // Simply return the user to the detail view.
                    // Not too worried about the throw.
                    return RedirectToAction("Details", new { id = recipe.RecipeId });
                }

                var entityToSave = this
                    ._editViewModelMapper
                    .ViewModelToEntity(recipe);

                this._recipeRepository.Save(entityToSave);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private bool CanEdit(RecipeEntity entity)
        {
            if (Request.IsAuthenticated)
            {
                return WebSecurity.CurrentUserId == entity.Contributor.UserId;
            }

            return false;
        }
    }
}
