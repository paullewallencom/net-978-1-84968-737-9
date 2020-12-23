using System;
using System.Linq;
using BrewHow.Domain.Entities;

namespace BrewHow.Domain.Repositories
{
    public interface ILibraryRepository
    {
        void AddRecipeToLibrary(int recipeId, int userId);
        IQueryable<RecipeEntity> GetRecipesInLibrary(int userId);
        void RemoveRecipeFromLibrary(int recipeId, int userId);
    }
}