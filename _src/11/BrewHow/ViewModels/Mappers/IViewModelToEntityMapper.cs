using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewHow.ViewModels.Mappers
{
    public interface IViewModelToEntityMapper<TEntity, TViewModel>
    {
        TEntity ViewModelToEntity(TViewModel viewModel);
    }
}
