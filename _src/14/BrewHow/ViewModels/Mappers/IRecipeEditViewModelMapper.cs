using System;
using BrewHow.Domain.Entities;

namespace BrewHow.ViewModels.Mappers
{
    public interface IRecipeEditViewModelMapper : 
        IViewModelToEntityMapper<RecipeEntity, RecipeEditViewModel>,
        IEntityToViewModelMapper<RecipeEntity, RecipeEditViewModel>
    {
    }
}
