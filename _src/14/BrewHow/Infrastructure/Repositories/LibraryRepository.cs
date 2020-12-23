using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BrewHow.Domain.Entities;
using BrewHow.Domain.Repositories;
using BrewHow.Models;

namespace BrewHow.Infrastructure.Repositories
{
    public class LibraryRepository : RepositoryBase, ILibraryRepository
    {
        public LibraryRepository(IBrewHowContext context)
            : base(context)
        {
        }

        public void AddRecipeToLibrary(int recipeId, int userId)
        {
            var recipe = this
                .Context
                .Recipes
                .FirstOrDefault(r => r.RecipeId == recipeId);

            if (recipe == null)
            {
                return;
            }

            var user = this
                .Context
                .UserProfiles
                .FirstOrDefault(u => u.UserId == userId);

            // No null check on this.  We're calling it.  If we 
            // call it w/o knowing who the user is, then shame on us.
            user.Library.Add(recipe);

            this.Context.SaveChanges();
        }

        /// <summary>
        /// Retrieves all of the recipes within a user's library.
        /// </summary>
        /// <param name="userId">The id of the user owning the library</param>
        /// <returns></returns>
        public IQueryable<RecipeEntity> GetRecipesInLibrary(int userId)
        {
            return this
                .Context
                .UserProfiles
                .Include("Library")
                .Where(u => u.UserId == userId)
                .SelectMany(u => u.Library)
                .OrderBy(r => r.Name)
                .Select(EntityMappingExpressions.AsRecipeEntity);
        }

        public void RemoveRecipeFromLibrary(int recipeId, int userId)
        {
            var recipe = this
                .Context
                .Recipes
                .FirstOrDefault(r => r.RecipeId == recipeId);

            if (recipe == null)
            {
                return;
            }

            var user = this
                .Context
                .UserProfiles
                .FirstOrDefault(u => u.UserId == userId);

            // No null check on this.  We're calling it.  If we 
            // call it w/o knowing who the user is, then shame on us.
            user.Library.Remove(recipe);

            this.Context.SaveChanges();
        }

    }
}