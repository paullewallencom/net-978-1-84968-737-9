using System;
using BrewHow.Domain.Entities;

namespace BrewHow.ViewModels.Mappers
{
    public interface IRecipeDisplayViewModelMapper 
        : IEntityToViewModelMapper<RecipeEntity, RecipeDisplayViewModel>
    {
    }
}
