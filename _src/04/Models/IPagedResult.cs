using System;

namespace BrewHow.Models
{
    public interface IPagedResult
    {
        int Page { get; }
        int TotalPages { get; }
    }
}
