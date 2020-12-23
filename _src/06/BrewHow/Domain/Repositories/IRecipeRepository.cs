using System;
using System.Linq;

using BrewHow.Domain.Entities;

namespace BrewHow.Domain.Repositories
{
    public interface IRecipeRepository
    {
        RecipeEntity GetRecipe(int recipeId);
        IQueryable<RecipeEntity> GetRecipes();
        IQueryable<RecipeEntity> GetRecipesByStyle(string styleName);
        void Save(RecipeEntity recipeEntity);
    }
}
