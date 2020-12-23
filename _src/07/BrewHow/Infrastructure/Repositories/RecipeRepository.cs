using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using BrewHow.Domain.Entities;
using BrewHow.Domain.Repositories;
using BrewHow.Models;

namespace BrewHow.Infrastructure.Repositories
{
    public class RecipeRepository : RepositoryBase, IRecipeRepository
    {
        public RecipeRepository(IBrewHowContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Retrieve all recipes from the repository
        /// </summary>
        /// <returns>A queryable colleciton of recipe entities</returns>
        public IQueryable<RecipeEntity> GetRecipes()
        {
            return this.RecipeEntities;
        }

        /// <summary>
        /// Retrieves a specific recipe in the system. 
        /// </summary>
        /// <param name="recipeId">The id of the recipe
        /// to retrieve.</param>
        /// <returns>A recipe entity.</returns>
        public RecipeEntity GetRecipe(int recipeId)
        {
            return this
                .RecipeEntities
                .First(r => r.RecipeId == recipeId);
        }

        /// <summary>
        /// Retrieves all recipes belonging to a 
        /// particular style.
        /// </summary>
        /// <param name="styleName">Used to filter
        /// recipes by style.</param>
        /// <returns>A queryable collection of Recipe entities</returns>
        public IQueryable<RecipeEntity> GetRecipesByStyleSlug(string styleSlug)
        {
            return this
                .RecipeEntities
                .Where(r => r.Style.Slug == styleSlug);
        }

        /// <summary>
        /// Saves the state of a recipe to persistent
        /// storage.
        /// </summary>
        /// <param name="recipeEntity">The recipe to persist
        /// in storage.</param>
        public void Save(RecipeEntity recipeEntity)
        {
            if (recipeEntity == null)
            {
                throw new ArgumentNullException("recipe",
                    "You must specify a recipe to save.");
            }

            if (recipeEntity.Style == null)
            {
                throw new InvalidOperationException(
                    "Recipes require a style.");
            }

            var existingStyleModel = new Style();
            AssignEntityToModel(recipeEntity.Style, existingStyleModel);

            this.
                Context
                .Styles
                .Attach(existingStyleModel);

            if (recipeEntity.RecipeId == 0)
            {
                var newRecipeModel = new Recipe();
                this.Context.Recipes.Add(newRecipeModel);

                // Assign the properties that can only be assigned on creation.
                newRecipeModel.Style = existingStyleModel;
                newRecipeModel.Slug = recipeEntity.Slug;

                AssignEntityToModel(recipeEntity, newRecipeModel);

                this.Context.SaveChanges();
                return;
            }

            var recipeModel = this
                .Context
                .Recipes
                .FirstOrDefault(r => r.RecipeId == recipeEntity.RecipeId);

            if (recipeModel == null)
            {
                throw new InvalidOperationException(
                    "The recipe being modified does not exist.");
            }

            AssignEntityToModel(recipeEntity, recipeModel);
            recipeModel.Style = existingStyleModel;

            this.Context.SaveChanges();
        }

        /// <summary>
        /// Retrieves the base IQueryable upon which all
        /// queries are based.  The IQueryable marshalls
        /// the results to the entity -- performing the
        /// mapping but still allowing us to filter the 
        /// results.
        /// </summary>
        /// <param name="context">The context from which
        /// the IQueryable is retrieved.</param>
        /// <returns>A IQueryable for Recipes</returns>
        private IQueryable<RecipeEntity> RecipeEntities
        {
            get 
            { 
                return this
                    .Context
                    .Recipes
                    .Include("Style")
                    .OrderBy(r => r.Name)
                    .Select(AsRecipeEntity); 
            }
        }

        /// <summary>
        /// Expression to convert a Recipe model to a 
        /// RecipeEntity domain model.
        /// </summary>
        private static readonly Expression<Func<Recipe, RecipeEntity>> AsRecipeEntity =
            r => new RecipeEntity
            {
                RecipeId = r.RecipeId,
                Name = r.Name,
                OriginalGravity = r.OriginalGravity,
                FinalGravity = r.FinalGravity,
                GrainBill = r.GrainBill,
                Instructions = r.Instructions,
                Slug = r.Slug,
                Style = new StyleEntity 
                { 
                    Name = r.Style.Name, 
                    StyleId = r.Style.StyleId, 
                    Category = (CategoryEntity)r.Style.Category,
                    Slug = r.Style.Slug
                }
            };

        /// <summary>
        /// Assigns the values of a Recipe entity's 
        /// properties to the properties of a Recipe
        /// data model.
        /// </summary>
        /// <param name="recipe">The Recipe entity
        /// containing the property values to assign.
        /// </param>
        /// <param name="dbRecipe">The Recipe data
        /// model to receive the property values.
        /// </param>
        private void AssignEntityToModel(RecipeEntity recipe, Recipe dbRecipe)
        {
            dbRecipe.FinalGravity = recipe.FinalGravity;
            dbRecipe.GrainBill = recipe.GrainBill;
            dbRecipe.Instructions = recipe.Instructions;
            dbRecipe.Name = recipe.Name;
            dbRecipe.OriginalGravity = recipe.OriginalGravity;
            dbRecipe.RecipeId = recipe.RecipeId;
        }

        /// <summary>
        /// Assigns the values of a Style entity's 
        /// properties to the properties of a Style
        /// data model.
        /// </summary>
        /// <param name="style">The Style entity
        /// containing the property values to assign.
        /// </param>
        /// <param name="dbStyle">The Style data
        /// model to receive the property values.
        /// </param>
        private void AssignEntityToModel(StyleEntity style, Style dbStyle)
        {
            dbStyle.Name = style.Name;
            dbStyle.StyleId = style.StyleId;
            dbStyle.Category = ConvertToModel(style.Category);
            dbStyle.Slug = style.Slug;
        }

        /// <summary>
        /// Converts a Category entity to a Category data
        /// model.
        /// </summary>
        /// <param name="category">The Category entity to
        /// convert to a Category model.</param>
        /// <returns>A Category model.</returns>
        private Category ConvertToModel(CategoryEntity category)
        {
            switch (category)
            {
                case CategoryEntity.Lager:
                    return Category.Lager;
                default:
                    return Category.Ale;
            }
        }
    }
}