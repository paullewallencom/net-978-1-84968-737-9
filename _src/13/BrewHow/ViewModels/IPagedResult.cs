using System;
using System.Collections.Generic;

namespace BrewHow.ViewModels
{
    public interface IPagedResult
    {
        int Page { get; }
        int TotalPages { get; }
    }
}
