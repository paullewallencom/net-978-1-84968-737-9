using System;
using BrewHow.Domain.Entities;

namespace BrewHow.ViewModels.Mappers
{
    public interface IStyleDisplayViewModelMapper : 
        IEntityToViewModelMapper<StyleEntity, StyleDisplayViewModel>,
        IViewModelToEntityMapper<StyleEntity, StyleDisplayViewModel>
    {
    }
}
