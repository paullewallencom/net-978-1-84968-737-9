using System;
using System.Collections.Generic;

namespace BrewHow.ViewModels
{
    public interface ITypedPagedResult<T> : IList<T>, IPagedResult
    {
    }
}
